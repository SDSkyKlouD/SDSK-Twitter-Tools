using SDSK.Twitter.ConsoleTools.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDSK.Twitter.ConsoleTools.REPL.Command.Auth {
    class AuthCommand : CommandCommon {
        public override string CommandHelpDescription { get; } = "Authenticate Twitter access with a Twitter application token for further command usage";

        public override List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)>() {
            ("consumer_key", "Consumer key of Twitter app", false),
            ("consumer_secret", "Consumer secret key of Twitter app", false),
        };

        public override void DoCommand(params string[] args) {

        }
    }
}
