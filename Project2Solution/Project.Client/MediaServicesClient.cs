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

namespace Project.Client {

    public class MediaServicesClient {

        private static Configuration             _configuration;
        private static ClientCredential          _clientCredential;
        private static ServiceClientCredentials  _serviceClientCredentials;
        private static IAzureMediaServicesClient _client;
        private static Transform                 _encoding;

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

            try {

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

            } catch ( Exception exception ) {

                if ( exception.Source.Contains ( "ActiveDirectory" ) )
                    Console.Error.WriteLine ( "TIP: Make sure that you have filled out the appsettings.json file before running this sample." );
                else if ( exception.Source.Contains ( "Forbidden" ) )
                    Console.Error.WriteLine ( "TIP: Make sure the resource identified in the appsettings.json file has not been deleted or configured with CORS which would require the enclusion of special headers" );

                Console.Error.WriteLine ( $"{ exception.Message }" );

                ApiErrorException apiException = exception.GetBaseException() as ApiErrorException;

                if ( apiException != null )
                    Console.Error.WriteLine (
                        $"ERROR: API call failed with error code '{ apiException.Body.Error.Code }' and message '{ apiException.Body.Error.Message }'."
                    );

                return null;

            }

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
        /// Removes all active jobs and assets from the media service
        /// </summary>

