using System;

namespace KMChartsUpdater.BLL.Utils
{
    public class RandomString
    {
        private static readonly Random _random = new Random();

        public static string Generate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var salt = new char[length];

            for (int i = 0; i < length; i++)
            {
                salt[i] = chars[_random.Next(chars.Length)];
            }

            return new string(salt);
        }
    }
}
