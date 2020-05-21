using System;
using System.Collections.Generic;
using System.Linq;
using program.Commands;
using program.CustomExceptions;
using program.Data;
using program.Parsers;
using program.Validators;

namespace program
{
    public class CommandService
    {
        public List<ICommandParser> DefaultCommandParsers { get; set; } = new List<ICommandParser>();
        public List<IPayloadValidator> DefaultPayloadValidators { get; set; } = new List<IPayloadValidator>();

        public ICommand CreateCommand(Packet packet)
        {
            var commandType = IdentifyCommand(packet);

            var validator = DefaultPayloadValidators.First(p => p.CommandType == commandType);
            if (!ValidatePacket(packet, validator))
                throw new InvalidPayloadException();

            var parser = DefaultCommandParsers.First(p => p.CommandType == commandType);
            return ParseCommand(packet, parser);
        }

        public static CommandEnum IdentifyCommand(Packet packet)
        {
            var commandLetter = "";
            if (packet.Command.Length > 0)
                commandLetter = packet.Command.Remove(0, 1);

            return commandLetter switch
            {
                "T" => CommandEnum.TextCommand,
                "S" => CommandEnum.SoundCommand,
                _ => throw new InvalidPayloadException()
            };
        }

        public bool ValidatePacket(Packet packet, IPayloadValidator validator)
        {
            return validator.Validate(packet.Parameters);
        }

        public ICommand ParseCommand(Packet packet, ICommandParser parser)
        {
            try
            {
                return parser.Parse(packet.Parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Parser error");
                throw ex;
            }
        }
    }
}