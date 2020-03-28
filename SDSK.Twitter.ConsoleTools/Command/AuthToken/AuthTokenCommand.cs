using SDSK.Twitter.ConsoleTools.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SDSK.Twitter.ConsoleTools.Command.AuthToken {
    class AuthTokenCommand : CommandCommon {
        public override string CommandHelpDescription { get; } = "Gets the user auth token of Twitter app using PIN authentication method\n" +
                                                                 "THIS SHOULD BE USED AND INTENDED FOR TEST/TEMPORARY USE PURPOSE ONLY";
        public override List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)> {
            ("consumer_key", "Consumer key of Twitter app", false),
            ("consumer_secret", "Consumer secret key of Twitter app", false),
        };

        public override void DoCommand(params string[] args) {
            if(args.Length == 2) {
                // Check-up
                var consumerKey = args[0].ToSecureString();
                var consumerSecret = args[1].ToSecureString();

                var cred = TweetinviUtil.AuthorizeAndGetCredentials(consumerKey, consumerSecret);

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
