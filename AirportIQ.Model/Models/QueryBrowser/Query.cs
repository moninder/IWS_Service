using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class Query
    {

        private int m_Id;
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private int m_OwnerId;
        public int OwnerId
        {
            get { return m_OwnerId; }
            set { m_OwnerId = value; }
        }

        private string m_LinkType = LinkTypes.All;
        public string LinkType
        {
            get { return m_LinkType; }
            set { m_LinkType = value; }
        }

        private bool m_Editable = false;
        public bool Editable
        {
            get { return m_Editable; }
            set { m_Editable = value; }
        }

        private List<DisplayColumn> m_AvailableColumns = new List<DisplayColumn>();
        public List<DisplayColumn> AvailableColumns
        {
            get { return m_AvailableColumns; }
            set { m_AvailableColumns = value; }
        }

        private List<DisplayColumn> m_SelectedColumns = new List<DisplayColumn>();
        public List<DisplayColumn> SelectedColumns
        {
            get { return m_SelectedColumns; }
            set { m_SelectedColumns = value; }
        }

        private List<SortColumn> m_SortColumns = new List<SortColumn>();
        public List<SortColumn> SortColumns
        {
            get { return m_SortColumns; }
            set { m_SortColumns = value; }
        }

        private string m_DataSource;
        public string DataSource
        {
            get { return m_DataSource; }
            set { m_DataSource = value; }
        }

        private List<Filter> m_Filters = new List<Filter>();
        public List<Filter> Filters
        {
            get { return m_Filters; }
            set { m_Filters = value; }
        }

        private string m_QueryType;
        public string QueryType
        {
            get { return m_QueryType; }
            set { m_QueryType = value; }
        }

        private string m_QueryKey;
        public string QueryKey
        {
            get { return m_QueryKey; }
            set { m_QueryKey = value; }
        }

        private bool m_FilterByFacilityCode;
        public bool FilterByFacilityCode
        {
            get { return m_FilterByFacilityCode; }
            set { m_FilterByFacilityCode = value; }
        }

        private Dictionary<string, object> m_EditorData = new Dictionary<string, object>();
        public Dictionary<string, object> EditorData
        {
            get { return m_EditorData; }
            set { m_EditorData = value; }
        }

        //default implementation
        public virtual string SprocName
        {
            get { return String.Empty; }
        }

        //default implementation
        public virtual string[] LookupSets
        {
            get { return null; }
        }

        protected IEnumerable<ListItem> GetEnumListItems(System.Type enumType)
        {
            int[] values = (int[])System.Enum.GetValues(enumType);
            List<ListItem> list = new List<ListItem>();

            for (int i = 0; i <= values.Length - 1; i++)
            {
                list.Add(new ListItem(values[i].ToString(), System.Enum.GetName(enumType, values[i])));
            }

            return list;
        }

        //Ensures if any of the columns from the master query were removed that they are removed from a saved query which may still contain them
        //This is to prevent a possibly SQL error looking for an old column that doesn't exist
        public void EnsureSubset(List<DisplayColumn> masterColumnSet)
        {
            if ((masterColumnSet == null))
                throw new ArgumentNullException("masterColumnSet");

            DisplayColumnComparer comparer = new DisplayColumnComparer();
            for (int i = this.AvailableColumns.Count - 1; i >= 0; i += -1)
            {
                if (!masterColumnSet.Contains(this.AvailableColumns[i], comparer))
                    this.AvailableColumns.RemoveAt(i);
            }

            for (int i = this.SelectedColumns.Count - 1; i >= 0; i += -1)
            {
                if (!masterColumnSet.Contains(this.SelectedColumns[i], comparer))
                    this.SelectedColumns.RemoveAt(i);
            }

            //Column missing from the original available set, add it in
            for (int i = 0; i <= masterColumnSet.Count - 1; i++)
            {
                if (!this.AvailableColumns.Contains(masterColumnSet[i], comparer) && !this.SelectedColumns.Contains(masterColumnSet[i], comparer))
                {
                    this.AvailableColumns.Add(masterColumnSet[i]);
                }
            }
        }
    }
}
