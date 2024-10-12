﻿namespace Application.UseCases.ExternalServices
{
    using Application.Result;
    using Domain.DTO.Log;

    /// <summary>
    ///  The logging service is responsible for recording the events or actions that occur within the application.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// The CreateLog method takes in a log object, which encapsulates the details of an event that occurred in the system.
        /// </summary>
        /// <param name="log">The log</param>
        /// <returns>The result of operation</returns>
        Task<OperationResult<string>> CreateLog(Log log);
    }
}
