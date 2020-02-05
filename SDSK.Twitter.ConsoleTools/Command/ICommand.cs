using System.Collections.Generic;

namespace SDSK.Twitter.ConsoleTools.Command {
    public interface ICommand {
        public string CommandHelpDescription { get; }
        // List<(OPTION_NAME, OPTION_DESCRIPTION, OPTION_CAN_IGNORE)>
        public List<(string, string, bool)> CommandOptions { get; }

        public void DoCommand(params string[] args);
    }
}
