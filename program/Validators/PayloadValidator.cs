using System.Text.RegularExpressions;

namespace program.Validators
{
    public class PayloadValidator: IPayloadValidator
    {
        public CommandEnum CommandType { get; }
        public Regex Regex { get; set; }

        public PayloadValidator(CommandEnum type, Regex regex)
        {
            CommandType = type;
            Regex = regex;
        }

        public bool Validate(string data)
        {
            return Regex.Match(data).Success;
        }

    }
}