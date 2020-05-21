using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentAssertions;
using program;
using program.Commands;
using program.CustomExceptions;
using program.Data;
using program.Parsers;
using program.Validators;
using Xunit;

namespace Test
{
    public class CommandServiceTest
    {
        class BadParser: ICommandParser
        {
            public ICommand Parse(string parameters)
            {
                throw new Exception();
            }

            public CommandEnum CommandType { get; }
        }

        public static IEnumerable<object[]> GetWrongCommands =>
            new List<object[]>
            {
                new object[] {new Packet("Wrong Command", "Any Parameters")},
                new object[] {new Packet("1", "Any Parameters")},
                new object[] {new Packet("", "Any Parameters")},
            };

        public static IEnumerable<object[]> GetIncorrectParameters =>
            new List<object[]>
            {
                new object[]
                {
                    new Packet("", "Wrong Parameters"),
                    new Regex(@"[0-9]")
                },
                new object[]
                {
                    new Packet("", "010100101010"),
                    new Regex(@"[a-z]")
                }
            };
            public static IEnumerable<object[]> GetCorrectParameters =>
            new List<object[]>
            {
                new object[]
                {
                    new Packet("", "010100101010"),
                    new Regex(@"[0-9]")
                },
                new object[]
                {
                    new Packet("", "Wrong Parameters"),
                    new Regex(@"[a-z]")
                }
            };

        [Theory]
        [MemberData(nameof(GetWrongCommands))]
        public static void InvalidCommandThrowsException(Packet packet)
        {
            var commandService = new CommandService();

            commandService.DefaultPayloadValidators.AddRange(new List<IPayloadValidator>()
            {
                new PayloadValidator(CommandEnum.TextCommand, new Regex(@"")),
                new PayloadValidator(CommandEnum.SoundCommand, new Regex(@"[0-9],[0-9]")),
            });
            commandService.DefaultCommandParsers.AddRange(new List<ICommandParser>()
            {
                new TextCommandParser(),
                new SoundCommandParser(),
            });

            Action act = () => CommandService.IdentifyCommand(packet);

            act.Should().Throw<InvalidPayloadException>();
        }

        [Theory]
        [MemberData(nameof(GetIncorrectParameters))]
        public static void InvalidParametersReturnFalse(Packet packet, Regex regex)
        {
            var commandService = new CommandService();
            var validator = new PayloadValidator(CommandEnum.TextCommand, regex);

            commandService.ValidatePacket(packet, validator).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(GetCorrectParameters))]
        public static void ValidParametersReturnTrue(Packet packet, Regex regex)
        {
            var commandService = new CommandService();
            var validator = new PayloadValidator(CommandEnum.TextCommand, regex);

            commandService.ValidatePacket(packet, validator).Should().BeTrue();
        }

        [Fact]
        public static void BadParserThrowsException()
        {
            var badParser = new BadParser();
            
            var commandService = new CommandService();
            Action act = () => commandService.ParseCommand(new Packet("", ""), badParser);

            act.Should().Throw<Exception>();
        }


        [Fact]
        public static void NullValidatorThrowsException()
        {
            var commandService = new CommandService();
            Action act = () => commandService.ValidatePacket(new Packet("", ""), null);

            act.Should().Throw<NullReferenceException>();
        }
        [Fact]
        public static void NullParserThrowsException()
        {
            var commandService = new CommandService();
            Action act = () => commandService.ParseCommand(new Packet("", ""), null);

            act.Should().Throw<NullReferenceException>();
        }
    }
}