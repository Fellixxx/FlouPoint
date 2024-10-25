using FlouPoint.CLI.TestGeneration.Interfaces;

namespace FlouPoint.CLI.TestGeneration.Strategies.Generation
{
    public class BoolTestGenerationStrategy : BaseTestGenerationStrategy
    {
        public override List<KeyValuePair<string, string?>> GetInvalidValues()
        {
            throw new NotImplementedException();
        }

        public override List<KeyValuePair<string, string>> GetValidValues()
        {
            throw new NotImplementedException();
        }
    }

}
