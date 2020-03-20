using System;
using System.Collections.Generic;

namespace SDSK.Twitter.ConsoleTools.Command.ReplRedirect {
    class ReplRedirectCommand : ICommand {
        public string CommandHelpDescription { get; } = "Run the program in REPL mode.\n" +
                                                        "This also can be done by running the program without any parameters.";
        public List<(string, string, bool)> CommandOptions => null;

        public void DoCommand(params string[] args) {
            if(Statics.IsReplMode) {
                Console.WriteLine(" You are already in REPL mode!\n");
            } else {
                REPL.REPLProcessor.StartRepl();
            }
        }
    }
}
