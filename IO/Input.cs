using System;

namespace Pong.IO
{
    static class Input
    {
        public static string GetString(string desc, Func<string, bool> validator = null, string fallback = null)
        {
            Console.Write($"{desc}: ");

            bool inputValid = false;
            string input = null;

            do {
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) && fallback != null) {
                    return fallback;
                }

                if (validator != null && !validator(input)) {
                    Console.Write("Invalid input. Try again: ");
                    continue;
                }

                inputValid = true;
            } while (!inputValid);

            return input;
        }

        public static int GetInt(string desc, Func<int, bool> validator = null, int? fallback = null)
        {
            Console.Write($"{desc}: ");

            bool inputValid = false;
            int result;

            do {
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) && fallback != null) {
                    return (int)fallback;
                }

                if (!int.TryParse(input, out result) || (validator != null && !validator(result))) {
                    Console.Write("Invalid input. Try again: ");
                    continue;
                }

                inputValid = true;
            } while (!inputValid);

            return result;
        }
    }
}
