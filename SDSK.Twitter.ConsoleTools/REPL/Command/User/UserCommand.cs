using SDSK.Twitter.ConsoleTools.Command;
using System;
using System.Collections.Generic;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;

namespace SDSK.Twitter.ConsoleTools.REPL.Command.User {
    class UserCommand : CommandCommon {
        public override string CommandHelpDescription {
            get {
                return "Gets an authenticated or specified user information";
            }
        }

        public override List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)>() {
            ("screenname",
             "Specific user's screen name (Twitter ID starts with '@' mark)",
             true)
        };

        public override void DoCommand(params string[] args) {
            IUser user;

            try {
                if(args != null && args.Length == 1 && args[0] != null) {
                    user = Tweetinvi.User.GetUserFromScreenName(args[0].Replace("@", "").Trim());
                } else {
                    user = Tweetinvi.User.GetAuthenticatedUser();
                }
            } catch(TwitterNullCredentialsException) {
                Console.WriteLine(Statics.STR_AuthRequired);

                return;
            }

            // Final null checking
            if(user == null) {
                Console.WriteLine("Null user object detected. Something's wrong. Terminating this command...");

                return;
            }

            Console.WriteLine();
            Console.WriteLine(" *                 Nickname : " + user.Name);
            Console.WriteLine(" * Screen name (account ID) : " + user.ScreenName);
            Console.WriteLine(" *        Profile image URL : " + user.ProfileImageUrlFullSize);
            Console.WriteLine(" *       Profile banner URL : " + user.ProfileBannerURL);
        }
    }
}
