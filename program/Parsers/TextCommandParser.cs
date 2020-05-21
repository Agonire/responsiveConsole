using program.Commands;

namespace program.Parsers
{
    public class TextCommandParser: ICommandParser
    {
        public CommandEnum CommandType { get; } = CommandEnum.TextCommand;

        public ICommand Parse(string parameters)
        {
            return new TextCommand(parameters);
        }
    }
}