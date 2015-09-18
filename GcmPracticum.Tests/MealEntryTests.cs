using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace GcmPracticum.Tests
{
    public class MealEntryTests
    {
        [Fact]
        public void SmokeTest()
        {
            var entry = new MealEntry
            (
                dishes: new string[] { "one", "two"},
                allowedMultiple: new int[] { 1}
            );

            entry.Should().NotBeNull();
            entry.Dishes.Count().Should().Be(2);
            entry.AllowedMultiple.Count().Should().Be(1);
        }
    }
}
