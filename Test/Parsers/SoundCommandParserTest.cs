using System;
using System.Collections.Generic;
using FluentAssertions;
using program.Commands;
using program.Data;
using program.Parsers;
using Xunit;

namespace Test.Parsers
{
    public class SoundCommandParserTest
    {
        
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
            var parser = new SoundCommandParser();
            parser.Parse(parameters).Should().BeOfType(typeof(SoundCommand));
        }

        [Theory]
        [MemberData(nameof(GetInvalidSoundQualities))]
        public void ParsingInvalidSoundQualitiesThrowsException(string parameters)
        {
            var parser = new SoundCommandParser();
            Action act = () => parser.Parse(parameters);

            act.Should().Throw<Exception>();
        }

        

        [Theory]
        [InlineData("100000000000000000000000,1")]
        [InlineData("1, 10000000000000000000000")]
        public void SoundQualitiesWithBigNumbersThrowsOverflowException(string parameters)
        {
            var parser = new SoundCommandParser();
            Action act = () => parser.Parse(parameters);

            act.Should().Throw<OverflowException>();
        }

       
        [Fact]
        public static void ParsingSoundQualitiesReturnsNullWhenInputIsNull()
        {
            var parser = new SoundCommandParser();
            parser.Parse(null).Should().BeNull();
        }
    }
}