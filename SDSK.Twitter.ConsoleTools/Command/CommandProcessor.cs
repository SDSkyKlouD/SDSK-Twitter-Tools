using SDSK.Twitter.ConsoleTools.Command.Help;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDSK.Twitter.ConsoleTools.Command {
    public static class CommandProcessor {
        private static readonly Dictionary<string, ICommand> _commandDictionary;

        static CommandProcessor() => _commandDictionary = new Dictionary<string, ICommand>();

        public static void RegisterCommand(string commandName, ICommand commandObject)
            => _commandDictionary.Add(commandName.ToLower(), commandObject);

        public static Dictionary<string, ICommand> GetAllRegisteredCommands()
            => new Dictionary<string, ICommand>(_commandDictionary);

        public static ICommand GetSpecificCommandObject(string command) {
            if(_commandDictionary.ContainsKey(command.ToLower())) {
                return _commandDictionary[command.ToLower()];
            } else {
                return null;
            }
        }

        public static void Process(params string[] commands) {
            if(commands != null && commands.Length >= 1) {
                string mainCommandName = commands[0].ToLower();
                var command = GetSpecificCommandObject(mainCommandName);

                if(command != null) {
                    command.DoCommand(commands.ToList().GetRange(1, commands.Length - 1).ToArray());

                    return;
                } else {
                    Console.WriteLine("Command not found.\n");
                }
            } else {
                Console.WriteLine("No command has given, or something went wrong with command processor.\n");
            }

            // Run main help command if something's wrong
            new HelpCommand().DoCommand();
        }
    }
}
