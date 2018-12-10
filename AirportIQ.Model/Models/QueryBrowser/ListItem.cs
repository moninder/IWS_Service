using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class DependentListItem : ListItem
    {
        public DependentListItem(string value, string text)
            : base(value, text)
        {
        }

        private List<ListItem> m_ChildList = new List<ListItem>();
        public List<ListItem> ChildList
        {
            get { return m_ChildList; }
            set { m_ChildList = value; }
        }
    }


    public class ListItem
    {
        public ListItem()
        {
        }

        public ListItem(string value, string text)
        {
            m_Value = value;
            m_Text = text;
        }

        private string m_Value;
        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        private string m_Text;
        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
    }
}
