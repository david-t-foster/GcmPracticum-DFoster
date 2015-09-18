using System.Linq;
using FluentAssertions;
using GcmPracticum;
using Xunit;

namespace GcmPracticum.Tests
{
    public class MealRequestTests
    {

        public class StaticFactory
        {
            [Theory]
            [InlineData("morning, 1, 2", "morning")]
            [InlineData("night, 2", "night")]
            [InlineData("Night, 2", "night")]
            public void CreatesCorrectMealType(string input, string expected)
            {
                var request = MealRequest.CreateFromString(input);
                request.MealRequestType.Should().Be(expected);
            }

            [Theory]
            [InlineData("morning, 1, 2", 2)]
            [InlineData("night, 1, 2,     3", 3)]
            [InlineData("morning, 2,1,2", 2)]
            [InlineData("morning, 1,1,2", 2)]
            public void CreatesCorrectItemCount(string input, int expected)
            {
                var request = MealRequest.CreateFromString(input);
                request.Items.Count().Should().Be(expected);
            }

            [Theory]
            [InlineData("morning, 2, 1, 2", 0, 1)]
            [InlineData("night, 3, 1, 2", 1, 2)]
            [InlineData("night, 3, 1, 2", 2, 3)]
            [InlineData("morning, 2, 1, 2", 0, 1)]
            public void CreatesCorrectItemsInOrder(string input, int expectedIndex, int expected)
            {
                var request = MealRequest.CreateFromString(input);

                request.Items.Skip(expectedIndex).First()
                    .Item.Should().Be(expected);
            }

            [Theory]
            [InlineData("afternoon, 1, 2")]
            [InlineData("night, 1 ..2")]
            [InlineData(", 1, 2")]
            [InlineData("1, 2")]
            [InlineData("morning, 1, -2")]
            public void ThrowsParseExceptionOnError(string input)
            {
                Assert.Throws<ParseInputException>(() => MealRequest.CreateFromString(input));
            }

        }

        public class MealDescription
        {
            [Theory]
            [InlineData("morning, 1, 3", "coffee")]
            [InlineData("morning, 2", "toast")]
            [InlineData("night, 1, 2", "potato")]
            [InlineData("night, 1, 4", "cake")]
            [InlineData("morning, 1, 4", "error")]
            public void IncludesDish(string input, string expected)
            {
                var meal = MealRequest.CreateFromString(input);
                meal.Description.Should().Contain(expected);
            }

            // the complete set of matches
            [Theory]
            [InlineData("morning, 1, 2, 3", "eggs, toast, coffee")]
            [InlineData("morning, 2, 1, 3", "eggs, toast, coffee")]
            [InlineData("morning, 1, 2, 3, 4", "eggs, toast, coffee, error")]
            [InlineData("morning, 1, 2, 3, 3, 3", "eggs, toast, coffee(x3)")]
            [InlineData("night, 1, 2, 3, 4", "steak, potato, wine, cake")]
            [InlineData("night, 1, 2, 2, 4", "steak, potato(x2), cake")]
            [InlineData("night, 1, 2, 3, 5", "steak, potato, wine, error")]
            [InlineData("night, 1, 1, 2, 3, 5", "steak, error")]
            public void MatchesAcceptanceCriteria(string input, string output)
            {
                MealRequest.CreateFromString(input).Description.Should().Be(output);
            }

            [Theory]
            [InlineData("Morning, 1, 2, 3", "eggs, toast, coffee")]
            [InlineData("Morning, 1, 2, 3, 3, 3", "eggs, toast, coffee(x3)")]
            [InlineData("Night, 1, 1, 2, 3, 5", "steak, error")]
            public void AcceptsCaseInsensitiveInput(string input, string output)
            {
                MealRequest.CreateFromString(input).Description.Should().Be(output);
            }

        }


    }
}
