using SDSK.Twitter.ConsoleTools.Command;
using System;
using System.Collections.Generic;
using Tweetinvi;

namespace SDSK.Twitter.ConsoleTools.REPL.Command.CheckAuth {
    class CheckAuthCommand : ICommand {
        public string CommandHelpDescription { get; } = "Show current Twitter authentication status";
        public List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)>() {
            ("reveal_hidings",
             "Reveal hidden characters that can harm your privacy. '1' to reveal, '0' or no parameter to hide.",
             true)
        };

        public void DoCommand(params string[] args) {
            bool revealHidings = (args != null && args.Length == 1 && args[0] != null && args[0].Trim() == "1");

            try {
                Console.WriteLine("Consumer Key : " + (revealHidings ? Auth.ApplicationCredentials.ConsumerKey : HideString(Auth.ApplicationCredentials.ConsumerKey)));
                Console.WriteLine("Consumer Secret Key : " + (revealHidings ? Auth.ApplicationCredentials.ConsumerSecret : HideString(Auth.ApplicationCredentials.ConsumerSecret)));
                Console.WriteLine("Access Token : " + (revealHidings ? Auth.ApplicationCredentials.AccessToken : HideString(Auth.ApplicationCredentials.AccessToken)));
                Console.WriteLine("Access Token Secret : " + (revealHidings ? Auth.ApplicationCredentials.AccessTokenSecret : HideString(Auth.ApplicationCredentials.AccessTokenSecret)));

            } catch(NullReferenceException) {
                Console.WriteLine("Seems like an authentication has not been made. Please authenticate your Twitter access with your app keys by 'auth' command.");
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
