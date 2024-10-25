using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scriban;
using FlouPoint.CLI.TestGeneration.Factories;
using FlouPoint.CLI.TestGeneration.Context;
using FlouPoint.CLI.TestGeneration.Interfaces;

namespace TestGenerator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Path to the class file
            string classFilePath = @"MyClass.cs";
            string classCode = File.ReadAllText(classFilePath);

            // Parse the class code
            SyntaxTree tree = CSharpSyntaxTree.ParseText(classCode);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            // Find the class declaration
            var classDeclaration = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault();

            if (classDeclaration != null)
            {
                // Process the class
                ProcessClass(classDeclaration);
            }
        }

        static void ProcessClass(ClassDeclarationSyntax classDeclaration)
        {
            var className = classDeclaration.Identifier.Text;

            var properties = classDeclaration.Members.OfType<PropertyDeclarationSyntax>();

            foreach (var prop in properties)
            {
                var propertyType = prop.Type.ToString();
                var strategy = TestGenerationStrategyFactory.GetStrategy(propertyType);
                GenerateTest(className, prop, strategy, true);
                GenerateTest(className, prop, strategy, false);
            }
        }

        private static void GenerateTest(string className, PropertyDeclarationSyntax prop, ITestGenerationStrategy strategy, bool success)
        {
            var expectedResult = success ? "Success" : "Failed";
            var testCases = success ? strategy.GetValidValues() : strategy.GetInvalidValues();
            foreach (var testCase in testCases)
            {
                var propertyName = prop.Identifier.Text;
                var expectedValue = testCase.Value;
                var testCaseValue = testCase.Key;
                var context = new TestGeneratorContext(strategy);
                var testCode = context.GenerateTestCode(className, propertyName, expectedValue, testCaseValue, expectedResult);
                Console.WriteLine(testCode);
            }
        }
    }
}
