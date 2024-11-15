namespace Infrastructure.Test.Message
{
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    [TestClass]
    public class ResourceVerificationTests
    {
        [TestMethod]
        public void VerifyClassResourcesExistInCorrespondingResxFile()
        {
            // Define the root directory for your Repositories folder
            string repositoriesPath = @"C:\GitHub\FlouPoint\Infrastructure\";

            // Get all .cs files that are not resource files
            var classFiles = Directory.GetFiles(repositoriesPath, "*.cs", SearchOption.AllDirectories)
                .Where(file => !file.EndsWith(".resx", StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var classFile in classFiles)
            {
                // Get class name and expected .resx file path
                string className = Path.GetFileNameWithoutExtension(classFile);
                string resxFilePath = Path.Combine(Path.GetDirectoryName(classFile), $"Resource{className}.resx");

                // Ensure the corresponding .resx file exists
                if (!File.Exists(resxFilePath))
                {
                    continue;
                }

                // Load resource keys from the .resx file
                var resourceKeys = LoadResourceKeys(resxFilePath);

                // Get resource keys used in the class file
                var usedResourceKeys = ExtractResourceKeysFromClassFile(classFile);

                // Check if each used resource key exists in the resource file
                foreach (var key in usedResourceKeys)
                {
                    Assert.IsTrue(resourceKeys.Contains(key), $"Resource key '{key}' used in {className} is missing in {resxFilePath}");
                }
            }
        }


        [TestMethod]
        public void VerifyClassResourcesExistInResourceKeysList()
        {
            // Define the root directory for your Repositories folder
            string repositoriesPath = @"C:\GitHub\FlouPoint\Infrastructure\";

            // Get all .cs files that are not resource files
            var classFiles = GetClassFiles(repositoriesPath);

            foreach (var classFile in classFiles)
            {
                if (classFile.EndsWith(".Designer.cs"))
                {
                    continue;
                }

                var code = File.ReadAllText(classFile);
                if (!code.Contains("_resourceKeys"))
                {
                    continue;
                }


                // Get resource keys used in the class file
                var usedResourceKeys = ExtractResourceKeysFromClassFile(classFile);
                var usedListResourceKey = ExtractResourceKeysListFromClassFile(classFile);
                string className = Path.GetFileNameWithoutExtension(classFile);
                foreach (var key in usedResourceKeys)
                {
                    Assert.IsTrue(usedListResourceKey.Contains(key), $"In the list _resourceKey is missing  '{key}' used in {className} is missing");
                }
            }
        }

        private static string[] GetClassFiles(string repositoriesPath)
        {
            return Directory.GetFiles(repositoriesPath, "*.cs", SearchOption.AllDirectories);
        }

        private HashSet<string> LoadResourceKeys(string resxFilePath)
        {
            var resourceKeys = new HashSet<string>();
            var document = XDocument.Load(resxFilePath);

            foreach (var dataElement in document.Descendants("data"))
            {
                var key = dataElement.Attribute("name")?.Value;
                if (!string.IsNullOrEmpty(key))
                {
                    resourceKeys.Add(key);
                }
            }

            return resourceKeys;
        }

        private HashSet<string> ExtractResourceKeysFromClassFile(string classFilePath)
        {
            var usedKeys = new HashSet<string>();

            // Read each line in the file to find resource keys
            IEnumerable<string> lines = File.ReadLines(classFilePath);
            foreach (var line in lines)
            {
                // Example regex to match keys in the form "_handler.GetResource(\"KeyName\")"
                var match = Regex.Match(line, @"_handler\.GetResource\(\s*""([^""]+)""\s*\)");

                // If a match is found, add the key to the set
                if (match.Success)
                {
                    usedKeys.Add(match.Groups[1].Value);
                }
            }

            return usedKeys;
        }

        public HashSet<string> ExtractResourceKeysListFromClassFile(string classFilePath)
        {
            var resourceKeys = new HashSet<string>();

            // Load and parse the file content into a SyntaxTree
            var fileContent = File.ReadAllText(classFilePath);
            var syntaxTree = CSharpSyntaxTree.ParseText(fileContent);

            // Get the root node of the syntax tree
            var root = syntaxTree.GetRoot();

            // Look for assignment expressions directly setting _resourceKeys
            var assignmentExpressions = root.DescendantNodes()
                .OfType<AssignmentExpressionSyntax>()
                .Where(ae => ae.Left.ToString() == "_resourceKeys");

            foreach (var assignment in assignmentExpressions)
            {
                // Check if the right-hand side is an array or list initializer
                var initializer = assignment.Right as CollectionExpressionSyntax;
                if (initializer != null)
                {
                    // Extract each string literal within the initializer
                    foreach (var expression in initializer.Elements)
                    {
                        var expressionElement = expression as ExpressionElementSyntax;
                        foreach (var node in expressionElement.DescendantNodes())
                        {
                            var nodekey = node as LiteralExpressionSyntax;
                            var key = nodekey.Token.ValueText;
                            resourceKeys.Add(key);
                        }                      
                    }
                }
                var objectCreation = assignment.Right as ObjectCreationExpressionSyntax;
                if (objectCreation != null)
                {
                    var initializerExpression = objectCreation.Initializer as InitializerExpressionSyntax;
                    if (initializerExpression != null)
                    {
                        foreach (var expression in initializerExpression.Expressions)
                        {
                            if (expression is LiteralExpressionSyntax literalExpression)
                            {
                                // Extract the string value of the literal and add it to the set
                                var key = literalExpression.Token.ValueText;
                                resourceKeys.Add(key);
                            }
                        }
                    }

                }


            }

            return resourceKeys;
        }
    }
}
