using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    public static class ResponsiveConsole
    {
        public static string ReadKeys(Regex correctFormat)
        {
            var allowedChars = new Regex(@"[\x20-\x7E]");

            var stringBuilder = new StringBuilder();

            while (!Validator.Validate(stringBuilder.ToString(), correctFormat))
            {
                var inputChar = Console.ReadKey(true).KeyChar;
                
                // Partial input possibility
                if (inputChar == '\r')
                    break;

                if (Validator.Validate(inputChar.ToString(), allowedChars))
                    stringBuilder.Append(inputChar);
                else if (inputChar == '\b' && stringBuilder.Length > 0)
                    stringBuilder.Length--;

                Console.WriteLine(stringBuilder.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}