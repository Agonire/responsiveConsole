using System;
using program.Commands;
using program.Data;

namespace program.Parsers
{
    public class SoundCommandParser : ICommandParser
    {
        public CommandEnum CommandType { get; } = CommandEnum.SoundCommand;

        public ICommand Parse(string parameters)
        {
            if (string.IsNullOrEmpty(parameters))
                return null;

            var parts = parameters.Split(",");
            var soundQualities = new SoundQualities(
                Convert.ToInt32(parts[0]),
                Convert.ToInt32(parts[1]));

            return new SoundCommand(soundQualities);
        }
    }
}