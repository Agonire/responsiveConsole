using System;
using System.Collections.Generic;
using FluentAssertions;
using Program;
using Program.CustomExceptions;
using Program.Data;
using Xunit;

namespace Test
{
    public static class CommandServiceTest
    {
        [Fact]
        public static void NullPacketThrowsException()
        {
            Action act = () => CommandService.CreateCommand(null);

            act.Should().Throw<NullReferenceException>();
        }
        
        [Theory]
        [MemberData(nameof(Data))]
        public static void InvalidCommandThrowsException(Packet packet)
        {
            Action act = () => CommandService.CreateCommand(packet);
            
            act.Should().Throw<InvalidPayloadException>();
        } 
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { new Packet("Wrong Command", "Any Parameters") },
                new object[] { new Packet("1", "Any Parameters") },
                new object[] { new Packet("", "Any Parameters") },
            };
    }
}