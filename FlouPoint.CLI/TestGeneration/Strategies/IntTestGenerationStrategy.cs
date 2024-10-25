using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies.Given;
using FlouPoint.CLI.TestGeneration.Strategies.Then;
using FlouPoint.CLI.TestGeneration.Strategies.When;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class IntTestGenerationStrategy : ITestGenerationStrategy
    {
        private readonly IGivenExpression _givenExpression;
        private readonly IWhenExpression _whenExpression;
        private readonly IThenExpression _thenExpression;

        public IntTestGenerationStrategy()
        {
            _givenExpression = new IntGivenExpression();
            _whenExpression = new IntWhenExpression();
            _thenExpression = new IntThenExpression();
        }
        public string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult)
        {
            var propertyType = "int";
            // Concatenate the parts of the test code
            string testCode = "[Test]\n" +
                              $"public void When_{propertyName}_IsSetToValidValue_Then_ShouldReturnSameValue()\n" +
                              "{\n" +
                              _givenExpression.GenerateGiven(className, propertyName, propertyType, expectedValue) +
                              "\n" +
                              _whenExpression.GenerateWhen(className, propertyName) +
                              "\n" +
                              _thenExpression.GenerateThen(propertyName, expectedValue) +
                              "}\n";

            return testCode;
        }

        public List<KeyValuePair<string, string?>> GetInvalidValues()
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, string>> GetValidValues()
        {
            throw new NotImplementedException();
        }
    }

}
