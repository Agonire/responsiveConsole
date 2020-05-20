using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentAssertions;
using Program;
using Xunit;

namespace Test
{
    public class ValidatorTest
    {
        public static IEnumerable<object[]> GetValidRawData =>
            new List<object[]>
            {
                new object[] {"PS:1000,2:E"},
                new object[] {"PS::E"},
                new object[] {"PS:;,.:E"},
            };

        public static IEnumerable<object[]> GetInvalidRawData =>
            new List<object[]>
            {
                new object[] {"PS:1000,2:"},
                new object[] {"PS:1000,2: E"},
                new object[] {""},
                new object[] {"S:100,2:E"},
                new object[] {"PS::E "},
                new object[] {"PSE"},
                new object[] {"P::E"},
            };

        [Theory]
        [MemberData(nameof(GetValidRawData))]
        public void ValidRawDataDefaultFormat(string data)
        {
            Validator.ValidateRawInput(data).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(GetInvalidRawData))]
        public void InvalidRawDataDefaultFormat(string data)
        {
            Validator.ValidateRawInput(data).Should().BeFalse();
        }
    }
}