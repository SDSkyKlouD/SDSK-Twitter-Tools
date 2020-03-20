using SDSK.Twitter.ConsoleTools.Command;
using SDSK.Twitter.ConsoleTools.Command.AuthToken;
using SDSK.Twitter.ConsoleTools.Command.Help;
using SDSK.Twitter.ConsoleTools.Command.ReplRedirect;
using SDSK.Twitter.ConsoleTools.REPL.Command.CheckAuth;

namespace SDSK.Twitter.ConsoleTools {
    public class ConsoleToolsMain {
        public static void Main(string[] args) {
            CommandProcessor.RegisterCommand("help", new HelpCommand());
            CommandProcessor.RegisterCommand("repl", new ReplRedirectCommand());
            CommandProcessor.RegisterCommand("authtoken", new AuthTokenCommand());

            CommandProcessor.RegisterReplOnlyCommand("checkauth", new CheckAuthCommand());

            CommandProcessor.Process(args);
        }
    }
}
