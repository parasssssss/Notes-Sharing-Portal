using System;
using System.Security.Cryptography;
using System.Text;

namespace NotesSharingWebForms.Helpers
{
    public static class SecurityHelper
    {
        // Hash using SHA256 and return Base64 (to match your DB)
        public static string HashPassword(string plain)
        {
            if (plain == null) plain = string.Empty;
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyPassword(string plain, string storedHashBase64)
        {
            return HashPassword(plain) == (storedHashBase64 ?? "");
        }
    }
}
