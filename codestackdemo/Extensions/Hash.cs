using codestackdemo.Extensions.Exceptions;
using CryptoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codestackdemo.Extensions
{
    public class Hash
    {
        const string salt = "kjh^2bnsd67";
        public static string HashPassword(string password) => Crypto.HashPassword(password + salt);
        public static bool VerifyPassword(string hash, string password) => Crypto.VerifyHashedPassword(hash, password.Trim());

        public static string GetValue(string item)
        {
            for(var i = 1; i < 1000; i++)
            {
                if (VerifyPassword(item, i.ToString()))
                    return i.ToString();
            }
            throw new ExceptionModel(500, "Can't get value");
        }
    }
}
