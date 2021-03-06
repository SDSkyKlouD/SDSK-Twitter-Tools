﻿using SDSK.Twitter.ConsoleTools.Command;
using SDSK.Twitter.ConsoleTools.Command.AuthToken;
using SDSK.Twitter.ConsoleTools.Command.Help;
using SDSK.Twitter.ConsoleTools.Command.ReplRedirect;
using SDSK.Twitter.ConsoleTools.REPL.Command.Auth;
using SDSK.Twitter.ConsoleTools.REPL.Command.CheckAuth;
using SDSK.Twitter.ConsoleTools.REPL.Command.User;

namespace SDSK.Twitter.ConsoleTools {
    public class ProgramMain {
        public static void Main(string[] args) {
            CommandProcessor.RegisterCommand("help", new HelpCommand());
            CommandProcessor.RegisterCommand("repl", new ReplRedirectCommand());
            CommandProcessor.RegisterCommand("authtoken", new AuthTokenCommand());

            CommandProcessor.RegisterReplOnlyCommand("auth", new AuthCommand());
            CommandProcessor.RegisterReplOnlyCommand("checkauth", new CheckAuthCommand());
            CommandProcessor.RegisterReplOnlyCommand("user", new UserCommand());

            CommandProcessor.Process(args);
        }
    }
}
