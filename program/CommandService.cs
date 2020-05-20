using System;
using System.Text.RegularExpressions;
using Program.Commands;
using Program.CustomExceptions;
using Program.Data;
using Program.Parsers;

namespace Program
{
    public static class CommandService
    {
        public static ICommand CreateCommand(Packet packet)
        {
            var commandType = "";
            if (packet.Command.Length > 0)
            {
                commandType = packet.Command.Remove(0, 1);
            }

            return commandType switch
            {
                "T" => CreateTextCommand(packet),
                "S" => CreateSoundCommand(packet),
                _ => throw new InvalidPayloadException()
            };
        }

        private static ICommand CreateTextCommand(Packet packet)
        {
            if(!Validator.ValidateTextCommand(packet.Parameters))
                throw new InvalidPayloadException();

            return new TextCommand(Parse.Text(packet.Parameters));
        }

        private static ICommand CreateSoundCommand(Packet packet)
        {
            if(!Validator.ValidateSoundCommand(packet.Parameters))
                throw new InvalidPayloadException();

            return new SoundCommand(Parse.SoundQualities(packet.Parameters));
        }
    }
}