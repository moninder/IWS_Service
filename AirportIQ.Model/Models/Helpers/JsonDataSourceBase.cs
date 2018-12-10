using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AirportIQ.Model.Models.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    class JsonDataSourceBase : IJsonDatasource, IJsonDatasourceChild
    {

        /// <summary>
        /// This function will take a data table and marshal the data into this 
        /// object.  It will walk the columns in the data table and look for 
        /// corresponding public properties on this object.  This is for 
        /// handling a single row.  To help avoid issues with reflection's
        /// to create a list call the MarshalListFromDataTable function.
        /// 
        /// There will no errors raised for fields that are present in the 
        /// dataset but not defined on the object.  There will be no errors
        /// raised for properties on the object that are not in the dataset.
        /// </summary>
        /// <param name="dt">The data table containing the data that you
        /// wish to place into this object.  It should have a single row.</param>
        /// <exception cref="ArgumentNullException">Thrown if the DB is null</exception>
        /// <exception cref="ArgumentException">Thrown when the data table does not have exactly one row</exception>
        public void MarshalFromDataTable(DataTable dt)
        {
            ////validity checks
            //if (null == dt)
            //    throw new ArgumentNullException("The passed data table is null");
            //if (dt.Rows.Count == 0)
            //    throw new ArgumentException("The passed data table has no rows");
            //if (dt.Rows.Count > 1)
            //    throw new ArgumentException("The passed data table has more than one row");

            ////walk this objects properties and map them either to the property name or 
            ////the field name specified in the MarshallingAttribute.
            //var properties = this.GetType().GetProperties();
            ////var query = from prop in properties
            ////            from att in properties.Any<Attributes
                        


            //DataRow dr = dt.Rows[0];

        }


        /// <summary>
        /// Property that is used to determine any actions performed on the row when being edited as part of a higher level 
        /// data source.  Due to possible serialization issues this is being set as a string with required values and not 
        /// as a enum.  Current valid values are as follows:
        ///     “I” - insert.  Indicates that the record has been inserted
        ///     “U” - update.  Indicates that the record has been updated
        ///     “D” - delete.  Indicates that the record has been deleted
        ///     “” – no action.  Indicates that the record has not changed
        /// </summary>
        protected string _action = "";
        public string _Action
        {
            get { return _action; }
            set
            {
                //null check
                if (null == value)
                    throw new ArgumentNullException("_Action value cannot be null");

                switch (value)
                {
                    case "I":
                    case "U":
                    case "D":
                    case "":
                        _action = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(String.Format("_Action value must be 'I', 'U', 'D', '' instead of {0}", value));
                }
            }
        }

        /// <summary>
        /// Base definition for serializing this object out to a JSON object 
        /// string.  Any common handlers for data type serialization should be handled here.
        /// </summary>
        /// <returns>Returns a JSON model of this object as a string</returns>
        public string ToJson()
        {
            IsoDateTimeConverter idtc = new IsoDateTimeConverter();
            idtc.DateTimeFormat = "MM/dd/yyyy";

            return JsonConvert.SerializeObject(this, idtc);
        }
    }
}
