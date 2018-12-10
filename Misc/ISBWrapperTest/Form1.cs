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
			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Starting test: {0}", DateTime.Now.ToLongTimeString()));

			isbt.AllTests();

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			txtDisplayTrace.Clear();
			txtDisplayTrace.Refresh();
		}


		private void AllCMSTests_Click(object sender, EventArgs e)
		{
			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Starting all CMS tests: {0}", DateTime.Now.ToLongTimeString()));

			isbt.AllCMSTests();

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));

		}

		private void RunAuditTest_Click(object sender, EventArgs e)
		{
			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Starting Audit test: {0}", DateTime.Now.ToLongTimeString()));

			isbt.Audit(ThresholdDays: 365);

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}

		private void RunDOCM_Click(object sender, EventArgs e)
		{
			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Starting all DOCM tests: {0}", DateTime.Now.ToLongTimeString()));

			isbt.AllDOCMTests();

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}

		private void IDMSTest_Click(object sender, EventArgs e)
		{
			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Starting all IDMS tests: {0}", DateTime.Now.ToLongTimeString()));

			isbt.AllIDMSTests();

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}

		
		private void GetPersonDocumentsButton_Click(object sender, EventArgs e)
		{ //only get the persons signature doc

			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   

			isbt.testSvc.log(string.Format("Showing IDMS signature document for IWS Person Guid {0} : {1}", IWSPersonGUID.Text, DateTime.Now.ToLongTimeString()));

			//isbt.GetPersonDocuments(IWSPersonGUID.Text); //if you wanted to get a list of all their documents
			isbt.GetDocument(IWSPersonGUID.Text, 15);

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}

		private void GetPersonPortraitButton_Click(object sender, EventArgs e)
		{ //only get the persons portrait

			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();

			isbt.testSvc.log(string.Format("Getting portrait for IWS Person Guid {0} : {1}", IWSPersonGUID.Text, DateTime.Now.ToLongTimeString()));

			isbt.GetIdentityDocument(IWSPersonGUID.Text);

			isbt.testSvc.log(string.Format("'Get' complete: {0}", DateTime.Now.ToLongTimeString()));
		}

		private void ShowCMSProvButton_Click(object sender, EventArgs e)
		{
			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Showing CMS provisioning data for CMS Card ID {0} : {1}", cms_card_id.Text, DateTime.Now.ToLongTimeString()));

			isbt.showCMSProvisioningDataByIWSCardID(int.Parse(cms_card_id.Text));

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}



		private void ShowBOAAProvButton_Click(object sender, EventArgs e)
		{

			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Showing BOAA provisioning data for BOAA Badge ID {0} : {1}", boaa_badge_id.Text, DateTime.Now.ToLongTimeString()));

			ISBTest.ProvisionData BOAAProvData = isbt.GetBOAAProvisioningData(badgeID: int.Parse(boaa_badge_id.Text));

			isbt.testSvc.log("IWS Card ID: " + BOAAProvData.IWS_CardID);
			isbt.testSvc.log("PIN: " + BOAAProvData.PIN);

			foreach (ISBTest.ProvCategoryData bcd in BOAAProvData.ProvisionedCategories)
			{
				isbt.testSvc.log("       AccessType: " + bcd.AccessType);
				isbt.testSvc.log("       CategoryID: " + bcd.CategoryID);
				isbt.testSvc.log("     CategoryName: " + bcd.CategoryName);
				isbt.testSvc.log("WhenBecomesActive: " + bcd.WhenBecomesActive);
				isbt.testSvc.log("      WhenExpires: " + bcd.WhenExpires);
			}
			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}




		private void PokeCMS_Click(object sender, EventArgs e)
		{

			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Telling CMS to download provisioning data for BOAA Badge ID {0} : {1}", boaa_badge_id.Text, DateTime.Now.ToLongTimeString()));
			isbt.pokeCMS(int.Parse(boaa_badge_id.Text));

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));
		}

		private void AuditThisBadgeButton_Click(object sender, EventArgs e)
		{

			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Auditing provisioning data for BOAA Badge ID {0} : {1}", boaa_badge_id.Text, DateTime.Now.ToLongTimeString()));
			isbt.testSvc.log("NOTE: THIS TEST DOES NOT UPDATE THE AUDIT STATUS TABLES!");

			isbt.SmallAuditTest(int.Parse(boaa_badge_id.Text));

			isbt.testSvc.log("NOTE: THIS TEST DOES NOT UPDATE THE AUDIT STATUS TABLES!");
			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));


		}

		private void ShowCMSButton_Click(object sender, EventArgs e)
		{

			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Showing provisioning data for BOAA Badge ID {0} : {1}", boaa_badge_id.Text, DateTime.Now.ToLongTimeString()));

			isbt.showCMSProvisioningDataByBOAABadgeID(int.Parse(boaa_badge_id.Text));

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));

		}

		private void ReTransmitButton_Click(object sender, EventArgs e)
		{

			isbt = new ISBTest(docm_ip_address.Text, idms_ip_address.Text, cms_ip_address.Text);
			isbt.Init();   //  setup (shard with NUnit code)

			isbt.testSvc.log(string.Format("Retransmit transaction {0} to IWS/TSC : {1}", TransactionControlNumber.Text, DateTime.Now.ToLongTimeString()));
			
			string result = isbt.RetransmitTSCTransaction(TransactionControlNumber.Text);

			isbt.testSvc.log(string.Format("Test complete: {0}", DateTime.Now.ToLongTimeString()));

		}














	}
}
