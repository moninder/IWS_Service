using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class Operator
    {
        public Operator()
        {
        }

        public Operator(string key, string text)
        {
            m_Key = key;
            m_Text = text;
        }

        private string m_Key;
        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        private string m_Text;
        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
    }
}
