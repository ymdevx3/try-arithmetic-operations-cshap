using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace TryArithmeticOperations
{
    public class Calculator2
    {
        public static double Calculate(string expression)
        {
            // 数値または演算子で分割してリストにする
            var elements = Regex.Split(expression.Replace(" ", ""), "([0-9]+\\.?[0-9]?|\\+|-|\\*|/)").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            // Console.WriteLine($"  -> {string.Join(",", elements)}");

            // 先頭が数値なら
            if (int.TryParse(elements.First(), out int i)) elements.Insert(0, "+");

            Console.WriteLine($"{string.Join(",", elements)} : ");

            // 要素から [*] or [/] を見つけて先に計算
            // ※一回ずつ計算して要素リストを書き換えることを繰り返す
            var isContinue = true;
            var newElements = new List<string>(elements);
            while (newElements.Any(x => x = "*" || x = "/"))
            {
                newElements = CalcMultiOrDiv(newElements);
                // Console.WriteLine($"    -> {string.Join(",", newElements)}");
            }

            // newElementsは加算、減算のみなので先頭から計算
            double result = 0;
            string op = "";
            foreach (var element in newElements)
            {
                if (element == "+" || element == "-")
                {
                    op = element;
                }
                else
                {
                    if (op == "+")
                    {
                        result = result + double.Parse(element);
                    }
                    else if (op == "-")
                    {
                        result = result - double.Parse(element);
                    }
                    else
                    {
                        result = double.Parse(element);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 要素リストから最初の [*] or [/] を探して前後で計算し
        /// 要素リストのその結果に入れ直してリストを返却
        /// ※ [*] or [/] がなくなるまで呼び出し元で繰り返しコールする
        /// </summary>
        /// <param name="elements">要素のリスト</param>
        /// <returns>乗算または商算の結果に置き換えた要素のリスト</returns>
        private static List<string> CalcMultiOrDiv(List<string> elements)
        {
            var newElements = new List<string>();

            for (int i = 0; i < elements.Count(); i++)
            {
                if (elements[i] == "*" || elements[i] == "/")
                {
                    // [*] or [/] より二つ前があれば先にリストにそのままつっこむ
                    if (i - 2 >= 0) newElements.AddRange(elements.Take(i-1));
                    // [*] or [/] なら前後の値で計算
                    if (elements[i] == "*"){
                        newElements.Add((double.Parse(elements[i-1]) * double.Parse(elements[i+1])).ToString());
                    }
                    else if (elements[i] == "/")
                    {
                        newElements.Add((double.Parse(elements[i-1]) / double.Parse(elements[i+1])).ToString());
                    }
                    // [*] or [/] より二つ後ろがまだあれば最後にリストにつっこむ
                    if (i + 2 < elements.Count()) newElements.AddRange(elements.Skip(i + 2).Take(elements.Count() - i + 1));
                    
                    return newElements; 
                }
            }
            return elements;
        }
    }
}
