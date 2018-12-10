using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class DatabaseColumn
    {
        public DatabaseColumn()
        {
        }

        public DatabaseColumn(string columnName, string dataType, int maxLength)
        {
            m_ColumnName = columnName;
            m_DataType = dataType;
            m_MaxLength = maxLength;
        }

        private string m_ColumnName;
        public string ColumnName
        {
            get { return m_ColumnName; }
            set { m_ColumnName = value; }
        }

        private string m_DataType;
        public string DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }

        private int m_MaxLength;
        public int MaxLength
        {
            get { return m_MaxLength; }
            set { m_MaxLength = value; }
        }

    }

}
