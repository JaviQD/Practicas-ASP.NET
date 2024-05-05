using System.Security.Cryptography;

namespace Practicas_ASP.NET.Methods
{
    public class RandomKey
    {
        public string GenerarRandomKey(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randomBytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = validChars[randomBytes[i] % validChars.Length];
            }

            return new string(result);
        }
    }
}
