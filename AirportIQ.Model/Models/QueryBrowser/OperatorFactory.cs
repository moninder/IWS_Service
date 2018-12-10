using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class OperatorFactory
    {

        public static Operator[] Create(params string[] keys)
        {
            List<Operator> list = new List<Operator>();

            for (int i = 0; i <= keys.Length - 1; i++)
            {
                list.Add(Create(keys[i]));
            }

            return list.ToArray();
        }

        public static Operator Create(string key)
        {
            switch (key)
            {
                case OperatorType.Equal:
                    return new Operator(OperatorType.Equal, "Equal to");
                case OperatorType.NotEqual:
                    return new Operator(OperatorType.NotEqual, "Not equal to");
                case OperatorType.LessThan:
                    return new Operator(OperatorType.LessThan, "Less than");
                case OperatorType.LessThanEqual:
                    return new Operator(OperatorType.LessThanEqual, "Less than or equal to");
                case OperatorType.GreaterThan:
                    return new Operator(OperatorType.GreaterThan, "Greater than");
                case OperatorType.GreaterThanEqual:
                    return new Operator(OperatorType.GreaterThanEqual, "Greater than or equal to");
                case OperatorType.IsNull:
                    return new Operator(OperatorType.IsNull, "Is null");
                case OperatorType.IsNotNull:
                    return new Operator(OperatorType.IsNotNull, "Is not null");
                case OperatorType.StartsWith:
                    return new Operator(OperatorType.StartsWith, "Starts with");
                case OperatorType.NotStartsWith:
                    return new Operator(OperatorType.NotStartsWith, "Does not starts with");
                case OperatorType.Contains:
                    return new Operator(OperatorType.Contains, "Contains");
                case OperatorType.NotContains:
                    return new Operator(OperatorType.NotContains, "Does not contain");
                case OperatorType.EndsWith:
                    return new Operator(OperatorType.EndsWith, "Ends with");
                case OperatorType.NotEndsWith:
                    return new Operator(OperatorType.NotEndsWith, "Does not end with");
                default:
                    throw new ArgumentException(string.Format("Key {0} is not a valid value.", key));
            }
        }

        private static Dictionary<string, Operator> m_OperatorDictionary = null;
        public static Dictionary<string, Operator> GetOperatorDictionary()
        {
            if (m_OperatorDictionary == null)
            {
                m_OperatorDictionary = new Dictionary<string, Operator>();

                m_OperatorDictionary.Add(OperatorType.Equal, Create(OperatorType.Equal));
                m_OperatorDictionary.Add(OperatorType.NotEqual, Create(OperatorType.NotEqual));
                m_OperatorDictionary.Add(OperatorType.LessThan, Create(OperatorType.LessThan));
                m_OperatorDictionary.Add(OperatorType.LessThanEqual, Create(OperatorType.LessThanEqual));
                m_OperatorDictionary.Add(OperatorType.GreaterThan, Create(OperatorType.GreaterThan));
                m_OperatorDictionary.Add(OperatorType.GreaterThanEqual, Create(OperatorType.GreaterThanEqual));
                m_OperatorDictionary.Add(OperatorType.IsNull, Create(OperatorType.IsNull));
                m_OperatorDictionary.Add(OperatorType.IsNotNull, Create(OperatorType.IsNotNull));
                m_OperatorDictionary.Add(OperatorType.StartsWith, Create(OperatorType.StartsWith));
                m_OperatorDictionary.Add(OperatorType.NotStartsWith, Create(OperatorType.NotStartsWith));
                m_OperatorDictionary.Add(OperatorType.Contains, Create(OperatorType.Contains));
                m_OperatorDictionary.Add(OperatorType.NotContains, Create(OperatorType.NotContains));
                m_OperatorDictionary.Add(OperatorType.EndsWith, Create(OperatorType.EndsWith));
                m_OperatorDictionary.Add(OperatorType.NotEndsWith, Create(OperatorType.NotEndsWith));
            }

            return m_OperatorDictionary;
        }

        private static Dictionary<string, IEnumerable<string>> m_TypeToOperators = null;
        public static Dictionary<string, IEnumerable<string>> GetTypeToOperatorsMap()
        {
            if (m_TypeToOperators == null)
                m_TypeToOperators = LoadTypeDictionary();
            return m_TypeToOperators;
        }

        private static Dictionary<string, IEnumerable<string>> LoadTypeDictionary()
        {
            Dictionary<string, IEnumerable<string>> list = new Dictionary<string, IEnumerable<string>>();

            list.Add(DataType.Bool, new string[] {
				OperatorType.Equal,
				OperatorType.NotEqual
			});
            list.Add(DataType.Guid, new string[] {
				OperatorType.Equal,
				OperatorType.NotEqual,
				OperatorType.IsNull,
				OperatorType.IsNotNull
			});
            list.Add(DataType.DateTime, new string[] {
				OperatorType.Equal,
				OperatorType.NotEqual,
				OperatorType.LessThan,
				OperatorType.LessThanEqual,
				OperatorType.GreaterThan,
				OperatorType.GreaterThanEqual,
				OperatorType.IsNull,
				OperatorType.IsNotNull
			});
            list.Add(DataType.Float, new string[] {
				OperatorType.Equal,
				OperatorType.NotEqual,
				OperatorType.LessThan,
				OperatorType.LessThanEqual,
				OperatorType.GreaterThan,
				OperatorType.GreaterThanEqual,
				OperatorType.IsNull,
				OperatorType.IsNotNull
			});
            list.Add(DataType.Int, new string[] {
				OperatorType.Equal,
				OperatorType.NotEqual,
				OperatorType.LessThan,
				OperatorType.LessThanEqual,
				OperatorType.GreaterThan,
				OperatorType.GreaterThanEqual,
				OperatorType.IsNull,
				OperatorType.IsNotNull
			});
            list.Add(DataType.String, new string[] {
				OperatorType.Equal,
				OperatorType.NotEqual,
				OperatorType.IsNull,
				OperatorType.IsNotNull,
				OperatorType.StartsWith,
				OperatorType.NotStartsWith,
				OperatorType.Contains,
				OperatorType.NotContains,
				OperatorType.EndsWith,
				OperatorType.NotEndsWith
			});

            return list;
        }

    }
}
