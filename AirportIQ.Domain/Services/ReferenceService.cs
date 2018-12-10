using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.References;
using AirportIQ.Data.SqlServer.Repositories.References;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
    /// <summary>
    /// Reference Table Service
    /// </summary>
    public class ReferenceService : IReferenceService
    {
        private IReferenceRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceService"/> class.
        /// </summary>
        /// <param name="viewKey">The view key.</param>
        public ReferenceService(string viewKey)
        {
            _repository = ReferenceRepositoryFactory.Create(viewKey);
        }

        /// <summary>
        /// Gets the view data.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public IEnumerable<ReferenceViewEntity> GetViewData(int pageNumber, int pageSize)
        {
            return _repository.GetViewData(pageNumber, pageSize);
        }

        /// <summary>
        /// Saves the specified view data.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        public Dictionary<string, string> Save(IEnumerable<ReferenceViewEntity> viewData)
        {
            return _repository.Save(viewData);
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="newValues">The new values.</param>
        /// <returns></returns>
        public bool Validate(ReferenceViewEntity original, ReferenceViewEntity newValues)
        {
            if (!newValues.Action.HasValue || newValues.Action.Value == ReferenceAction.Delete)
                return true;

            if (newValues.Action.Value == ReferenceAction.Insert || original.Key != newValues.Key)
                return !_repository.Exists(newValues);

            return true;
        }

        /// <summary>
        /// Gets the total row count.
        /// </summary>
        /// <returns></returns>
        public int GetTotalRowCount()
        {
            return _repository.GetTotalRowCount();
        }
    }
}
