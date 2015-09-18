using System;
using System.Collections.Generic;
using System.Linq;

namespace GcmPracticum
{
    public class MealRequest
    {
        // eventually, we'll probably read in the available meals from someplace else
        private static readonly IDictionary<string, MealEntry> AvailableMeals =
            new Dictionary<string, MealEntry>(StringComparer.InvariantCultureIgnoreCase)
            {
                {
                    "morning",
                    new MealEntry(
                        dishes: new string[] {"eggs", "toast", "coffee"},
                        allowedMultiple: new int[] {3}
                        )
                },
                {
                    "night",
                    new MealEntry(
                        dishes: new string[] {"steak", "potato", "wine", "cake"},
                        allowedMultiple: new int[] {2}
                        )
                }
            };

        private const string ParseErrorMessage = @"Could not read meal request"; 


        private MealRequest(string mealRequestType, IEnumerable<DishRequestItem> items)
        {
            this.MealRequestType = mealRequestType;
            this.Items = items;
        }

        public string MealRequestType { get; private set; }
        public IEnumerable<DishRequestItem> Items { get; private set; }

        public string Description
        {
            get
            {
                var decoder = new RequestDecoder();
                return decoder.DescribeRequest(this);
            }
        }

        private static int ParsePositiveInteger(string input)
        {
            int value = Int32.Parse(input);
            if (value <= 0)
                throw new ParseInputException(ParseErrorMessage);
            return value;
        }

        // this takes a string like "night, 1, 2, 2" and creates the meal request
        public static MealRequest CreateFromString(string input)
        {
            try
            {
                var parseItems = input.Split(", ".ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                string mealType = parseItems.First().ToLowerInvariant();
                if(!AvailableMeals.ContainsKey(mealType))
                    throw new ParseInputException("Invalid Meal Type");

                var dishItems = parseItems.Skip(1)
                    .Select(ParsePositiveInteger)
                    .GroupBy(i => i)
                    .Select(g => DishRequestItem.Create(g.Key, g.Count()))
                    .OrderBy(t => t.Item)
                    .ToArray();

                return new MealRequest(mealType, dishItems);

            }
            catch (InvalidOperationException ex)
            {
                throw new ParseInputException(ParseErrorMessage, ex);
            }
            catch (FormatException ex)
            {
                throw new ParseInputException(ParseErrorMessage, ex);
            }
        }

        // the description property got a little large, so I've broken it into 
        // its own class for isolation.  In the future, this could easily be changed
        // to be injected (and have an IRequestDecoder interface yada yada), but YAGNI for now
        private class RequestDecoder
        {
            private static string DecodeDish(int offset, string[] dishes)
            {
                return offset < 0 || offset >= dishes.Length ?
                    "error" :
                    dishes[offset];
            }

            private string DishSuffix(int[] allowedMultiple, DishRequestItem item)
            {
                return item.Count == 1 ? "" :
                    allowedMultiple.Contains(item.Item) ?
                        string.Format("(x{0})", item.Count) :
                        ", error";
            }

            private string FormatSingleDish(MealRequest request, DishRequestItem item)
            {
                var dishes = AvailableMeals[request.MealRequestType].Dishes;
                var allowed = AvailableMeals[request.MealRequestType].AllowedMultiple;
                return DecodeDish(item.Offset, dishes) + DishSuffix(allowed, item);
            }

            public string DescribeRequest(MealRequest request)
            {
                return request.Items
                    .Select(i => FormatSingleDish(request, i))
                    .TakeWhileInclusive(s => !s.Contains("error"))
                    .Join(", ");
            }
        }


    }
}
