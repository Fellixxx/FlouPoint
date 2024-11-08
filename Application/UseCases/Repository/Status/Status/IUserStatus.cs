using Application.Result;
using Application.UseCases.Repository.Status.StatusChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Repository.Status.Status
{
    public interface IUserStatus : IStatusRepository
    {
        Task<OperationResult<bool>> Activate(string id);
        Task<OperationResult<bool>> Deactivate(string id);
    }
}
