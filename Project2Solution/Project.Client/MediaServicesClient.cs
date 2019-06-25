using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Project.Client
{

    public class MediaServicesClient
    {

        private static Configuration _configuration;
        private static ClientCredential _clientCredential;
        private static ServiceClientCredentials _serviceClientCredentials;
        private static IAzureMediaServicesClient _client;
        private static Transform _encoding;

        /// <summary>
        /// Creates credentials for the Azure Media Service based on credentials from Configuration (appsettings.json)
        /// </summary>
        /// <returns>Generic asynchronous operation that returns type ServiceClientCredentials</returns>

        private static async Task<ServiceClientCredentials> ClientCredentials()
        {

            if (_clientCredential == null)
                _clientCredential = new ClientCredential(_configuration.AadClientId, _configuration.AadSecret);

            return await
                ApplicationTokenProvider
                    .LoginSilentAsync(_configuration.AadTenantId, _clientCredential, ActiveDirectoryServiceSettings.Azure);

        }

        /// <summary>
        /// Create an AzureMediaServicesClient object based on the credentials supplied in appsettings.json
        /// </summary>
        /// <returns>Generic asynchronous operation that returns type IAzureMediaServicesClient</returns>

        private static async Task<IAzureMediaServicesClient> ClientService()
        {

            if (_configuration == null)
            {

                _configuration = new Configuration(

                    new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build()

                );

            }

            _serviceClientCredentials = await ClientCredentials();

            return new AzureMediaServicesClient(_configuration.ArmEndpoint, _serviceClientCredentials)
            {

                SubscriptionId = _configuration.SubscriptionId

            };

        }

        /// <summary>
        /// Connects to the Azure Media Service
        /// </summary>
        /// <returns>Generic asynchronous operation that returns type IAzureMediaServicesClient</returns>

        public static async Task<IAzureMediaServicesClient> Connect(string TransformName = "transformName")
        {

            _client = await ClientService();

            _encoding = await _client.Transforms.GetAsync(_configuration.ResourceGroup, _configuration.AccountName, TransformName);

            _client.LongRunningOperationRetryTimeout = 2;

            if (_encoding == null)
            {

                TransformOutput[] output = new TransformOutput[] {

                    new TransformOutput {

                        Preset = new BuiltInStandardEncoderPreset () {

                            PresetName = EncoderNamedPreset.AdaptiveStreaming

                        }

                    }

                };

                _encoding = await _client.Transforms.CreateOrUpdateAsync(_configuration.ResourceGroup, _configuration.AccountName, TransformName, output);

            }

            return _client;

        }

        /// <summary>
        /// Uploads the specified local video file into an Asset input stream
        /// </summary>
        /// <param name="FileToUpload">Name of the file to upload</param>
        /// <param name="TransformName">Name of the transformation object (optional)</param>
        /// <returns>Generic asynchronous operation that returns type Asset</returns>

        public static async Task<Asset> Upload(string @FileToUpload, string TransformName = "transformName")
        {

            var AssetName = Path.GetFileName(FileToUpload).Split('.')[0];
            var asset = await _client.Assets.GetAsync(_configuration.ResourceGroup, _configuration.AccountName, AssetName);

            if (asset == null)
            {

                var output = await _client.Assets.CreateOrUpdateAsync(_configuration.ResourceGroup, _configuration.AccountName, "Output-" + AssetName, new Asset());

                asset = await _client.Assets.CreateOrUpdateAsync(

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    AssetName,
                    new Asset()

                );

                var response = await _client.Assets.ListContainerSasAsync(

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    AssetName,
                    permissions: AssetContainerPermission.ReadWrite,
                    expiryTime: DateTime.Now.AddHours(4).ToUniversalTime()

                );

                var container = new CloudBlobContainer(new Uri(response.AssetContainerSasUrls[0]));

                await container.GetBlockBlobReference(Path.GetFileName(FileToUpload)).UploadFromFileAsync(FileToUpload);

                Job job = await _client.Jobs.CreateAsync(

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    TransformName,
                    "Job-" + AssetName,
                    new Job
                    {

                        Input = new JobInputAsset(assetName: AssetName),
                        Outputs = new JobOutput[] { new JobOutputAsset(output.Name) },

                    }

                );

            }

            return asset;

        }

        /// <summary>
        /// Uploads the specified remote video file into from an input stream
        /// </summary>
        /// <param name="stream">Stream to upload into an asset</param>
        /// <param name="FileToUpload">Name of the file to upload</param>
        /// <param name="TransformName">Name of the transformation object (optional)</param>
        /// <returns>Generic asynchronous operation that returns type Asset</returns>

        public static async Task<Asset> Upload(Stream stream, string @FileToUpload, string TransformName = "transformName")
        {

            var AssetName = Path.GetFileName(FileToUpload).Split('.')[0];
            var asset = await _client.Assets.GetAsync(_configuration.ResourceGroup, _configuration.AccountName, AssetName);

            if (asset == null)
            {

                var output = await _client.Assets.CreateOrUpdateAsync(_configuration.ResourceGroup, _configuration.AccountName, "Output-" + AssetName, new Asset());

                asset = await _client.Assets.CreateOrUpdateAsync(

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    AssetName,
                    new Asset()

                );

                var response = await _client.Assets.ListContainerSasAsync(

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    AssetName,
                    permissions: AssetContainerPermission.ReadWrite,
                    expiryTime: DateTime.Now.AddHours(4).ToUniversalTime()

                );

                var container = new CloudBlobContainer(new Uri(response.AssetContainerSasUrls[0]));

                await container.GetBlockBlobReference(Path.GetFileName(FileToUpload)).UploadFromStreamAsync(stream);

                Job job = await _client.Jobs.CreateAsync(

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    TransformName,
                    "Job-" + AssetName,
                    new Job
                    {

                        Input = new JobInputAsset(assetName: AssetName),
                        Outputs = new JobOutput[] { new JobOutputAsset(output.Name) },

                    }

                );

            }

            return asset;

        }

        /// <summary>
        /// File transfer of media file to local storage
        /// </summary>
        /// <param name="OutputFolder">Full path including file name (no file extension)</param>
        /// <returns>Download Task</returns>

        public static async Task Download(string OutputFolder)
        {

            var AssetName = Path.GetFileName(OutputFolder).Split('.')[0];
            BlobContinuationToken continueToken = null;
            Task download = null;

            if (!Directory.Exists(@OutputFolder)) Directory.CreateDirectory(@OutputFolder);

            var assetSasContainer = await _client.Assets.ListContainerSasAsync(

                _configuration.ResourceGroup,
                _configuration.AccountName,
                AssetName,
                permissions: AssetContainerPermission.Read,
                expiryTime: DateTime.UtcNow.AddHours(1).ToUniversalTime()

            );

            var BlobContainer = new CloudBlobContainer(new Uri(assetSasContainer.AssetContainerSasUrls[0]));

            do
            {

                var segments =
                    await BlobContainer.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, 1, continueToken, null, null);

                foreach (var segment in segments.Results)
                {

                    var blobBlock = (CloudBlockBlob)segment;

                    if (blobBlock != null)
                        download = (blobBlock.DownloadToFileAsync(Path.Combine(@OutputFolder, blobBlock.Name), FileMode.Create));

                }

                continueToken = segments.ContinuationToken;

            } while (continueToken != null);

            await Task.WhenAny(download);

        }

        /// <summary>
        /// Stream transfer of media file to local storage
        /// </summary>
        /// <param name="stream">Stream of the song to download</param>
        /// <param name="OutputFolder">Full path including file name (no file extension)</param>
        /// <returns></returns>

        public static async Task<Stream> Download(string OutputFolder, Stream stream)
        {

            var AssetName = Path.GetFileName(OutputFolder).Split('.')[0];
            BlobContinuationToken continueToken = null;
            Task<Stream> download = null;

            var assetSasContainer = await _client.Assets.ListContainerSasAsync(

                _configuration.ResourceGroup,
                _configuration.AccountName,
                AssetName,
                permissions: AssetContainerPermission.Read,
                expiryTime: DateTime.UtcNow.AddHours(1).ToUniversalTime()

            );

            var BlobContainer = new CloudBlobContainer(new Uri(assetSasContainer.AssetContainerSasUrls[0]));

            do
            {

                var segments =
                    await BlobContainer.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, 1, continueToken, null, null);

                foreach (var segment in segments.Results)
                {

                    var blobBlock = (CloudBlockBlob)segment;

                    if (blobBlock != null)
                        download = (Task<Stream>)blobBlock.UploadFromStreamAsync(stream);

                }

                continueToken = segments.ContinuationToken;

            } while (continueToken != null);

            await Task.WhenAny(download);

            return download.Result;

        }

        public static async Task<IList<string>> Stream(string assetName, string streamingEndpointName)
        {

            var streamingUrls = new List<string>();

            var locator = await _client.StreamingLocators.CreateAsync(

                _configuration.ResourceGroup,
                _configuration.AccountName,
                "locator-" + assetName,
                new StreamingLocator
                {

                    AssetName = assetName,
                    StreamingPolicyName = PredefinedStreamingPolicy.ClearStreamingOnly

                }

            );

            var streamingEndpoint = await _client.StreamingEndpoints.GetAsync(

                _configuration.ResourceGroup,
                _configuration.AccountName,
                streamingEndpointName

            );

            var paths = await _client.StreamingLocators.ListPathsAsync(

                _configuration.ResourceGroup,
                _configuration.AccountName,
                "locator-" + assetName

            );

            foreach (var path in paths.StreamingPaths)

                streamingUrls.Add(

                    new UriBuilder
                    {

                        Scheme = "https",
                        Host = streamingEndpoint.HostName,
                        Path = path.Paths[0]

                    }.ToString()

                );


            return streamingUrls;

        }

        public static Task<IAzureMediaServicesClient> Client { get { return (Task<IAzureMediaServicesClient>)_client; } }

        public static Configuration Configuration { get { return _configuration; } }

    }

}