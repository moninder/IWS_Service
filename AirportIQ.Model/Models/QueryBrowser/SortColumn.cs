using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class SortColumn
    {
        public SortColumn()
        {
        }

        public SortColumn(DisplayColumn column, string sort)
        {
            m_Column = column;
            m_SortDirection = sort;
        }


        private DisplayColumn m_Column;
        public DisplayColumn Column
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        private string m_SortDirection;
        public string SortDirection
        {
            get { return m_SortDirection; }
            set { m_SortDirection = value; }
        }

    }

}
