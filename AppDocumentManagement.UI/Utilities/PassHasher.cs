using System.Security.Cryptography;
using System.Text;


namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Password hasher class
    /// </summary>
    class PassHasher
    {
        /// <summary>
        /// Function to calculate hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string</returns>
        static public string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
