using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Wpf_UserAccount.Converters
{
    public static class SecureStringHelper
    {
        public static SecureString ConvertToSecureString(string password)
        {
            if (password == null)
            {
                return null;
            }
            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            securePassword.MakeReadOnly();
            return securePassword;
        }

        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message: {ex.Message}");
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
            return Marshal.PtrToStringUni(unmanagedString);
        }

    }

}
