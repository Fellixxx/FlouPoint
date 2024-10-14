using Application.Result;
using Application.UseCases.ExternalServices;
using Domain.DTO.Log;
using Domain.EnumType.OperationExecute;
using FlouPoint.GitHub;
using Infrastructure.Other;
using System.Net.Mail;
/*
namespace FlouPoint.Infrastructure.ExternalServices.OS
{
    public class EnvironmentVariableProvider : IEnvironmentVariableProvider
    {
        private readonly ILogService _logService;

        public EnvironmentVariableProvider(ILogService logService)
        {
            _logService = logService;
        }


        public async Task<OperationResult<string>> GetEnvironmentVariable(string key)
        {
            try
            {
                if (key == null)
                {
                    return OperationBuilder<string>.FailureConfigurationMissingError("Resource.FailureConfigurationMissingErrorSendEmail");
                }

                // Retrieves the value of an environment variable from the current process.
                return OperationResult<string>.Success(Environment.GetEnvironmentVariable(key), "Resource.SuccessfullyEmai");
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, key, OperationExecute.Activate);
                await _logService.CreateLog(log);
                return OperationBuilder<string>.FailureDatabase("Resource.FailedEmailService");
            }
        }
    }
}
*/