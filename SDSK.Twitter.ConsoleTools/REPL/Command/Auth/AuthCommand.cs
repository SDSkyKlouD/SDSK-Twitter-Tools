using SDSK.Twitter.ConsoleTools.Command;
using SDSK.Twitter.ConsoleTools.Util;
using System;
using System.Collections.Generic;

namespace SDSK.Twitter.ConsoleTools.REPL.Command.Auth {
    class AuthCommand : CommandCommon {
        public override string CommandHelpDescription { get; } = "Authenticate Twitter access with a Twitter application token for further command usage";
        public override List<(string, string, bool)> CommandOptions { get; } = null;

        public override void DoCommand(params string[] args) {
            if(Tweetinvi.Auth.Credentials != null) {
                Console.WriteLine("A credential already set up. Are you sure authenticate again?");
                Console.Write("Type 'Y' to continue authentication, otherwise will stop this command. : ");

                var sel = Console.ReadKey();
                if(!(sel.Key.ToString().ToLower() == "y")) {
                    Console.WriteLine("\nAbort.");

                    return;
                }

                Console.WriteLine();
            }

            var consumerKeyResult    = ConsoleUtil.ReadSecureLine("Enter application consumer key        : ");
            var consumerSecretResult = ConsoleUtil.ReadSecureLine("Enter application consumer secret key : ");

            var credential = TweetinviUtil.AuthorizeAndGetCredentials(consumerKeyResult.ResultString, consumerSecretResult.ResultString);
            if(credential == null) {
                Console.WriteLine("\nSeems like the credential had not successfully created. Try again later.\n");

                return;
            }

            var tempCredUser = Tweetinvi.User.GetAuthenticatedUser(credential);

            Console.WriteLine();
            Console.WriteLine( " According to authenticated user information...");
            Console.WriteLine($"  * Screen name (Twitter ID) : @{tempCredUser.ScreenName}");
            Console.WriteLine($"  * Nickname                 : {tempCredUser.Name}");

            Console.WriteLine("\nIf the information is right, Type 'Y' or press Enter to continue.");

            var selection = Console.ReadKey();
            if(selection.Key == ConsoleKey.Enter || selection.KeyChar.ToString().ToLower() == "y") {
                Console.WriteLine("\nThis credential will be used in further commands.");

                Tweetinvi.Auth.SetCredentials(credential);
                Tweetinvi.Auth.ApplicationCredentials = credential;
            } else {
                Console.WriteLine("\nCancelled.");

                return;
            }
        }
    }
}
