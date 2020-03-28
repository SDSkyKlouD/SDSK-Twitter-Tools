using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace SDSK.Twitter.ConsoleTools.Util {
    public static class SecureStringExtensions {
        public static SecureString ToSecureString(this string value, bool readOnly = true) {
            if(string.IsNullOrEmpty(value)) {
                return new SecureString();
            }

            SecureString secureString = new SecureString();
            value.ToList().ForEach(secureString.AppendChar);

            if(readOnly) {
                secureString.MakeReadOnly();
            }

            return secureString;
        }


        public static string AsString(this SecureString value) {
            if(value == null)
                return null;

            IntPtr ptr = IntPtr.Zero;
            try {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(ptr);
            } catch(ArgumentNullException) {
                Console.WriteLine("[Error] Null SecureString object");

                return null;
            } catch(OutOfMemoryException) {
                Console.WriteLine("[FATAL] Out of memory while converting a SecureString object to string! Terminating the program...");

                Environment.Exit(137);      // out of memory
                return null;
            } finally {
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }
    }
}
