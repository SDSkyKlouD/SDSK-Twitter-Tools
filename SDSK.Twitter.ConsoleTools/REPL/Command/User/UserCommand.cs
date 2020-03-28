using SDSK.Twitter.ConsoleTools.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;

namespace SDSK.Twitter.ConsoleTools.REPL.Command.User {
    class UserCommand : CommandCommon {
        public override string CommandHelpDescription { get; } = "Gets an authenticated or specified user information";

        public override List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)>() {
            ("screenname",
             "Specific user's screen name (Twitter ID starts with '@' mark)",
             true)
        };

        public override void DoCommand(params string[] args) {
            string screenname = null;
            IUser user, me;

            try {
                if(args != null && args.Length == 1 && args[0] != null) {
                    screenname = args[0].Replace("@", "").Trim();
                    user = Tweetinvi.User.GetUserFromScreenName(screenname);
                } else {
                    user = Tweetinvi.User.GetAuthenticatedUser();
                }

                me = Tweetinvi.User.GetAuthenticatedUser();
            } catch(TwitterNullCredentialsException) {
                Console.WriteLine(Statics.STR_AuthRequired);

                return;
            }

            // Null = no account found
            if(user == null) {
                Console.WriteLine($"User '{screenname}' not found.");

                return;
            }

            Console.WriteLine();
            Console.WriteLine(" *       Account numeric ID : " + user.IdStr);
            Console.WriteLine(" *     Account created date : " + user.CreatedAt.ToString());
            Console.WriteLine(" *         Account Timezone : " + user.TimeZone);
            Console.WriteLine();
            Console.WriteLine(" *                 Nickname : " + user.Name);
            Console.WriteLine(" * Screen name (Twitter ID) : @" + user.ScreenName);
            Console.WriteLine(" *        Profile image URL : " + user.ProfileImageUrlFullSize);
            Console.WriteLine(" *       Profile banner URL : " + user.ProfileBannerURL);
            Console.WriteLine();
            Console.WriteLine(" *            Is protected? : " + (user.Protected ? "Yes" : "No"));
            Console.WriteLine(" *         User description : " + user.Description);
            Console.WriteLine(" *            User location : " + user.Location);
            Console.WriteLine(" *            User homepage : " + user.Url);
            Console.WriteLine();
            Console.WriteLine(" *      Verified by Twitter : " + (user.Verified ? "Yes" : "No"));
            Console.WriteLine(" *                Following : " + user.FriendsCount);
            Console.WriteLine(" *                Followers : " + user.FollowersCount);
            Console.WriteLine();
            Console.WriteLine(" *              Tweet count : " + user.StatusesCount);
            Console.WriteLine(" *              Likes count : " + user.FavouritesCount);

            if(user.Id != me.Id) {
                var relationship = user.GetRelationshipWith(me);

                Console.WriteLine();
                Console.WriteLine(" *          Am I following? : " + (user.Following ? "Yes" : "No"));
                Console.WriteLine(" *         Is following me? : " + (relationship.Following ? "Yes" : "No"));
                Console.WriteLine(" *          Can send me DM? : " + (relationship.CanSendDirectMessage ? "Yes" : "No"));
            }
        }
    }
}
