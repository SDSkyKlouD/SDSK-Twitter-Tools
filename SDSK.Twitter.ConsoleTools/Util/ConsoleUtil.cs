using System;
using System.Security;
using System.Text;

namespace SDSK.Twitter.ConsoleTools.Util {
    public static class ConsoleUtil {
        public class ReadSecureLineResult {
            public SecureString ResultString { get; private set; }
            public bool Cancelled { get; private set; }

            public ReadSecureLineResult(SecureString result, bool cancelled = false) {
                ResultString = result;
                Cancelled = cancelled;
            }

            ~ReadSecureLineResult() {
                ResultString.Dispose();
            }
        }

        public static ReadSecureLineResult ReadSecureLine(string preceding = null) {
            StringBuilder builder = new StringBuilder();
            bool cancel = false;

            if(!string.IsNullOrEmpty(preceding)) {
                Console.Write($"{preceding.TrimEnd()} ");
            }

            while(true) {
                var key = Console.ReadKey(true);

                if(key.Key == ConsoleKey.Enter) break;
                if(key.Key == ConsoleKey.Escape) {
                    cancel = true;
                    break;
                }
                if(key.Key == ConsoleKey.Backspace && builder.Length > 0) builder.Remove(builder.Length - 1, 1);
                if(key.Key != ConsoleKey.Backspace) builder.Append(key.KeyChar);
            }

            Console.WriteLine();

            return new ReadSecureLineResult(builder.ToString().ToSecureString(), cancel);
        }
    }
}
