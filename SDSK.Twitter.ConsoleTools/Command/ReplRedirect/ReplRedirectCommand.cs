using System;
using System.Collections.Generic;

namespace SDSK.Twitter.ConsoleTools.Command.ReplRedirect {
    class ReplRedirectCommand : CommandCommon {
        public override string CommandHelpDescription { get; } = "Run the program in REPL mode.\n" +
                                                                 "This also can be done by running the program without any parameters.";
        public override List<(string, string, bool)> CommandOptions => null;

        public override void DoCommand(params string[] args) {
            if(Statics.IsReplMode) {
                Console.WriteLine(" You are already in REPL mode!\n");
            } else {
                REPL.REPLProcessor.StartRepl();
            }
        }
    }
}
