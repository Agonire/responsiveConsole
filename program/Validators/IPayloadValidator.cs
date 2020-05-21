namespace program.Validators
{
    public interface IPayloadValidator: IRegexValidator
    {
        public CommandEnum CommandType { get; }
    }
}