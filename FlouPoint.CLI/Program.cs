using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scriban;
using FlouPoint.CLI.TestGeneration.Factories;
using FlouPoint.CLI.TestGeneration.Context;

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
                var propertyName = prop.Identifier.Text;
                var propertyType = prop.Type.ToString();
               
                //
                var expectedResult = "Success";

                // Get the appropriate strategy
                var strategy = TestGenerationStrategyFactory.GetStrategy(propertyType);

                foreach (var testCase in strategy.GetSuccessValues())
                {
                    var expectedValue = testCase.Value;
                    var testCaseValue = testCase.Key;
                    var context = new TestGeneratorContext(strategy);
                    var testCode = context.GenerateTestCode(className, propertyName, expectedValue, testCaseValue, expectedResult);
                    Console.WriteLine(testCode);
                }

            }
        }

    }
}
