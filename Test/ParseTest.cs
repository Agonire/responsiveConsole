using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Program.Commands;
using Program.Data;
using Program.Parsers;
using Xunit;

namespace Test
{
    public class ParseTest
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
        public static IEnumerable<object[]> GetValidSoundQualities =>
            new List<object[]>
            {
                new object[]{"1000,1"},
                new object[]{"1000,2"},
            };
        public static IEnumerable<object[]> GetInvalidSoundQualities =>
            new List<object[]>
            {
                new object[]{"100, Not a number"},
                new object[]{"Not a number, 100"},
                new object[]{"Not a number, Not a number"},
            };

        [Theory]
        [MemberData(nameof(GetValidSoundQualities))]
        public void ParsingValidSoundQualities(string parameters)
        {
            Parse.SoundQualities(parameters).Should().BeOfType(typeof(SoundQualities));
        }

        [Theory]
        [MemberData(nameof(GetInvalidSoundQualities))]
        public void ParsingInvalidSoundQualitiesThrowsException(string parameters)
        {
            Action act = () => Parse.SoundQualities(parameters);

            act.Should().Throw<Exception>();
        }

        [Theory]
        [MemberData(nameof(GetValidPackets))]
        public void ParsingValidPackets(string parameters)
        {
            Parse.Packet(parameters).Should().BeOfType(typeof(Packet));
        }

        [Theory]
        [MemberData(nameof(GetInvalidPackets))]
        public void ParsingInvalidPacketsThrowsException(string parameters)
        {
            Action act = () => Parse.Packet(parameters);

            act.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("100000000000000000000000,1")]
        [InlineData("1, 10000000000000000000000")]
        public void SoundQualitiesWithBigNumbersThrowsOverflowException(string parameters)
        {
            Action act = () => Parse.SoundQualities(parameters);

            act.Should().Throw<OverflowException>();
        }

        [Fact]
        public static void ParsingPacketReturnsNullWhenInputIsNull()
        {
            Parse.Packet(null).Should().BeNull();
        }

        [Fact]
        public static void ParsingSoundQualitiesReturnsNullWhenInputIsNull()
        {
            Parse.SoundQualities(null).Should().BeNull();
        }
    }
}