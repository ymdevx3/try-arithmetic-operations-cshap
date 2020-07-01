using System;
using System.Collections.Generic;

namespace TryArithmeticOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            var expressions = new List<string>() {
                "10+20*30+40-50*2/4 * 2",
                "23+31",
                "43-4+45*23",
                "32+54*27*4/13",
                " 1",
                "+ 2",
                "- 2",
                "* 2",
                "/ 2",
                "10 + 2",
                "10 - 2",
                "10 * 2",
                "10 / 2",
                "-10 + 2",
                "-10 - 2",
                "-10 * 2",
                "-10 / 2",
                "10 + 4 + 3",
                "10 + 4 - 3",
                "10 + 4 * 3",
                "10 + 4 / 3",
                "10 - 3 + 5",
                "10 - 3 - 5",
                "10 - 3 * 5",
                "10 - 3 / 5",
                "10 * 3 + 5",
                "10 * 3 - 5",
                "10 * 3 * 5",
                "10 * 3 / 5",
                "10 / 2 + 5",
                "10 / 2 - 5",
                "10 / 2 * 5",
                "10 / 2 / 5",
                "10 + 3 - 4 + 8",
                "10 + 3 - 4 - 8",
                "10 + 3 - 4 * 8",
                "10 + 3 - 4 / 8",
                "10 + 3 / 4 + 8",
                "10 + 3 / 4 - 8",
                "10 + 3 / 4 * 8",
                "10 + 3 / 4 / 8",
                "10 * 3 - 4 + 8",
                "10 * 3 - 4 - 8",
                "10 * 3 - 4 * 8",
                "10 * 3 - 4 / 8",
                "10 * 3 / 4 + 8",
                "10 * 3 / 4 - 8",
                "10 * 3 / 4 * 8",
                "10 * 3 / 4 / 8",
                "10 + 3 + 4 + 8 * 5",
                "10 + 3 + 4 * 8 + 5",
                "10 + 3 * 4 + 8 + 5",
                "10 * 3 + 4 + 8 + 5",
                "10 + 3 + 4 * 8 * 5",
                "10 + 3 * 4 + 8 * 5",
                "10 + 3 * 4 * 8 + 5",
                "10 * 3 + 4 + 8 * 5",
                "10 * 3 + 4 * 8 + 5",
                "10 * 3 * 4 + 8 + 5",
                "12+ -3",
                "1 + 2 / 0 * 0",
                "1 + a",
                "1.5 + 2.2"
            };

            foreach (var expression in expressions)
            {
                try
                {
                    var result = Calculator.Calculate(expression);
                    Console.WriteLine($"    ->  {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"    ->  [Error] {ex.Message}");
                }
            }

            foreach (var expression in expressions)
            {
                try
                {
                    var result = Calculator2.Calculate(expression);
                    Console.WriteLine($"    ->  {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"    ->  [Error] {ex.Message}");
                }
            }
        }
    }
}
