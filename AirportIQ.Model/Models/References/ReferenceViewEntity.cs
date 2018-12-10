using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.References
{
    /// <summary>
    /// Reference Screen View Data
    /// </summary>
    [Serializable]
    public class ReferenceViewEntity
    {
        public ReferenceAction? Action { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Determines if this instance is equal to the passed entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool Equals(ReferenceViewEntity entity)
        {
            if (entity == null)
                return false;

            return
            (
                Key == entity.Key &&
                Description == entity.Description &&
                Abbreviation == entity.Abbreviation &&
                SortOrder == entity.SortOrder &&
                IsActive == entity.IsActive
            );
        }
    }
}
