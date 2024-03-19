using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecaster.DataProtection
{
    internal static class DataEncryptor
    {
        private static readonly byte[] _entropy = [0, 1, 2, 3, 4, 5, 6];
        public static byte[] Encrypt(string data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            return ProtectedData.Protect(byteData, _entropy, DataProtectionScope.CurrentUser);
        }

        public static string Decrypt(byte[] data)
        {
            byte[] byteData = ProtectedData.Unprotect(data, _entropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(byteData);
        }
    }
}
