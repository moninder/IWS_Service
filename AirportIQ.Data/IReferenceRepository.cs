using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.References;

namespace AirportIQ.Data.SqlServer.Repositories.References
{
    /// <summary>
    /// Reference Table Repository
    /// </summary>
    public interface IReferenceRepository
    {
        /// <summary>
        /// Gets the total row count.
        /// </summary>
        /// <returns></returns>
        int GetTotalRowCount();
        
        /// <summary>
        /// Gets the view data.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        IEnumerable<ReferenceViewEntity> GetViewData(int pageNumber, int pageSize);
        
        /// <summary>
        /// Saves the specified view data.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        /// <returns></returns>
        Dictionary<string, string> Save(IEnumerable<ReferenceViewEntity> viewData);
        
        /// <summary>
        /// Determines if the entity exists
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Exists(ReferenceViewEntity entity);
    }
}
