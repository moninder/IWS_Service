using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace AirportIQ.Model.Models.Helpers
{
    /// <summary>
    /// Helper class for creating the PersonAlias jqgrid for creating JSON code.
    /// </summary>
    public class PersonAlias : IJsonDatasource
    {
        /// <summary>
        /// Constructor off of values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        public PersonAlias(Int32 id, String prefix, String first, String middle, String last, String suffix)
        {
            ID = id;
            Prefix = prefix;
            First = first;
            Middle = middle;
            Last = last;
            Suffix = suffix;
        }

        public Int32 ID { get; set; }
        public String Prefix { get; set; }
        public String First { get; set; }
        public String Middle { get; set; }
        public String Last { get; set; }
        public String Suffix { get; set; }

        /// <summary>
        /// Implementation of the IJsonDatasource interface
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
