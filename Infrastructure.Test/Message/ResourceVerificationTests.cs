namespace Infrastructure.Test.Message
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
    }
}
