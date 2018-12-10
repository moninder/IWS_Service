using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

using Logging;

using ISBLibTest;

namespace ISBFormTest
{
	public partial class Form1 : Form
	{
		ISBTest isbt;

		public Form1()
		{
			InitializeComponent();
		}

		TextBoxTraceListener _textBoxListener;

		private void Form1_Load(object sender, EventArgs e)
		{
			_textBoxListener = new TextBoxTraceListener(txtDisplayTrace);
//			Trace.Listeners.Add(_textBoxListener);
			Debug.Listeners.Add(_textBoxListener);
		}

		private void textbox_changed(object sender, EventArgs e)
		{
			txtDisplayTrace.SelectionStart = txtDisplayTrace.Text.Length; 
			txtDisplayTrace.ScrollToCaret(); 
			txtDisplayTrace.Refresh();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			Trace.WriteLine(string.Format("Starting test: {0}", DateTime.Now.ToLongTimeString()));

//			isbt = new ISBTest();
//			isbt = new ISBTest("192.168.60.155");
			isbt = new ISBTest(ipAddress.Text);

			isbt.AllTestsAllPeople_Battery();

			Trace.WriteLine(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			txtDisplayTrace.Clear();
			txtDisplayTrace.Refresh();
		}


	}
}
