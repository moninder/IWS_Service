using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class FilterOption
    {
        public FilterOption()
        {
        }

        public FilterOption(DatabaseColumn dbColumn, string operatorKey, string value)
        {
            m_DatabaseColumn = dbColumn;
            m_OperatorKey = operatorKey;
            m_FilterValue = value;
        }

        private DatabaseColumn m_DatabaseColumn;
        public DatabaseColumn DatabaseColumn
        {
            get { return m_DatabaseColumn; }
            set { m_DatabaseColumn = value; }
        }

        private string m_OperatorKey;
        public string OperatorKey
        {
            get { return m_OperatorKey; }
            set { m_OperatorKey = value; }
        }

        private string m_FilterValue;
        public string FilterValue
        {
            get { return m_FilterValue; }
            set { m_FilterValue = value; }
        }
    }


    public class Filter
    {
        public Filter()
        {
        }

        public Filter(DisplayColumn column, Operator op, string value)
        {
            if ((column == null))
                throw new ArgumentException("column cannot be null");
            if ((op == null))
                throw new ArgumentException("operator cannot be null");
            if ((column.DatabaseColumns == null || column.DatabaseColumns.Count == 0))
                throw new Exception("DisplayColumn contains no database columns");

            m_Column = column;
            m_Operator = op;
            m_DisplayValue = value;
            m_Options.Add(new FilterOption(m_Column.DatabaseColumns[0], op.Key, value));
        }

        private DisplayColumn m_Column;
        public DisplayColumn Column
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        private Operator m_Operator;
        public Operator Operator
        {
            get { return m_Operator; }
            set { m_Operator = value; }
        }

        private string m_DisplayValue;
        public string DisplayValue
        {
            get { return m_DisplayValue; }
            set { m_DisplayValue = value; }
        }

        private List<FilterOption> m_Options = new List<FilterOption>();
        public List<FilterOption> Options
        {
            get { return m_Options; }
            set { m_Options = value; }
        }

        private List<string> m_Settings = new List<string>();
        public List<string> Settings
        {
            get { return m_Settings; }
            set { m_Settings = value; }
        }

    }
}
