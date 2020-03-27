using System;
using System.Collections.Generic;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;

namespace SDSK.Twitter.ConsoleTools.Command.User {
    class UserCommand : CommandCommon {
        public override string CommandHelpDescription {
            get {
                return "Gets a user information" +
                       (Statics.IsReplMode ? "\nOn REPL mode, this command without any parameter gets the authenticated user information" : "");
            }
        }

        public override List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)>() {
            ("screenname",
             "User screen name (Twitter ID starts with '@' mark)",
             true)
        };

        public override void DoCommand(params string[] args) {
            IUser user;

            try {
                if(args != null && args.Length == 1 && args[0] != null) {
                    user = Tweetinvi.User.GetUserFromScreenName(args[0].Replace("@", "").Trim());
                } else {
                    if(Statics.IsReplMode) {
                        user = Tweetinvi.User.GetAuthenticatedUser();
                    } else {
                        Console.WriteLine("No parameters or more than 1 parameter has given. Check help description how to use this command.");

                        return;
                    }
                }
            } catch(TwitterNullCredentialsException) {
                Console.WriteLine(Statics.STR_required_auth);

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
