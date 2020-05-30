using System;
using System.Linq;

namespace repopractise.Helpers.Utils
{
    public class Utils : IUtils
    {
        public Utils() {}
        public string GenerateAccountNumber(int length) {
            const string chars = "0123456789";
            Random _random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public string GenerateTransactionReference()
        {
            const string chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789";
            Random _random = new Random();
            return new string(Enumerable.Repeat(chars, 15)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}