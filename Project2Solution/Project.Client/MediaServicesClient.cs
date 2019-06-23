using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Project.Client {

    public class MediaServicesClient {

        private static Configuration _configuration;
        private static ClientCredential _clientCredential;
        private static ServiceClientCredentials _serviceClientCredentials;
        private static IAzureMediaServicesClient _client;
        private static Transform _encoding;

        /// <summary>
        /// Creates credentials for the Azure Media Service based on credentials from Configuration (appsettings.json)
        /// </summary>
        /// <returns>Generic asynchronous operation that returns type ServiceClientCredentials</returns>

        private static async Task < ServiceClientCredentials > ClientCredentials () {

            if ( _clientCredential == null )
                _clientCredential = new ClientCredential ( _configuration.AadClientId, _configuration.AadSecret );

            return await
                ApplicationTokenProvider
                    .LoginSilentAsync( _configuration.AadTenantId, _clientCredential, ActiveDirectoryServiceSettings.Azure );

        }

        /// <summary>
        /// Create an AzureMediaServicesClient object based on the credentials supplied in appsettings.json
        /// </summary>
        /// <returns>Generic asynchronous operation that returns type IAzureMediaServicesClient</returns>

        private static async Task < IAzureMediaServicesClient > ClientService () {

            if ( _configuration == null ) {

                _configuration = new Configuration (

                    new ConfigurationBuilder ()
                        .SetBasePath ( Directory.GetCurrentDirectory () )
                        .AddJsonFile ( "appsettings.json", optional: true, reloadOnChange: true )
                        .AddEnvironmentVariables ()
                        .Build ()

                );

            }

            _serviceClientCredentials = await ClientCredentials ();

            return new AzureMediaServicesClient ( _configuration.ArmEndpoint, _serviceClientCredentials ) {

                SubscriptionId = _configuration.SubscriptionId

            };

        }

        /// <summary>
        /// Connects to the Azure Media Service
        /// </summary>
        /// <returns>Generic asynchronous operation that returns type IAzureMediaServicesClient</returns>

        public static async Task < IAzureMediaServicesClient > Connect ( string TransformName = "transformName") {

            _client = await ClientService ();

            _encoding = await _client.Transforms.GetAsync ( _configuration.ResourceGroup, _configuration.AccountName, TransformName );

            _client.LongRunningOperationRetryTimeout = 2;

            if ( _encoding == null ) {

                TransformOutput [] output = new TransformOutput [] {

                    new TransformOutput {

                        Preset = new BuiltInStandardEncoderPreset () {

                            PresetName = EncoderNamedPreset.AdaptiveStreaming

                        }

                    }

                };

                _encoding = await _client.Transforms.CreateOrUpdateAsync ( _configuration.ResourceGroup, _configuration.AccountName, TransformName, output );

            }

            return _client;

        }

        /// <summary>
        /// Uploads the specified local video file into an Asset input stream
        /// </summary>
        /// <param name="FileToUpload">Name of the file to upload</param>
        /// <param name="TransformName">Name of the transformation object (optional)</param>
        /// <returns>Generic asynchronous operation that returns type Asset</returns>

        public static async Task < Asset > Upload ( string @FileToUpload, string TransformName = "transformName") {

            var AssetName = Path.GetFileName ( FileToUpload ).Split ( '.' ) [ 0 ];
            var asset     = await _client.Assets.GetAsync ( _configuration.ResourceGroup, _configuration.AccountName, AssetName );

            if ( asset == null ) { 

                var uploadId  = Guid.NewGuid ().ToString ();
                var output    = await _client.Assets.CreateOrUpdateAsync ( _configuration.ResourceGroup, _configuration.AccountName, "Output-" + AssetName + uploadId, new Asset () );

                asset = await _client.Assets.CreateOrUpdateAsync (

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    AssetName,
                    new Asset ()

                );

                var response = await _client.Assets.ListContainerSasAsync (

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    AssetName,
                    permissions: AssetContainerPermission.ReadWrite,
                    expiryTime: DateTime.Now.AddHours ( 4 ).ToUniversalTime ()

                );

                var container = new CloudBlobContainer ( new Uri ( response.AssetContainerSasUrls [ 0 ] ) );

                await container.GetBlockBlobReference ( Path.GetFileName ( FileToUpload ) ).UploadFromFileAsync ( FileToUpload );

                Job job = await _client.Jobs.CreateAsync (

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    TransformName,
                    "Job-" + AssetName + uploadId,
                    new Job {

                        Input = new JobInputAsset ( assetName: AssetName ),
                        Outputs = new JobOutput [] { new JobOutputAsset ( output.Name ) },

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

        public static async Task < Asset > Upload ( Stream stream, string @FileToUpload, string TransformName = "transformName") {

            var AssetName = Path.GetFileName ( FileToUpload ).Split ( '.' ) [ 0 ];
            var asset     = await _client.Assets.GetAsync ( _configuration.ResourceGroup, _configuration.AccountName, AssetName );

            if ( asset == null ) { 

                var uploadId  = Guid.NewGuid ().ToString ();
                var output    = await _client.Assets.CreateOrUpdateAsync ( _configuration.ResourceGroup, _configuration.AccountName, "Output-" + uploadId, new Asset () );

                asset = await _client.Assets.CreateOrUpdateAsync (

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    AssetName,
                    new Asset ()

                );

                var response = await _client.Assets.ListContainerSasAsync (

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    AssetName,
                    permissions: AssetContainerPermission.ReadWrite,
                    expiryTime: DateTime.Now.AddHours ( 4 ).ToUniversalTime ()

                );

                var container = new CloudBlobContainer ( new Uri ( response.AssetContainerSasUrls [ 0 ] ) );

                await container.GetBlockBlobReference ( Path.GetFileName ( FileToUpload ) ).UploadFromStreamAsync ( stream );

                Job job = await _client.Jobs.CreateAsync (

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    TransformName,
                    "Job-" + uploadId,
                    new Job {

                        Input = new JobInputAsset ( assetName: AssetName ),
                        Outputs = new JobOutput [] { new JobOutputAsset ( output.Name ) },

                    }

                );

            }

            return asset;

        }

        public static async Task Download ( string @Location, int? maxResults = 5, bool Direct = false ) {


            BlobContinuationToken continueToken = null;
            IList < Task > downloads = new List < Task > ();
            var AssetName = ( Path.GetFileName ( @Location ).Split ( '.' ) [ 0 ] );
            var @path = Location.Substring ( 0, @Location.LastIndexOf ( @"\" ) + 1 );

            if ( Direct && ! Directory.Exists ( @Location ) ) Directory.CreateDirectory ( @path );

            var asset = await _client.Assets.ListContainerSasAsync ( 

                _configuration.ResourceGroup, 
                _configuration.AccountName, 
                AssetName, 
                permissions: AssetContainerPermission.Read,
                expiryTime: DateTime.UtcNow.AddHours ( 1 ).ToUniversalTime () 
                
            );

            var container = new CloudBlobContainer ( new Uri ( asset.AssetContainerSasUrls [ 0 ] ) );

            do {

                var segment = await container.ListBlobsSegmentedAsync ( null, true, BlobListingDetails.None, maxResults, continueToken, null, null );

                foreach ( var element in segment.Results ) {

                    var blob = ( CloudBlockBlob ) element;

                    if ( blob != null ) {

                        downloads.Add ( blob.DownloadToFileAsync ( AssetName, FileMode.Create ) );

                    }

                }

                continueToken = segment.ContinuationToken;

            } while ( continueToken == null );

            await Task.WhenAll ( downloads );

        }

        public static async Task < StreamingLocator > Stream ( string assetName, string streamingEndpointName ) {

            var streamingUrls = new List < string > ();
            

            var locator = await _client.StreamingLocators.CreateAsync (
            
                _configuration.ResourceGroup,
                _configuration.AccountName,
                "locator-" + Guid.NewGuid (),
                new StreamingLocator {

                    AssetName = assetName,
                    StreamingPolicyName = PredefinedStreamingPolicy.ClearStreamingOnly

                }
                
            );

            return null;

        }

        public static Task < IAzureMediaServicesClient > Client { get { return ( Task < IAzureMediaServicesClient >)_client; } }

        public static Configuration Configuration { get { return _configuration; } }

    }

}