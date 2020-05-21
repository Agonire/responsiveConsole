using System;
using System.Collections.Generic;
using FluentAssertions;
using program.Data;
using program.Parsers;
using Xunit;

namespace Test.Parsers
{
    public class PacketParserTest
    {
        public static IEnumerable<object[]> GetValidPackets =>
            new List<object[]>
            {
                new object[] {"PS:params:E"},
                new object[] {":params:"},
                new object[] {"params:"},
                new object[] {":"},
            };
        public static IEnumerable<object[]> GetInvalidPackets =>
            new List<object[]>
            {
                new object[]{"100000000000000000000000,1"},
                new object[]{"100, Not a number"},
            };
        [Theory]
        [MemberData(nameof(GetValidPackets))]
        public void ParsingValidPackets(string parameters)
        {
            var parser = new PacketParser();
            parser.Parse(parameters).Should().BeOfType(typeof(Packet));
        }

        [Theory]
        [MemberData(nameof(GetInvalidPackets))]
        public void ParsingInvalidPacketsThrowsException(string parameters)
        {
            var parser = new PacketParser();
            Action act = () => parser.Parse(parameters);

            act.Should().Throw<Exception>();
        }
        [Fact]
        public static void ParsingPacketReturnsNullWhenInputIsNull()
        {
            var parser = new PacketParser();
            parser.Parse(null).Should().BeNull();
        }

        
    }
}