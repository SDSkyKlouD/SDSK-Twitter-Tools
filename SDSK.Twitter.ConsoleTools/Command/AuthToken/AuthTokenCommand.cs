using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SDSK.Twitter.ConsoleTools.Command.AuthToken {
    class AuthTokenCommand : ICommand {
        public string CommandHelpDescription { get; } = "Get user auth token of Twitter app using PIN authentication method\n" +
                                                        "THIS SHOULD BE USED AND INTENDED FOR TEST/TEMPORARY USE PURPOSE ONLY";
        public List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)> {
            ("consumer_key", "Consumer key of Twitter app", false),
            ("consumer_secret", "Consumer secret key of Twitter app", false),
        };

        public void DoCommand(params string[] args) {
            if(args.Length == 2) {
                // Check-up
                string consumerKey = args[0];
                string consumerSecret = args[1];

                if(consumerKey.Length != 25) {
                    // Consumer key length check
                    Console.WriteLine("Consumer key (parameter 1) is not 25 character long.");
                    return;
                }

                if(consumerSecret.Length != 50) {
                    // Consumer secret length check
                    Console.WriteLine("Consumer secret (parameter 2) is not 50 character long.");
                    return;
                }

                // Start PIN authorization
                Console.WriteLine("\n\n~~~ !!! DO NOT PROCEED AT PUBLIC PLACE! I WARNED YOU! !!! ~~~\n\n");

                Console.WriteLine("Initiating authentication with consumer key/secret...\n");

                var auth = Tweetinvi.AuthFlow.InitAuthentication(new Tweetinvi.Models.ConsumerCredentials(consumerKey, consumerSecret));
                string pinNumber = null;

                if(OpenUrl(auth.AuthorizationURL)) {
                    Console.WriteLine("Your web browser will be opened to Twitter app authentication page.");
                    Console.WriteLine($"  If not opened, manually open an web browser and navigate to: {auth.AuthorizationURL}\n");
                } else {
                    Console.WriteLine($"Opening URL failed! You can manually open an web browser and navigate to: {auth.AuthorizationURL}\n");
                }

                Console.WriteLine("After logged in, the page will provide 7-character PIN numbers.");
                do {
                    Console.Write("Please provide the correct PIN numbers here: ");
                    pinNumber = Console.ReadLine();
                } while(!(int.TryParse(pinNumber, out _) && pinNumber.Length == 7));

                Console.WriteLine("\nProcessing...\n");

                var cred = Tweetinvi.AuthFlow.CreateCredentialsFromVerifierCode(pinNumber, auth);

                if(cred != null && !string.IsNullOrEmpty(cred.AccessToken) && !string.IsNullOrEmpty(cred.AccessTokenSecret)) {
                    Console.WriteLine($"  Access token:        {cred.AccessToken}");
                    Console.WriteLine($"  Access token secret: {cred.AccessTokenSecret}");

                    Console.WriteLine("\nDO NOT SHARE YOUR ACCESS TOKENS TO STRANGERS!!!");
                } else {
                    Console.WriteLine("Credential information is null or empty. Try again later.");
                }
            } else {
                Console.WriteLine("There should be exactly 2 parameters passed to this command.");
                Console.WriteLine("You can check how to use this command with 'help' command.");
            }
        }

        public bool OpenUrl(string url) {
            // .NET Core Process.Start(<url>) workaround solution referenced from https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
            try {
                Process.Start(url);
            } catch {
                if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}") {
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    });
                } else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                    Process.Start("xdg-open", url);
                } else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    Process.Start("open", url);
                } else {
                    return false;
                }
            }

            return true;
        }
    }
}
