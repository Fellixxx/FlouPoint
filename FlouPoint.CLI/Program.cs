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
                string propertyName = prop.Identifier.Text;
                string propertyType = prop.Type.ToString();

                // Get the appropriate strategy
                var strategy = TestGenerationStrategyFactory.GetStrategy(propertyType);

                // Use the context to generate the test code
                var context = new TestGeneratorContext(strategy);
                string testCode = context.GenerateTestCode(className, propertyName);

                // Output the test code
                
                Console.WriteLine(testCode);
            }
        }

        static string GenerateTestCode(string className, string propertyName, string propertyType)
        {
            var templateText = File.ReadAllText(@"Template\TestTemplate_IsValid.sbn");
            var template = Template.Parse(templateText);
            var model = new
            {
                class_name = className,
                property_name = propertyName,
                property_type = propertyType,
                valid_value = GetValidValue(propertyType)
            };

            return template.Render(model);
        }
        static string GetValidValue(string propertyType)
        {
            switch (propertyType)
            {
                case "string":
                    return "\"ValidString\"";
                case "int":
                    return "42";
                case "double":
                    return "3.14";
                case "bool":
                    return "true";
                case "DateTime":
                    return "DateTime.Now";
                case "OperationExecute":
                    return "OperationExecute.Activate";
                case "LogLevel":
                    return "LogLevel.Information";
                // Add cases for other custom types if needed
                default:
                    return $"new {propertyType}()"; // Assumes a parameterless constructor
            }
        }

        static string GetInvalidValue(string propertyType)
        {
            switch (propertyType)
            {
                case "string":
                    return "\"\""; // Empty string
                case "int":
                    return "-1"; // Assuming negative values are invalid
                case "double":
                    return "-1.0"; // Assuming negative values are invalid
                case "bool":
                    return "false"; // Assuming false is invalid in certain contexts
                case "DateTime":
                    return "DateTime.MinValue"; // Represents an uninitialized date
                                                // For enums, you might return an invalid cast or out-of-range value
                case "OperationExecute":
                    return "(OperationExecute)999"; // Invalid enum value
                case "LogLevel":
                    return "(LogLevel)999"; // Invalid enum value
                default:
                    return "null"; // For reference types
            }
        }

        static bool IsInvalidValueApplicable(string propertyType)
        {
            // Determine if testing for invalid values makes sense for the property type
            switch (propertyType)
            {
                case "string":
                case "int":
                case "double":
                case "DateTime":
                case "OperationExecute":
                case "LogLevel":
                    return true;
                case "bool":
                    return false; // Assuming all boolean values are acceptable
                default:
                    return false; // Adjust as needed for custom types
            }
        }


    }
}
