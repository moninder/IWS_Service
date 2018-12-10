using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.References;

namespace AirportIQ.Domain.Contracts
{
    /// <summary>
    /// Reference Table Service
    /// </summary>
    public interface IReferenceService
    {
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
        Dictionary<string, string> Save(IEnumerable<ReferenceViewEntity> viewData);
        
        /// <summary>
        /// Gets the total row count.
        /// </summary>
        /// <returns></returns>
        int GetTotalRowCount();

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="newValues">The new values.</param>
        /// <returns></returns>
        bool Validate(ReferenceViewEntity original, ReferenceViewEntity newValues);
    }
}
