using FlouPoint.CLI.TestGeneration.Interfaces;
using System.Text;

namespace FlouPoint.CLI.TestGeneration.Strategies.Generation
{
    public class DoubleTestGenerationStrategy : BaseTestGenerationStrategy
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
