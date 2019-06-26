using System;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Tests {

    public class Tests {

        [ Test ]
        public async void A_Reset () {

            try {


                if ( Project.Client.MediaServicesClient.Client == null )
                    await Project.Client.MediaServicesClient.Connect ();

                await Project.Client.MediaServicesClient.ResetMediaService ();

                Thread.Sleep ( 30000 );

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void B_UploadFile () {

            try {

                if ( Project.Client.MediaServicesClient.Client == null )
                    await Project.Client.MediaServicesClient.Connect ();

                await Project.Client.MediaServicesClient.Upload ( @"C:\Users\jmau0\Desktop\Revature\Week 6\Prototype.Player\bin\Debug\netcoreapp2.2\Fade into you (Faithless).mp3" );

                Thread.Sleep ( 30000 );

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void C_UploadStream () {

            try {

                if ( Project.Client.MediaServicesClient.Client == null )
                    await Project.Client.MediaServicesClient.Connect ();

                await Project.Client.MediaServicesClient.Upload ( 
                
                    File.OpenRead (

                        @"C:\Users\jmau0\Desktop\Revature\Week 6\Prototype.Player\bin\Debug\netcoreapp2.2\03 - July.mp3"

                    ),

                    @"C:\Users\jmau0\Desktop\Revature\Week 6\Prototype.Player\bin\Debug\netcoreapp2.2\03 - July.mp3"

                );

                Thread.Sleep ( 30000 );

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void D_DownloadFile () {

            try {

                if ( Project.Client.MediaServicesClient.Client == null )
                    await Project.Client.MediaServicesClient.Connect ();

                await Project.Client.MediaServicesClient.Download ( @"C:\Users\jmau0\Desktop\SuperSpecialSongs\Fade into you (Faithless).mp3" );

                Thread.Sleep ( 30000 );

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void D_DownloadStream () {

            try {

                if ( Project.Client.MediaServicesClient.Client == null )
                    await Project.Client.MediaServicesClient.Connect ();

                

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void E_StreamingUrls () {

            try {

                var result = "Streaming Urls - \n";

                if ( Project.Client.MediaServicesClient.Client == null )
                    await Project.Client.MediaServicesClient.Connect ();

                var output = await Project.Client.MediaServicesClient.StreamingUri ( "Fade into you (Faithless).mp3" );
            
                Console.WriteLine ( "\n\nStreaming Urls -" );

                foreach ( var element in output )
                    result += "\n" + element;

                Assert.Pass ( result );

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

    }

}
