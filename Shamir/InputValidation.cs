using System;

namespace Shamir
{
    public static class InputValidation
    {
        public static int ValidateInput(int input, int value, string error, string newInput)
        {
            while (input <= value)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.WriteLine(newInput);
                Console.ForegroundColor = ConsoleColor.White;
                input = Convert.ToInt32(Console.ReadLine());
            }

            return input;
        }
    }
}
