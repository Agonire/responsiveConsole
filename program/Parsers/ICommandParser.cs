using program.Commands;

namespace program.Parsers
{
    public interface ICommandParser: IParser<ICommand>
    {
        public CommandEnum CommandType { get; }
    }
}