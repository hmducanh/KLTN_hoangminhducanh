using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace Helper
{
    public class Common
    {
        public Common() { }

        public static string ToMD5(string input)
        {
            string result = "";
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            for(int i = 0; i < buffer.Length; i++)
            {
                result += buffer[i].ToString("x2");  
            }
            return result;
        }
    }
}
