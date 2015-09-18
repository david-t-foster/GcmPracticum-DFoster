using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GcmPracticum;
using Xunit;

namespace GcmPracticum.Tests
{
    public class ExtensionTests
    {
        public class IndexOf
        {
            private static readonly string[] TestArray =
                new string[] { "first", "second", "third", "second" };
            [Theory]
            [InlineData("second", 1)]
            [InlineData("first", 0)]
            [InlineData("third", 2)]
            public void FindsCorrectIndex(string test, int expected)
            {
                TestArray.IndexOf(s => s == test).Should().Be(expected);
            }

            [Theory]
            [InlineData("second", 1)]
            [InlineData("first", 0)]
            [InlineData("third", 2)]
            public void WithEqualityFindsCorrectIndex(string test, int expected)
            {
                TestArray.IndexOf(test).Should().Be(expected);
            }


        }

        public class StringJoin
        {
            [Fact]
            public void JoinsOnStandardInput()
            {
                var items = new string[] {"1", "2", "3"};
                items.Join(", ").Should().Be("1, 2, 3");
                items.Join("-").Should().Be("1-2-3");
            }

            [Fact]
            public void EmptyListCreatesEmptyString()
            {
                var items = new string[] {};
                items.Join(", ").Should().Be(String.Empty);
            }

        }
    }
}