        public static async Task ResetMediaService () {

            var token = new System.Threading.CancellationToken ();

            var jobs   = await _client.Jobs.ListAsync ( _configuration.ResourceGroup, _configuration.AccountName, _encoding.Name, null, token );
            var assets = await _client.Assets.ListAsync ( _configuration.ResourceGroup, _configuration.AccountName );

            foreach ( var job in jobs )
                await _client.Jobs.DeleteAsync ( _configuration.ResourceGroup, _configuration.AccountName, _encoding.Name, job.Name );

            foreach ( var asset in assets )
                await _client.Assets.DeleteAsync ( _configuration.ResourceGroup, _configuration.AccountName, asset.Name );

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

                var output    = await _client.Assets.CreateOrUpdateAsync ( _configuration.ResourceGroup, _configuration.AccountName, "Output-" + AssetName, new Asset () );

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
                    "Job-" + AssetName,
                    new Job {

                        Input   = new JobInputAsset ( assetName: AssetName ),
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

                var output    = await _client.Assets.CreateOrUpdateAsync ( _configuration.ResourceGroup, _configuration.AccountName, "Output-" + AssetName, new Asset () );

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

                //await container.GetBlockBlobReference ( AssetName ).UploadFromStreamAsync ( stream );

                Job job = await _client.Jobs.CreateAsync (

                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    TransformName,
                    "Job-" + AssetName,
                    new Job {

                        Input   = new JobInputAsset ( assetName: AssetName ),
                        Outputs = new JobOutput [] { new JobOutputAsset ( output.Name ) },

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

        public static async Task Download ( string OutputFolder ) {

           var AssetName                       = Path.GetFileName ( OutputFolder ).Split ( '.' ) [ 0 ];
           BlobContinuationToken continueToken = null;
           Task download                       = null;

           if ( ! Directory.Exists ( @OutputFolder ) ) Directory.CreateDirectory ( @OutputFolder );

           var assetSasContainer = await _client.Assets.ListContainerSasAsync (

               _configuration.ResourceGroup,
               _configuration.AccountName,
               AssetName,
               permissions: AssetContainerPermission.Read,
               expiryTime: DateTime.UtcNow.AddHours ( 1 ).ToUniversalTime ()

           );

           var BlobContainer = new CloudBlobContainer ( new Uri ( assetSasContainer.AssetContainerSasUrls [ 0 ] ) );

           do {

               var segments =
                   await BlobContainer.ListBlobsSegmentedAsync ( null, true, BlobListingDetails.None, 1, continueToken, null, null );

               foreach ( var segment in segments.Results ) {

                   var blobBlock = ( CloudBlockBlob ) segment;

                   if ( blobBlock != null )
                       download = ( blobBlock.DownloadToFileAsync ( Path.Combine ( @OutputFolder, blobBlock.Name ), FileMode.Create ) );

               }

               continueToken = segments.ContinuationToken;

           } while ( continueToken != null );

           await Task.WhenAny ( download );

       }

        /// <summary>
        /// Stream transfer of media file to local storage
        /// </summary>
        /// <param name="local">Whether or not to download a file locally or a stream for transfer</param>
        /// <param name="OutputFolder">Full path including file name (no file extension)</param>
        /// <returns>Generic task of type 'Stream'</returns>

        public static async Task < Stream > Download ( string OutputFolder, bool local = false ) {

            var AssetName                       = Path.GetFileName ( OutputFolder ).Split ( '.' ) [ 0 ];
            BlobContinuationToken continueToken = null;
            Task < Stream > download            = null;

            var assetSasContainer = await _client.Assets.ListContainerSasAsync (

                _configuration.ResourceGroup,
                _configuration.AccountName,
                AssetName,
                permissions: AssetContainerPermission.Read,
                expiryTime: DateTime.UtcNow.AddHours ( 1 ).ToUniversalTime ()

            );

            var BlobContainer = new CloudBlobContainer ( new Uri ( assetSasContainer.AssetContainerSasUrls [ 0 ] ) );

            do {

                var segments =
                    await BlobContainer.ListBlobsSegmentedAsync ( null, true, BlobListingDetails.None, 1, continueToken, null, null );

                foreach ( var segment in segments.Results ) {

                    var blobBlock = ( CloudBlockBlob ) segment;

                    if ( blobBlock != null )
                         await blobBlock.DownloadToStreamAsync ( download.Result );

                }

                continueToken = segments.ContinuationToken;

            } while ( continueToken != null );

            await Task.WhenAny ( download );

            return download.Result;

        }

        /// <summary>
        /// Returns a list of streaming Uri's
        /// </summary>
        /// <param name="OutputName">Name of the song to play</param>
        /// <returns>A task of type IList < string > </string></returns>

        public static async Task < IList < string > > StreamingUri ( string OutputName ) {

            var streamingUrls = new List < string > ();

            var uriBuilder    = new UriBuilder ();

            var locator = await _client.StreamingLocators.GetAsync (
            
                _configuration.ResourceGroup,
                _configuration.AccountName,
                "Locator-" + ( OutputName.Contains ( "." ) ? OutputName.Split ( "." ) [ 0 ] : OutputName )

            );

            if ( locator == null ) {

                locator = await _client.StreamingLocators.CreateAsync (
            
                    _configuration.ResourceGroup,
                    _configuration.AccountName,
                    "Locator-" + ( OutputName.Contains ( "." ) ? OutputName.Split ( "." ) [ 0 ] : OutputName ),

                    new StreamingLocator {

                        AssetName = "Output-" + ( OutputName.Contains ( "." ) ? OutputName.Split ( "." ) [ 0 ] : OutputName ),
                        StreamingPolicyName = PredefinedStreamingPolicy.ClearStreamingOnly

                    }

                );

            }

            var streamingEndpoint = await _client.StreamingEndpoints.GetAsync ( 
                
                _configuration.ResourceGroup, _configuration.AccountName, "default"

            );

            if ( streamingEndpoint != null )

                if ( streamingEndpoint.ResourceState != StreamingEndpointResourceState.Running )

                    await _client.StreamingEndpoints.StartAsync (

                        _configuration.ResourceGroup, _configuration.AccountName, "default"

                    );

            ListPathsResponse paths = await _client.StreamingLocators.ListPathsAsync (  
            
                _configuration.ResourceGroup,
                _configuration.AccountName,
                locator.Name
                
            );

            uriBuilder.Scheme = "https";
            uriBuilder.Host   = streamingEndpoint.HostName;

            foreach ( var path in paths.StreamingPaths ) {

                uriBuilder.Path = path.Paths [ 0 ];
                streamingUrls.Add ( uriBuilder.ToString () );

            }

            return streamingUrls;

        }

        public static Task < IAzureMediaServicesClient > Client { get { return ( Task < IAzureMediaServicesClient >)_client; } }

        public static Configuration Configuration { get { return _configuration; } }

    }

}