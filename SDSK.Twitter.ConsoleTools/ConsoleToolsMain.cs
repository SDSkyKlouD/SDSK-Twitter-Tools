using SDSK.Twitter.ConsoleTools.Command;
using SDSK.Twitter.ConsoleTools.Command.AuthToken;
using SDSK.Twitter.ConsoleTools.Command.Help;

namespace SDSK.Twitter.ConsoleTools {
    public class ConsoleToolsMain {
        public static void Main(string[] args) {
            CommandProcessor.RegisterCommand("help", new TopHelpCommand());
            CommandProcessor.RegisterCommand("authtoken", new AuthTokenCommand());

            CommandProcessor.Process(args);
        }
    }
}
