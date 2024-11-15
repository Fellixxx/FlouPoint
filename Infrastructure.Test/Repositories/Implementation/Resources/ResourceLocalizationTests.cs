﻿namespace Infrastructure.Test.Repositories.Implementation.Resources
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Test.Repositories.Implementation.CRUD;

    [TestClass]
    public class ResourceLocalizationTests : SetupTest
    {
        private readonly Dictionary<string, string> _expectedMessages = new()
        {
            { "EntityFailedNecesaryData", "Necessary data was not provided." },
            { "FailedAlreadyRegisteredEmail", "A user is already registered with this email." },
            { "SuccessfullyGeneric", "{0} was created successfully." },
            { "FailedEmailInvalidFormat", "The given email is not in a valid format" },
            // Add more known keys and messages as needed
        };

        [TestMethod]
        public void ErrorMessageRetrieval_ForKnownKeys_ShouldReturnCorrectLocalizedMessages()
        {
            foreach (var kvp in _expectedMessages)
            {
                string key = kvp.Key;
                string expectedMessage = kvp.Value;

                // Act: Retrieve the message from the resource handler
                var message = _resourceHandler.GetResource(key);

                // Assert: Verify the retrieved message matches the expected one
                Assert.AreEqual(expectedMessage, message, $"The message for key '{key}' was not retrieved correctly.");
            }
        }

        [TestMethod]
        public void ErrorMessageRetrieval_ForUnknownKeys_ShouldReturnFallbackMessage()
        {
            const string unknownKey = "UnknownKey";
            const string fallbackMessage = "Message not found."; // Define a fallback message, if applicable

            // Set up the mock to return a fallback message for unknown keys
            var mockResourceHandler = new Mock<IResourceHandler>();
            mockResourceHandler.Setup(r => r.GetResource(It.IsAny<string>())).Returns((string key) =>
                _expectedMessages.ContainsKey(key) ? _expectedMessages[key] : fallbackMessage);

            _resourceHandler = mockResourceHandler.Object; // Override the setup with the mocked fallback

            // Act: Attempt to retrieve a message with an unknown key
            var resultMessage = _resourceHandler.GetResource(unknownKey);

            // Assert: Verify that the fallback message is returned
            Assert.AreEqual(fallbackMessage, resultMessage, "The fallback message for an unknown key was not correct.");
        }

        [TestMethod]
        public async Task FallbackMessageRetrieval_UsingResourceProvider_ShouldReturnDefaultOnUnknownKey()
        {
            const string unknownKey = "UnknownKey";
            const string fallbackMessage = "Message not found."; // Define the fallback message if the key is unknown

            // Set up the mock to handle unknown keys gracefully
            var mockResourceProvider = new Mock<IResourcesProvider>();
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string key, string defaultValue) =>
                    _expectedMessages.ContainsKey(key) ? _expectedMessages[key] : defaultValue);

            _resourceProvider = mockResourceProvider.Object;

            // Act: Attempt to retrieve a message with an unknown key, expecting a fallback
            var resultMessage = await _resourceProvider.GetMessageValueOrDefault(unknownKey, fallbackMessage);

            // Assert: Ensure the fallback message is returned for the unknown key
            Assert.AreEqual(fallbackMessage, resultMessage, "The fallback message for an unknown key was not correct.");
        }
    }
}