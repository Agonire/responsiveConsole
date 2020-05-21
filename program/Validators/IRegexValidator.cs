using System.Text.RegularExpressions;

namespace program.Validators
{
    public interface IRegexValidator: IValidator
    {
        public Regex Regex { get; set; }
    }
}