using System;
using System.Collections.Generic;

namespace SDSK.Twitter.ConsoleTools.Command {
    public interface ICommand {
        public string CommandHelpDescription { get; }

        public List<(string, string, bool)> CommandOptions { get; }

        public void DoCommand(params string[] args);

        public void PrintHelpMessage();
    }

    public abstract class CommandCommon : ICommand {
        public abstract string CommandHelpDescription { get; }

        public abstract List<(string, string, bool)> CommandOptions { get; }

        public abstract void DoCommand(params string[] args);

        public void PrintHelpMessage() {
            if(CommandOptions != null) {
                foreach(var (optionName, _, optionCanIgnore) in CommandOptions!) {
                    if(optionCanIgnore) {
                        Console.Write($" [{optionName}]");
                    } else {
                        Console.Write($" {{{optionName}}}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($" {CommandHelpDescription}\n");

            if(CommandOptions != null) {
                foreach(var (optionName, optionDesc, optionCanIgnore) in CommandOptions!) {
                    if(optionCanIgnore) {
                        Console.WriteLine($"    └ [{optionName}]");
                    } else {
                        Console.WriteLine($"    └ {{{optionName}}}");
                    }

                    Console.WriteLine($"      {optionDesc}\n");
                }
            }
        }
    }
}
