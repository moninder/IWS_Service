using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageWare.ISBLibrary;
using System.Collections;

namespace ISBWrapper
{
    public static class Extensions
    {
        internal static bool SetValue(this ArrayList MetaData, string Key, string Value, enValueType ValueType)
        {
            bool returnValue = false;
            NameValueSequence nvs = GetMetaField(MetaData, Key);
            if (nvs != null)
            {
                if (nvs.Value != Value)
                {
                    nvs.Value = Value;
                    returnValue = true;
                }
            }
            else
            {
                nvs = new NameValueSequence(0, Key, Value, ValueType);
                MetaData.Add(nvs);
                returnValue = true;
            }

            return returnValue;
        }

        internal static NameValueSequence GetMetaField(ArrayList metaList, string fieldName)
        {
            NameValueSequence retval = null;

            // if field present in list return it
            foreach (NameValueSequence nvp in metaList)
            {
                if (string.Compare(nvp.Name, fieldName, true) == 0)
                {
                    retval = nvp;
                    break;
                }
            }

            return retval;
        }
    }
}
