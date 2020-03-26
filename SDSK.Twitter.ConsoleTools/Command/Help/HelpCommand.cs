using System;
using System.Collections.Generic;

namespace SDSK.Twitter.ConsoleTools.Command.Help {
    class HelpCommand : ICommand {
        public string CommandHelpDescription { get; } = "Shows this help message.";
        public List<(string, string, bool)> CommandOptions { get; } = new List<(string, string, bool)> {
            ("specific_command", "Show help about the command, if specified", true)
        };

        protected string _executableFileName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        public void DoCommand(params string[] args) {
            Console.WriteLine("【SDSK Twitter Tools】\n" +
                              "  - A console utility program that plays with Twitter.\n" +
                              "  - Made only for SD SkyKlouD. Any issues will not be reviewed.\n" +
                              "  - SD SkyKlouD is not responsible to any loss/damage of your data.\n" +
                              "  - Source code is available at https://github.com/SDSkyKlouD/SDSK-Twitter-Tools \n" +
                              "  - ⓒ SD SkyKlouD\n\n" +
                              " { } means required parameter, and [ ] means optional parameter.\n\n");

            if(args.Length == 1) {
                var command = CommandProcessor.GetSpecificCommandObject(args[0]);

                if(command != null) {
                    // Show specific command help message and exit
                    PrintCommandHelpMessage(args[0], command.CommandHelpDescription, command.CommandOptions);

                    return;
                } else {
                    if(Statics.IsReplMode) {
                        var replOnlyCommand = CommandProcessor.GetSpecificReplOnlyCommandObject(args[0]);

                        if(replOnlyCommand != null) {
                            PrintCommandHelpMessage(args[0], replOnlyCommand.CommandHelpDescription, replOnlyCommand.CommandOptions);

                            return;
                        }
                    } else {
                        // No command found. Show error message and continue to show full help messages
                        Console.WriteLine($"Command '{args[0]}' not found.\n");
                    }
                }
            }

            // Show full help messages
            var commands = CommandProcessor.GetAllRegisteredCommands();

            foreach(string command in commands.Keys) {
                var commandObject = commands[command];
                PrintCommandHelpMessage(command, commandObject.CommandHelpDescription, commandObject.CommandOptions);
            }

            if(Statics.IsReplMode) {
                var replOnlyCommands = CommandProcessor.GetAllRegisteredReplOnlyCommands();

                foreach(string command in replOnlyCommands.Keys) {
                    var commandObject = replOnlyCommands[command];
                    PrintCommandHelpMessage(command, commandObject.CommandHelpDescription, commandObject.CommandOptions);
                }

                Console.WriteLine("* To exit REPL mode, type 'exit' or press Ctrl-C.\n");
            }
        }

        protected void PrintCommandHelpMessage(string commandName,
                                               string commandHelpDesc,
                                               List<(string optionName,string optionDesc, bool optionCanIgnore)> optionObjects) {
            if(Statics.IsReplMode) {
                Console.Write($"== {commandName}");
            } else {
                Console.Write($"== {_executableFileName} {commandName}");
            }

            if(optionObjects != null) {
                foreach(var (optionName, _, optionCanIgnore) in optionObjects!) {
                    if(optionCanIgnore) {
                        Console.Write($" [{optionName}]");
                    } else {
                        Console.Write($" {{{optionName}}}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($" {commandHelpDesc}\n");

            if(optionObjects != null) {
                foreach(var (optionName, optionDesc, optionCanIgnore) in optionObjects!) {
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
