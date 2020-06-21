using System;
using System.Text;

namespace SourceControlAssignment.Helpers
{
    public static class Helpers
    {
        public static string Hash(string password)
        {
            string hash = Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(password))
                ).Replace('"', ' ');
            hash = hash.Replace('+', ' ');
            return hash;
        }
    }
}