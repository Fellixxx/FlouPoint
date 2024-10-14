using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Repository.CRUD.ResourceEntry
{
    /// <summary>
    /// The IResourceEntryQuery interface defines the contract for querying ResourceEntry entities.
    /// It extends multiple interfaces to provide filtering, counting, paginated retrieval, 
    /// and ID-based querying functionality for ResourceEntry entities.
    /// </summary>
    public interface IResourceEntryQuery : IResourceEntryReadFilter, IResourceEntryReadFilterCount, IResourceEntryReadFilterPage, IResourceEntryReadId
    {
    }
}
