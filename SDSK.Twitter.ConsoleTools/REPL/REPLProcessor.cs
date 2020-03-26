using SDSK.Twitter.ConsoleTools.Command;
using System;

namespace SDSK.Twitter.ConsoleTools.REPL {
    public class REPLProcessor {
        public static void StartRepl() {
            Statics.IsReplMode = true;

            Console.WriteLine("  To exit REPL mode, type 'exit' or press Ctrl-C.\n");

            while(true) {
                Console.Write("SDSK Twitter Console Tools > ");
                string command = Console.ReadLine();

                if(command.Split()[0].ToLower() == "exit" || command.Split()[0].ToLower() == "quit") {
                    Console.WriteLine("Bye ;)");
                    Environment.Exit(0);
                    break;
                } else {
                    if(command.Trim().Length <= 0) continue;

                    if(!CommandProcessor.Process(command.Split())) {
                        Console.WriteLine("No such command found. Type 'help' to see the command list.\n");
                    }
                }
            }
        }
    }
}
