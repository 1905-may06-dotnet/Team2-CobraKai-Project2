using System;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Tests {

    public class Tests {

        [ Test ]
        public async void A_Reset () {

            try {


                if ( Prototype.Player.Azure.MediaServicesClient.Client == null )
                    await Prototype.Player.Azure.MediaServicesClient.Connect ();

                await Prototype.Player.Azure.MediaServicesClient.ResetMediaService ();

                Thread.Sleep ( 30000 );

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void B_UploadFile () {

            try {

                if ( Prototype.Player.Azure.MediaServicesClient.Client == null )
                    await Prototype.Player.Azure.MediaServicesClient.Connect ();

                await Prototype.Player.Azure.MediaServicesClient.Upload ( @"C:\Users\jmau0\Desktop\Revature\Week 6\Prototype.Player\bin\Debug\netcoreapp2.2\Fade into you (Faithless).mp3" );

                Thread.Sleep ( 30000 );

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void C_UploadStream () {

            try {

                if ( Prototype.Player.Azure.MediaServicesClient.Client == null )
                    await Prototype.Player.Azure.MediaServicesClient.Connect ();

                await Prototype.Player.Azure.MediaServicesClient.Upload ( 
                
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

                if ( Prototype.Player.Azure.MediaServicesClient.Client == null )
                    await Prototype.Player.Azure.MediaServicesClient.Connect ();

                await Prototype.Player.Azure.MediaServicesClient.Download ( @"C:\Users\jmau0\Desktop\SuperSpecialSongs\Fade into you (Faithless).mp3" );

                Thread.Sleep ( 30000 );

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void D_DownloadStream () {

            try {

                if ( Prototype.Player.Azure.MediaServicesClient.Client == null )
                    await Prototype.Player.Azure.MediaServicesClient.Connect ();

                

                Assert.Pass ();

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

        [ Test ]
        public async void E_StreamingUrls () {

            try {

                var result = "Streaming Urls - \n";

                if ( Prototype.Player.Azure.MediaServicesClient.Client == null )
                    await Prototype.Player.Azure.MediaServicesClient.Connect ();

                var output = await Prototype.Player.Azure.MediaServicesClient.StreamingUri ( "Fade into you (Faithless).mp3" );
            
                Console.WriteLine ( "\n\nStreaming Urls -" );

                foreach ( var element in output )
                    result += "\n" + element;

                Assert.Pass ( result );

            } catch ( Exception ex ) { Assert.Fail ( ex.Message ); }

        }

    }

}