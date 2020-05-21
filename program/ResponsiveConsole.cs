using System;
using System.Text;
using System.Text.RegularExpressions;
using program.Validators;

namespace program
{
    public static class ResponsiveConsole
    {
        public static string ReadKeys(Regex correctFormat)
        {
            var allowedChars = new Regex(@"[\x20-\x7E]");

            var inputStream = new StringBuilder();

            while (!correctFormat.Match(inputStream.ToString()).Success)
            {
                var inputChar = Console.ReadKey(true).KeyChar;
                
                // Partial input possibility
                if (inputChar == '\r')
                    break;

                if (allowedChars.Match(inputChar.ToString()).Success)
                    inputStream.Append(inputChar);
                else if (inputChar == '\b' && inputStream.Length > 0)
                    inputStream.Length--;

                Console.WriteLine(inputStream.ToString());
            }
            return inputStream.ToString();
        }
    }
}