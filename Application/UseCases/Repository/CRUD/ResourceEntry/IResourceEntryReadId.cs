using Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Repository.CRUD.ResourceEntry
{
    using ResourceEntry = Domain.Entities.ResourceEntry;
    public interface IResourceEntryReadId
    {
        /// <summary>
        /// Retrieves a ResourceEntry entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ResourceEntry entity to retrieve.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the ResourceEntry entity if it exists, or null if it does not.</returns>
        Task<OperationResult<ResourceEntry>> ReadId(string id);

        /// <summary>
        /// Retrieves a ResourceEntry entity based on the provided bearer token.
        /// This can be useful for authenticated requests where the bearer token is used to identify the entity.
        /// </summary>
        /// <param name="bearerToken">The bearer token used to authenticate and retrieve the ResourceEntry.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the ResourceEntry entity if it exists, or null if it does not.</returns>
        Task<OperationResult<ResourceEntry>> ReadByBearer(string bearerToken);
    }
}
