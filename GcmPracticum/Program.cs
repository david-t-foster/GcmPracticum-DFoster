using System;

namespace GcmPracticum
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
            }
            else
            {
                try
                {
                    var message = MealRequest.CreateFromString(args[0]).Description;
                    Console.WriteLine(message);
                }
                catch (ParseInputException)
                {
                    Usage();
                }

            }
            // anything else is thrown

        }

        static void Usage()
        {
            Console.WriteLine("meal request should be of the form 'morning, 1, 2' or 'night, 1, 2, 3'");
            Environment.Exit(1);
        }
    }
}
