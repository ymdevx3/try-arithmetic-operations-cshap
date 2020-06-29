using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace try_arithmetic_operations_cshap
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
        }
    }

    public class Calculator
    {
        private static readonly ReadOnlyCollection<char> operators = new List<char>() { '+', '-', '*', '/' }.AsReadOnly();

        public static double Calculate(string expression)
        {
            double returnValue = 0;

            // スペース取り除く
            expression = expression.Replace(" ", "");
            Console.WriteLine($"{expression} : ");

            // 先頭が演算子なら先頭に0を付ける
            var s = operators.Contains(expression.First()) ? $"0{expression}" : expression;
            // 演算子で分割した数値のみを抜き出す
            var values = s.Split(operators.ToArray()).Where(x => !string.IsNullOrEmpty(x));
            // 文字列から演算子のみを抜き出す
            var targetOperators = s.ToCharArray().Where(x => operators.Contains(x)).Select(x => x.ToString());

            // 演算子が連続していないか
            if (values.Count() != targetOperators.Count() + 1) throw new Exception("式が不正です");

            // 数値以外が混じっていないか
            foreach (var value in values)
            {
                int num;
                if (!int.TryParse(value, out num)) throw new Exception($"数値以外[{value}]が含まれています");
            }

            // Console.WriteLine($"    -> Values[{string.Join(" ", values)}] Operators[{string.Join(" ", targetOperators)}]");

            // 数式の文字列を逆ポーランド記法に変換
            var rpn = ConvertToRPN(values, targetOperators);

            Console.WriteLine($"    -> [{string.Join(" ", rpn)}]");

            // 逆ポーランド記法から計算結果を取得
            returnValue = GetResult(rpn);

            return returnValue;
        }

        private static List<string> ConvertToRPN(IEnumerable<string> values, IEnumerable<string> operations)
        {
            var returnValue = new List<string>();

            var valQueue = new Queue<string>(values);
            var opQueue = new Queue<string>(operations);
            var opStack = new Stack<string>();

            while (valQueue.Any())
            {
                // Console.WriteLine($"valQueue [{string.Join(" ", valQueue)}]");
                // Console.WriteLine($"opQueue  [{string.Join(" ", opQueue)}]");
                // Console.WriteLine($"rpn      [{string.Join(" ", rpn)}]");
                // Console.WriteLine($"opStack  [{string.Join(" ", opStack)}]");
                // Console.WriteLine($"");

                var targetVal = valQueue.Dequeue();
                returnValue.Add(targetVal);

                if (opQueue.Any())
                {
                    var targetOp = opQueue.Dequeue();

                    if (opStack.Any() && (opStack.Peek() == "+" || opStack.Peek() == "-"))
                    {
                        // スタックの先頭が + または -
                        if (targetOp == "+" || targetOp == "-")
                        {
                            // 対象の演算子が + または -
                            returnValue.Add(opStack.Pop());
                        }
                        // else
                        // {
                        //     // 対象の演算子が * または /
                        // }
                    }
                    else if (opStack.Any() && (opStack.Peek() == "*" || opStack.Peek() == "/"))
                    {
                        // スタックの先頭が * または /

                        if (targetOp == "+" || targetOp == "-")
                        {
                            // 対象の演算子が + または -

                            // スタックから取り出してリストに追加
                            while (opStack.Any())
                            {
                                returnValue.Add(opStack.Pop());
                            }
                        }
                        else
                        {
                            // 対象の演算子が * または /
                            while (opStack.Any() && (opStack.Peek() == "*" || opStack.Peek() == "/"))
                            {
                                returnValue.Add(opStack.Pop());
                            }
                        }
                    }

                    // targetOpをスタックに追加
                    opStack.Push(targetOp);
                }
            }
            // スタックに残っている演算子を全てリストに追加
            while (opStack.Any())
            {
                returnValue.Add(opStack.Pop());
            }

            return returnValue;
        }

        private static double GetResult(List<string> rpnList)
        {
            var calcStack = new Stack<double>();

            foreach (var val in rpnList)
            {
                if (operators.Select(x => x.ToString()).Contains(val))
                {
                    double a = calcStack.Pop();
                    double b = calcStack.Pop();
                    if (val == "+")
                    {
                        calcStack.Push(b + a);
                    }
                    else if (val == "-")
                    {
                        calcStack.Push(b - a);
                    }
                    else if (val == "*")
                    {
                        calcStack.Push(b * a);
                    }
                    else if (val == "/")
                    {
                        if (a == 0) throw new Exception("0で割ることはできません。");
                        calcStack.Push(b / a);
                    }
                }
                else
                {
                    calcStack.Push(double.Parse(val));
                }
            }

            return calcStack.Pop();
        }
    }
}
