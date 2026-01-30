using System.Text.RegularExpressions;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Validate input data class
    /// </summary>
    public class ValidateData
    {
        //string phoneNumber = "123-456-7890";
        //string pattern = @"^\d{3}-\d{3}-\d{4}$"; // Шаблон для номера телефона в формате XXX-XXX-XXXX

        /// <summary>
        /// Function to validate input string
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="length"></param>
        /// <returns>bool</returns>
        static public bool ValidateString(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length < length) return false;
            return true;
        }
        /// <summary>
        /// Function to trim string
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>string</returns>
        static public string TrimInputString(string inputString)
        {
            return inputString.Trim();
        }
        /// <summary>
        /// Function to validate password
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="length"></param>
        /// <returns>bool</returns>
        static public bool ValidatePassword(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length < length) return false;
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{5,}$";
            return Regex.IsMatch(inputString, pattern);
        }
        /// <summary>
        /// Function to validate login
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="length"></param>
        /// <returns>bool</returns>
        static public bool ValidateLogin(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length < length) return false;
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_]{5,25}$";
            return Regex.IsMatch(inputString, pattern);
        }
        /// <summary>
        /// Function to validate email
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="length"></param>
        /// <returns>bool</returns>
        static public bool ValidateEmail(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length < length) return false;
            string pattern = @"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$";
            return Regex.IsMatch(inputString, pattern);
        }
        /// <summary>
        /// Function to validate phone number by length
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        static public bool ValidatePhone(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            string pattern = @"^\d{11}$";
            return Regex.IsMatch(inputString, pattern);
        }
    }
}

