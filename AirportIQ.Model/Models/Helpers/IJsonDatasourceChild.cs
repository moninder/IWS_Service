using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Helpers
{
    public interface IJsonDatasourceChild : IJsonDatasource
    {
        /// <summary>
        /// Property that is used to determine any actions performed on the row when being edited as part of a higher level 
        /// data source.  Due to possible serialization issues this is being set as a string with reuqired values and not 
        /// as a enum.  Current valid values are as follows:
        ///     i - insert.  Indicates that the record has been inserted
        ///     u - update.  Indicates that the record has been updated
        ///     d - delete.  Indicates that the record has been deleted
        /// </summary>
        string _Action { get; set; }

}
}
