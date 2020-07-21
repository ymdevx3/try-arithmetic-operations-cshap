using NUnit.Framework;
using CalculatorService;

namespace CalculatorService.Tests
{
    public class CalculatorService_IsCorrectShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("10+20*30+40-50*2/4 * 2", 600)]
        [TestCase("23+31", 54)]
        [TestCase("43-4+45*23", 1074)]
        [TestCase("32+54*27*4/13", 480.61538461538464)]
        public void 結果が正しいか確認(string expression, double result)
        {
            var value = Calculator.Calculate(expression);
            Assert.AreEqual(value, result);
        }

        [TestCaseSourceAttribute("CalcCases")]
        public void 結果が正しいか確認2(string expression, double result)
        {
            var value = Calculator.Calculate(expression);
            //Assert.AreEqual(value, result);
            Assert.That(value, Is.EqualTo(result));
        }

        static object[] CalcCases = 
        {
            new object[] { "10+20*30+40-50*2/4 * 2", 600 },
            new object[] { "23+31", 54 },
            new object[] { "43-4+45*23", 1074 },
            new object[] { "32+54*27*4/13", 480.61538461538464 },
        };
    }
}