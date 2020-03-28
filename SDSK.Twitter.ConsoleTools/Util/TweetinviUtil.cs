using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Tweetinvi;
using Tweetinvi.Models;

namespace SDSK.Twitter.ConsoleTools.Util {
    public static class TweetinviUtil {
        public static ITwitterCredentials AuthorizeAndGetCredentials(SecureString consumerKey, SecureString consumerSecret) {
            string consumerKeyStr = consumerKey.AsString();
            string consumerSecretStr = consumerSecret.AsString();

            if(consumerKeyStr.Length != 25) {
                // Consumer key length check
                Console.WriteLine("Consumer key is not 25 characters long.");

                return null;
            }

            if(consumerSecretStr.Length != 50) {
                // Consumer secret key length check
                Console.WriteLine("Consumer secret is not 50 characters long.");

                return null;
            }

            Console.WriteLine("Initiating PIN authentiation with given application consumer key/secret...");

            var auth = AuthFlow.InitAuthentication(new ConsumerCredentials(consumerKeyStr, consumerSecretStr));
            string pinNumber;

            if(OpenUrl(auth.AuthorizationURL)) {
                Console.WriteLine("Your web browser will be opened and navigate to Twitter app authentication page.");
                Console.WriteLine($"  If not opened, manually open an web browser and navigate to: {auth.AuthorizationURL}\n");
            } else {
                Console.WriteLine($"Opening URL failed! You can manually open an web browser and navigate to: {auth.AuthorizationURL}\n");
            }

            Console.WriteLine("After logged in, the page will provide 7-character PIN numbers.");
            do {
                Console.Write("Please enter the correct PIN numbers here: ");
                pinNumber = Console.ReadLine();
            } while(!(pinNumber.Length == 7 && int.TryParse(pinNumber, out _)));

            Console.WriteLine("\nGetting credential...\n");

            ITwitterCredentials credential = AuthFlow.CreateCredentialsFromVerifierCode(pinNumber, auth);

            if(credential != null && !string.IsNullOrEmpty(credential.AccessToken) && !string.IsNullOrEmpty(credential.AccessTokenSecret)) {
                Console.WriteLine("Got credential successfully");

                return credential;
            } else {
                Console.WriteLine("Null credential. Something's wrong while getting credential.");

                return null;
            }
        }

        private static bool OpenUrl(string url) {
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
