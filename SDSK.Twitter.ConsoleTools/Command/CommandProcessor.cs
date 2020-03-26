using System;
using System.Collections.Generic;
using System.Linq;

namespace SDSK.Twitter.ConsoleTools.Command {
    public static class CommandProcessor {
        private static readonly Dictionary<string, ICommand> _commandDictionary;
        private static readonly Dictionary<string, ICommand> _commandReplOnlyDictionary;

        static CommandProcessor() {
            _commandDictionary = new Dictionary<string, ICommand>();
            _commandReplOnlyDictionary = new Dictionary<string, ICommand>();
        }

        public static void RegisterCommand(string commandName, ICommand commandObject)
            => _commandDictionary.Add(commandName.ToLower(), commandObject);

        public static void RegisterReplOnlyCommand(string commandName, ICommand commandObject)
            => _commandReplOnlyDictionary.Add(commandName.ToLower(), commandObject);

        public static Dictionary<string, ICommand> GetAllRegisteredCommands()
            => new Dictionary<string, ICommand>(_commandDictionary);

        public static Dictionary<string, ICommand> GetAllRegisteredReplOnlyCommands()
            => new Dictionary<string, ICommand>(_commandReplOnlyDictionary);

        public static ICommand GetSpecificCommandObject(string command) {
            if(_commandDictionary.ContainsKey(command.ToLower())) {
                return _commandDictionary[command.ToLower()];
            } else {
                return null;
            }
        }

        public static ICommand GetSpecificReplOnlyCommandObject(string command) {
            if(_commandReplOnlyDictionary.ContainsKey(command.ToLower())) {
                return _commandReplOnlyDictionary[command.ToLower()];
            } else {
                return null;
            }
        }

        public static bool Process(params string[] commands) {
            if(commands != null && commands.Length >= 1) {
                string mainCommandName = commands[0].Trim().ToLower();
                var command = GetSpecificCommandObject(mainCommandName);

                if(command != null) {
                    command.DoCommand(commands.ToList().GetRange(1, commands.Length - 1).ToArray());

                    return true;
                } else {
                    // Trying REPL only commands if the program is in REPL mode
                    if(Statics.IsReplMode) {
                        var replOnlyCommand = GetSpecificReplOnlyCommandObject(mainCommandName);

                        if(replOnlyCommand != null) {
                            replOnlyCommand.DoCommand(commands.ToList().GetRange(1, commands.Length - 1).ToArray());

                            return true;
                        }
                    } else {
                        Console.WriteLine($"Command '{mainCommandName}' is REPL mode only command.");
                    }

                    return false;
                }
            } else {
                if(Statics.IsReplMode) {
                    return false;
                } else {
                    REPL.REPLProcessor.StartRepl();

                    return true;
                }
            }
        }
    }
}
