using SDSK.Twitter.ConsoleTools.Command;
using System;
using System.Collections.Generic;

namespace SDSK.Twitter.ConsoleTools.REPL.Command.CheckAuth {
    class CheckAuthCommand : CommandCommon {
        public override string CommandHelpDescription { get; } = "Show current Twitter authentication status";
        public override List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)>() {
            ("reveal_hidings",
             "Reveal hidden characters that can harm your privacy. '1' to reveal, '0' or no parameter to hide.",
             true)
        };

        public override void DoCommand(params string[] args) {
            bool revealHidings = (args != null && args.Length == 1 && args[0] != null)
                                 && (args[0].Trim() == "1" || args[0].Trim() == "true" || args[0].Trim() == "yes");

            try {
                Console.WriteLine("Consumer Key : "        + (revealHidings ? Tweetinvi.Auth.ApplicationCredentials.ConsumerKey       : HideString(Tweetinvi.Auth.ApplicationCredentials.ConsumerKey)));
                Console.WriteLine("Consumer Secret Key : " + (revealHidings ? Tweetinvi.Auth.ApplicationCredentials.ConsumerSecret    : HideString(Tweetinvi.Auth.ApplicationCredentials.ConsumerSecret)));
                Console.WriteLine("Access Token : "        + (revealHidings ? Tweetinvi.Auth.ApplicationCredentials.AccessToken       : HideString(Tweetinvi.Auth.ApplicationCredentials.AccessToken)));
                Console.WriteLine("Access Token Secret : " + (revealHidings ? Tweetinvi.Auth.ApplicationCredentials.AccessTokenSecret : HideString(Tweetinvi.Auth.ApplicationCredentials.AccessTokenSecret)));
            } catch(NullReferenceException) {
                Console.WriteLine(Statics.STR_AuthRequired);
            }
}

        private string HideString(string value, string hideChar = "*", int revealCharCount = 5) {
            string output = value.Substring(0, revealCharCount);
            for(; revealCharCount > 0; revealCharCount--) {
                output += hideChar;
            }

            return output;
        }
    }
}
