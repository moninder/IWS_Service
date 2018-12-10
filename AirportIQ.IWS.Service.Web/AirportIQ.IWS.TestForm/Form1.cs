using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AirportIQ.IWS.Service.API;

using AirportIQ.IWS.Service.API.IwsService;    //real service

namespace AirportIQ.IWS.TestForm
{
    public partial class Form1 : Form
    {

        #region "Members"

        private IwsServiceAPI _api = null; 

        #endregion

        #region "Properties"

        public IwsServiceAPI Api
        {
            get 
            {
                if (null == _api)
                {
                    _api = new IwsServiceAPI();
                }

                return _api;
            }
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void cmdTestAPISetup_Click(object sender, EventArgs e)
        {
            lblApiSetupFeedback.Text = "";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (Api.IsAlive())
                {
                    lblApiSetupFeedback.Text = "Success";
                    lblApiSetupFeedback.ForeColor = Color.Green;
                }
                else
                {
                    lblApiSetupFeedback.Text = "Failed";
                    lblApiSetupFeedback.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblApiSetupFeedback.Text = String.Format("Failed: {0}", ex.Message);
                lblApiSetupFeedback.ForeColor = Color.Red;
            }

            this.Cursor = Cursors.Default;
        }

        private void cmdTestDbSetup_Click(object sender, EventArgs e)
        {
            lblDbSetupFeedback.Text = "";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (Api.IsDbConnected())
                {
                    lblDbSetupFeedback.Text = "Success";
                    lblDbSetupFeedback.ForeColor = Color.Green;
                }
                else
                {
                    lblDbSetupFeedback.Text = "Failed";
                    lblDbSetupFeedback.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblDbSetupFeedback.Text = String.Format("Failed: {0}", ex.Message);
                lblDbSetupFeedback.ForeColor = Color.Red;
            }

            this.Cursor = Cursors.Default;
        }

				private void cmdBGStatusTestButton_Click(object sender, EventArgs e)
				{
					rtbBCStatus.Text = "";
					this.Cursor = Cursors.WaitCursor;

					try
					{
						Guid personID = new Guid("d7031657-8fa1-45f4-a558-df53626af3b4");
						string TransactionName = "SetPerson";
						string AgencyCode = "TSA";
						string TransactionControlNumber = "fc78e44b-951b-43d8-a334-d96a153e96c8";
						DateTime TransactionDate = Convert.ToDateTime("6/10/2013 8:34:17 AM");
						string ProgramIdentification = "SD08";
						string ResponseIdentification = "";
						string TransmissionStatus = "SUCCESS";
						string TransmissionStatusText = "Message processing success";
						string XMLdata = "<ns2:com.daon.podium.mso.service.data.Acknowledgement xmlns:ns2='http://service.mso.podium.daon.com/data'><statusCode>Success</statusCode><statusText>Message processing success</statusText></ns2:com.daon.podium.mso.service.data.Acknowledgement>";

						if (Api.UpdateBackgroundCheckStatus(personID, TransactionName, AgencyCode, TransactionControlNumber, TransactionDate,
									ProgramIdentification, ResponseIdentification, TransmissionStatus, TransmissionStatusText, XMLdata, true))
						{
							rtbBCStatus.Text = "Success";
							rtbBCStatus.ForeColor = Color.Green;
						}
						else
						{
							rtbBCStatus.Text = "Failed";
							rtbBCStatus.ForeColor = Color.Red;
						}
					}
					catch (Exception ex)
					{
						rtbBCStatus.Text = String.Format("Failed: {0}", ex.Message);
						rtbBCStatus.ForeColor = Color.Red;
					}

					this.Cursor = Cursors.Default;
				}

				private void cmdBGResultTestButton_Click(object sender, EventArgs e)
				{

					rtbBDResults.Text = "";
            this.Cursor = Cursors.WaitCursor;

            try
            {
							  Guid personID = new Guid("d7031657-8fa1-45f4-a558-df53626af3b4");
							  string TransactionName = "BackgroundCheckComplete";
							  string AgencyCode = "FBI";
								string TransactionControlNumber = "fc78e44b-951b-43d8-a334-d96a153e96c8";
							  DateTime TransactionDate = Convert.ToDateTime("6/10/2013 8:34:17 AM");
					      string ProgramIdentification = "SD08";
							  string ResponseIdentification = "";
							  string TransmissionStatus = "SUCCESS";
							  string TransmissionStatusText = "Message processing success";
							  string XMLdata = "<ns2:com.daon.podium.mso.service.data.Acknowledgement xmlns:ns2='http://service.mso.podium.daon.com/data'><statusCode>Success</statusCode><statusText>Message processing success</statusText></ns2:com.daon.podium.mso.service.data.Acknowledgement>";
					      string AgencyResult = "OTHER";
							  DateTime AgencyResultDate = Convert.ToDateTime("6/10/2013 8:34:17 AM");
							  string AgencyResultDetails = "COMPLETED";
								DateTime AgencyResultDetailDate = Convert.ToDateTime("1/1/0001 12:00:00 AM");
					      string BackgroundCheckID = "TSCFP01100633";
							  string BackgroundCheckType = "CHRC";

                if (Api.UpdateBackgroundCheckResult(personID, TransactionName, AgencyCode, TransactionControlNumber, TransactionDate,
					            ProgramIdentification, ResponseIdentification, TransmissionStatus, TransmissionStatusText, XMLdata,
					            AgencyResult, AgencyResultDate, AgencyResultDetails, AgencyResultDetailDate,
					            BackgroundCheckID, BackgroundCheckType, true, ""))
                {
									rtbBDResults.Text = "Success";
									rtbBDResults.ForeColor = Color.Green;
                }
                else
                {
									rtbBDResults.Text = "Failed";
									rtbBDResults.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
							rtbBDResults.Text = String.Format("Failed: {0}", ex.Message);
							rtbBDResults.ForeColor = Color.Red;
            }

            this.Cursor = Cursors.Default;
        }

				private void Form1_Load(object sender, EventArgs e)
				{

				}

				private void label2_Click(object sender, EventArgs e)
				{

				}

				private void label3_Click(object sender, EventArgs e)
				{

				}

				private void cmdProvByBOAABadgeID_Click(object sender, EventArgs e)
				{
					rtbProvByBOAA.Text = "";
					this.Cursor = Cursors.WaitCursor;

					try
					{
						int BOAABadgeID = 4070157;
						AirportIQ.IWS.Service.API.IwsService.ProvisionData pd;

						pd = Api.ProvisionedByBOAABadgeID(BOAABadgeID);
						if (pd != null)
						{
							rtbProvByBOAA.Text = "Success";
							rtbProvByBOAA.ForeColor = Color.Green;
						}
						else
						{
							rtbProvByBOAA.Text = "Failed";
							rtbProvByBOAA.ForeColor = Color.Red;
						}
					}
					catch (Exception ex)
					{
						rtbProvByBOAA.Text = String.Format("Failed: {0}", ex.Message);
						rtbProvByBOAA.ForeColor = Color.Red;
					}

					this.Cursor = Cursors.Default;
				}

                private void rtbProvByBOAA_TextChanged(object sender, EventArgs e)
                {

                }

                private void button1_Click(object sender, EventArgs e)
                {
                    richTextBox1.Text = "";
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        if (Api.IsAlive())
                        {
                            if (Api.ExpireBadge(new Guid("233FDF7B-FCB7-4B31-C332-C4B4674CCCF2"), 4045501))
                            {
                                richTextBox1.Text = "Success";
                                richTextBox1.ForeColor = Color.Green;
                            }
                            else
                            {
                                richTextBox1.Text = "Failed";
                                richTextBox1.ForeColor = Color.Red;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.Text = String.Format("Failed: {0}", ex.Message);
                        richTextBox1.ForeColor = Color.Red;
                    }

                    this.Cursor = Cursors.Default;

                }

      private void test_fps_Click(object sender, EventArgs e)
      {
         fp_label.Text = "";
         this.Cursor = Cursors.WaitCursor;

         try
         {
            // Api.GetPerson(Guid.Parse("49BACB17-D8D5-424E-BFA0-A52D71000CA5"));
          FingerprintImages f =  Api.GetFingerprintImages(Guid.Parse("49BACB17-D8D5-424E-BFA0-A52D71000CA5"));
            //if ())
            //{
            //   fp_label.Text = "Success";
            //   fp_label.ForeColor = Color.Green;
            //}
            //else
            //{
            //   fp_label.Text = "Failed"; 
            //   fp_label.ForeColor = Color.Red;
            //}
         }
         catch (Exception ex)
         {
            fp_label.Text = String.Format("Failed: {0}", ex.Message);
            fp_label.ForeColor = Color.Red;
         }

         this.Cursor = Cursors.Default;
      }

      private void fp_label_Click(object sender, EventArgs e)
      {

      }
   }
}
