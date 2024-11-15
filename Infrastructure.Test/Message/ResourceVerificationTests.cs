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
                if(!File.Exists(resxFilePath))
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


        //[TestMethod]
        //public void VerifyClassResourcesExistInResourceKeysList()
        //{
        //    // Define the root directory for your Repositories folder
        //    string repositoriesPath = @"C:\GitHub\FlouPoint\Infrastructure\";

        //    // Get all .cs files that are not resource files
        //    var classFiles = Directory.GetFiles(repositoriesPath, "*.cs", SearchOption.AllDirectories)
        //        .Where(file => !file.EndsWith(".resx", StringComparison.OrdinalIgnoreCase))
        //        .ToList();

        //    foreach (var classFile in classFiles)
        //    {
        //        // Get class name and expected .resx file path
        //        string className = Path.GetFileNameWithoutExtension(classFile);
        //        string resxFilePath = Path.Combine(Path.GetDirectoryName(classFile), $"Resource{className}.resx");

        //        // Ensure the corresponding .resx file exists
        //        if (!File.Exists(resxFilePath))
        //        {
        //            continue;
        //        }

        //        if (classFile.Contains(".resx"))
        //        {
        //            continue;
        //        }

        //        // Get resource keys used in the class file
        //        var usedResourceKeys = ExtractResourceKeysFromClassFile(classFile);
        //        var usedListResourceKey = ExtractResourceKeysListFromClassFile(classFile);
        //        foreach (var key in usedResourceKeys)
        //        {
        //            Assert.IsTrue(usedListResourceKey.Contains(key), $"In the list _resourceKey is missing  '{key}' used in {className} is missing in {resxFilePath}");
        //        }
        //    }
        //}


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
            foreach (var line in File.ReadLines(classFilePath))
            {
                // Example regex to match keys in the form "_handler.GetResource(\"KeyName\")"
                var match = System.Text.RegularExpressions.Regex.Match(line, @"_handler\.GetResource\(\s*""([^""]+)""\s*\)");

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

        // Find the field declaration for _resourceKeys
        var fieldDeclarations = root.DescendantNodes()
            .OfType<FieldDeclarationSyntax>()
            .Where(f => f.Declaration.Variables.Any(v => v.Identifier.Text == "_resourceKeys"));

        foreach (var field in fieldDeclarations)
        {
            // Look for an initializer that contains a list of strings (new List<string> { "Key1", "Key2", ... })
            var initializer = field.Declaration.Variables
                .FirstOrDefault(v => v.Identifier.Text == "_resourceKeys")
                ?.Initializer?.Value as ObjectCreationExpressionSyntax;

            if (initializer != null)
            {
                // Find the initializer expression containing the list of keys
                var initializerExpression = initializer.Initializer as InitializerExpressionSyntax;

                if (initializerExpression != null)
                {
                    foreach (var expression in initializerExpression.Expressions.OfType<LiteralExpressionSyntax>())
                    {
                        // Extract the string value and add it to the resourceKeys set
                        var key = expression.Token.ValueText;
                        resourceKeys.Add(key);
                    }
                }
            }
        }

        return resourceKeys;
    }
    }
}
