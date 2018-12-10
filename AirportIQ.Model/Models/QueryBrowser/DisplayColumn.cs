using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class DisplayColumnComparer : IEqualityComparer<DisplayColumn>
    {

        public bool Equals(DisplayColumn x, DisplayColumn y)
        {
            return x.ColumnKey.Equals(y.ColumnKey);
        }

        public int GetHashCode(DisplayColumn obj)
        {
            return obj.ColumnKey.GetHashCode();
        }

    }

    public class DisplayColumn
    {


        public DisplayColumn()
        {
        }

        public DisplayColumn(string columnKey, string columnName, string headerText, string dataType, int maxLength, string editorType, string editorDataKey = "")
            : this(columnKey, columnName, headerText, new DatabaseColumn[] { new DatabaseColumn(columnName, dataType, maxLength) }, editorType, editorDataKey)
        {
        }

        public DisplayColumn(string columnKey, string displayColumnName, string headerText, IEnumerable<DatabaseColumn> databaseColumns, string editorType, string editorDataKey = "")
        {
            this.ColumnKey = columnKey;
            this.DisplayColumnName = displayColumnName;
            this.HeaderText = headerText;
            this.EditorType = editorType;
            this.EditorDataKey = editorDataKey;
            this.DatabaseColumns.AddRange(databaseColumns);
        }

        private string m_Key;
        public string ColumnKey
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        private string m_DisplayColumnName;
        public string DisplayColumnName
        {
            get { return m_DisplayColumnName; }
            set { m_DisplayColumnName = value; }
        }

        private string m_HeaderText;
        public string HeaderText
        {
            get { return m_HeaderText; }
            set { m_HeaderText = value; }
        }

        private string m_EditorDataKey;
        public string EditorDataKey
        {
            get { return m_EditorDataKey; }
            set { m_EditorDataKey = value; }
        }

        private string m_EditorType;
        public string EditorType
        {
            get { return m_EditorType; }
            set { m_EditorType = value; }
        }

        private List<DatabaseColumn> m_DatabaseColumns = new List<DatabaseColumn>();
        public List<DatabaseColumn> DatabaseColumns
        {
            get { return m_DatabaseColumns; }
            set { m_DatabaseColumns = value; }
        }

    }
}
