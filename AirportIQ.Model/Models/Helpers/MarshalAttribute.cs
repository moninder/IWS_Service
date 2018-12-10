using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Helpers
{
    /// <summary>
    /// Attribute that is used to describe properties on a POCO object that will allow the 
    /// marshalling routines to move the data in controlled, non-default ways.  Note the 
    /// MarshallingAttribute does not have to be on a property for data to be marshaled 
    /// through it.
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Property)]
    class MarshalAttribute : Attribute
    {
        #region "Members"

        private string _fieldName = String.Empty;

        #endregion

        #region "Constructors"

        public MarshalAttribute(String fieldName)
        {
            _fieldName = fieldName;
        }

        #endregion


        #region "Properties"
        
        /// <summary>
        /// Public accessor for the field name property.  This field name defines the 
        /// name in the dataset that this field will map to instead of defaulting to the 
        /// name of the class' property.
        /// </summary>
        public String FieldName {get;set; }

        #endregion

    }
}
