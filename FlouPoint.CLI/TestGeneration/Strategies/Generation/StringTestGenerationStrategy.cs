using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies.Generation.ExpressionGenerator;

namespace FlouPoint.CLI.TestGeneration.Strategies.Generation
{
    public class StringTestGenerationStrategy : BaseTestGenerationStrategy
    {
        public override List<KeyValuePair<string, string>> GetValidValues()
        {
            // Create a list of valid string values with descriptive test case names
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("TestString", "Test String"),
                new KeyValuePair<string, string>("HelloWorld", "Hello World"),
                new KeyValuePair<string, string>("ValidValue", "ValidValue"),
                new KeyValuePair<string, string>("SampleText", "SampleText"),
                new KeyValuePair<string, string>("AnotherValidString", "AnotherValidString"),
                new KeyValuePair<string, string>("UserInputWithNumbers", "UserInput123"),
                new KeyValuePair<string, string>("SimpleExample", "Example"),
                new KeyValuePair<string, string>("BasicName", "Name"),
                new KeyValuePair<string, string>("AddressField", "Address"),
                new KeyValuePair<string, string>("DescriptionField", "Description")
            };
        }

        public override List<KeyValuePair<string, string?>> GetInvalidValues()
        {
            return new List<KeyValuePair<string, string?>>
            {
                new KeyValuePair<string, string?>("EmptyString", ""),
                new KeyValuePair<string, string?>("WhitespaceOnly", "   "),
                new KeyValuePair<string, string?>("NullValue", null),
                new KeyValuePair<string, string?>("TabCharacter", "\t"),
                new KeyValuePair<string, string?>("NewlineCharacter", "\n"),
                new KeyValuePair<string, string?>("SpecialCharacters", "Invalid@@@###"),
                new KeyValuePair<string, string?>("ExcessivelyLongString", new string('a', 1001)),
                new KeyValuePair<string, string?>("LeadingTrailingWhitespace", "   Invalid Text   "),
                new KeyValuePair<string, string?>("QuotedText", "\"Quoted Text\""),
                new KeyValuePair<string, string?>("Emojis", "😊😀😁"),
            };
        }
    }
}


