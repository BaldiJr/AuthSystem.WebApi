using System.Security.Cryptography;
using System.Text;

namespace AuhtSystem.Business.Util
{
    public static class Encryptor
    {
        public static string EncryptPass(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x6"));
            }

            return strBuilder.ToString();
        }
    }
}
