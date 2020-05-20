using System.Text.RegularExpressions;

namespace Program
{
    public static class Validator
    {
        public static bool Validate(string data, Regex regex)
        {
            return regex.Match(data).Success;
        }

        public static bool ValidateRawInput(string data, string format = @"^[P][A-Z]{1}:[\x20-\x7E]*:[E]$")
        {
            return Validate(data, new Regex(format));
        }
        public static bool ValidateTextCommand(string data, string format = @"")
        {
            return Validate(data, new Regex(format));
        }

        public static bool ValidateSoundCommand(string data, string format = @"[0-9],[0-9]")
        {
            return Validate(data, new Regex(format));
        }
    }
}