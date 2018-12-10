namespace ISBFormTest
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtDisplayTrace = new System.Windows.Forms.TextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.docm_ip_address = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ClearButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.cms_ip_address = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.idms_ip_address = new System.Windows.Forms.TextBox();
			this.AllCMSTests = new System.Windows.Forms.Button();
			this.RunAuditTest = new System.Windows.Forms.Button();
			this.RunDOCM = new System.Windows.Forms.Button();
			this.IDMSTest = new System.Windows.Forms.Button();
			this.cms_card_id = new System.Windows.Forms.TextBox();
			this.ShowCMSProvButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.ShowBOAAProvButton = new System.Windows.Forms.Button();
			this.boaa_badge_id = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.CMSSyncButton = new System.Windows.Forms.Button();
			this.AuditThisBadgeButton = new System.Windows.Forms.Button();
			this.ShowCMSButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.GetPersonDocumentsButton = new System.Windows.Forms.Button();
			this.IWSPersonGUID = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.TransactionControlNumber = new System.Windows.Forms.TextBox();
			this.ReTransmitButton = new System.Windows.Forms.Button();
			this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
			this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.GetPersonPortraitButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtDisplayTrace
			// 
			this.txtDisplayTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDisplayTrace.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDisplayTrace.Location = new System.Drawing.Point(256, 9);
			this.txtDisplayTrace.Multiline = true;
			this.txtDisplayTrace.Name = "txtDisplayTrace";
			this.txtDisplayTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDisplayTrace.Size = new System.Drawing.Size(809, 665);
			this.txtDisplayTrace.TabIndex = 1;
			this.txtDisplayTrace.TextChanged += new System.EventHandler(this.textbox_changed);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(41, 15);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(149, 23);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "Run All Tests";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// docm_ip_address
			// 
			this.docm_ip_address.Location = new System.Drawing.Point(67, 44);
			this.docm_ip_address.Name = "docm_ip_address";
			this.docm_ip_address.Size = new System.Drawing.Size(123, 20);
			this.docm_ip_address.TabIndex = 3;
			this.docm_ip_address.Text = "192.168.16.12:8080";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "DOCM:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// ClearButton
			// 
			this.ClearButton.Location = new System.Drawing.Point(115, 651);
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.Size = new System.Drawing.Size(75, 23);
			this.ClearButton.TabIndex = 5;
			this.ClearButton.Text = "Clear";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(32, 93);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "CMS:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cms_ip_address
			// 
			this.cms_ip_address.Location = new System.Drawing.Point(67, 90);
			this.cms_ip_address.Name = "cms_ip_address";
			this.cms_ip_address.Size = new System.Drawing.Size(123, 20);
			this.cms_ip_address.TabIndex = 6;
			this.cms_ip_address.Text = "192.168.16.12:8080";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(28, 70);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "IDMS:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// idms_ip_address
			// 
			this.idms_ip_address.Location = new System.Drawing.Point(67, 67);
			this.idms_ip_address.Name = "idms_ip_address";
			this.idms_ip_address.Size = new System.Drawing.Size(123, 20);
			this.idms_ip_address.TabIndex = 8;
			this.idms_ip_address.Text = "192.168.16.12:8080";
			// 
			// AllCMSTests
			// 
			this.AllCMSTests.Location = new System.Drawing.Point(41, 120);
			this.AllCMSTests.Name = "AllCMSTests";
			this.AllCMSTests.Size = new System.Drawing.Size(149, 23);
			this.AllCMSTests.TabIndex = 10;
			this.AllCMSTests.Text = "Run All CMS Tests";
			this.AllCMSTests.UseVisualStyleBackColor = true;
			this.AllCMSTests.Click += new System.EventHandler(this.AllCMSTests_Click);
			// 
			// RunAuditTest
			// 
			this.RunAuditTest.Location = new System.Drawing.Point(41, 147);
			this.RunAuditTest.Name = "RunAuditTest";
			this.RunAuditTest.Size = new System.Drawing.Size(149, 23);
			this.RunAuditTest.TabIndex = 11;
			this.RunAuditTest.Text = "Run Audit Test";
			this.RunAuditTest.UseVisualStyleBackColor = true;
			this.RunAuditTest.Click += new System.EventHandler(this.RunAuditTest_Click);
			// 
			// RunDOCM
			// 
			this.RunDOCM.Location = new System.Drawing.Point(41, 174);
			this.RunDOCM.Name = "RunDOCM";
			this.RunDOCM.Size = new System.Drawing.Size(149, 23);
			this.RunDOCM.TabIndex = 12;
			this.RunDOCM.Text = "Run All DOCM Tests";
			this.RunDOCM.UseVisualStyleBackColor = true;
			this.RunDOCM.Click += new System.EventHandler(this.RunDOCM_Click);
			// 
			// IDMSTest
			// 
			this.IDMSTest.Location = new System.Drawing.Point(41, 202);
			this.IDMSTest.Name = "IDMSTest";
			this.IDMSTest.Size = new System.Drawing.Size(149, 23);
			this.IDMSTest.TabIndex = 13;
			this.IDMSTest.Text = "Run All IDMS Tests";
			this.IDMSTest.UseVisualStyleBackColor = true;
			this.IDMSTest.Click += new System.EventHandler(this.IDMSTest_Click);
			// 
			// cms_card_id
			// 
			this.cms_card_id.Location = new System.Drawing.Point(108, 443);
			this.cms_card_id.Name = "cms_card_id";
			this.cms_card_id.Size = new System.Drawing.Size(123, 20);
			this.cms_card_id.TabIndex = 14;
			this.cms_card_id.Text = "3992312";
			// 
			// ShowCMSProvButton
			// 
			this.ShowCMSProvButton.Location = new System.Drawing.Point(41, 465);
			this.ShowCMSProvButton.Name = "ShowCMSProvButton";
			this.ShowCMSProvButton.Size = new System.Drawing.Size(149, 23);
			this.ShowCMSProvButton.TabIndex = 15;
			this.ShowCMSProvButton.Text = "Show CMS Prov Data";
			this.ShowCMSProvButton.UseVisualStyleBackColor = true;
			this.ShowCMSProvButton.Click += new System.EventHandler(this.ShowCMSProvButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 446);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "CMS Card ID";
			// 
			// ShowBOAAProvButton
			// 
			this.ShowBOAAProvButton.Location = new System.Drawing.Point(41, 536);
			this.ShowBOAAProvButton.Name = "ShowBOAAProvButton";
			this.ShowBOAAProvButton.Size = new System.Drawing.Size(149, 23);
			this.ShowBOAAProvButton.TabIndex = 17;
			this.ShowBOAAProvButton.Text = "Show BOAA Prov Data";
			this.ShowBOAAProvButton.UseVisualStyleBackColor = true;
			this.ShowBOAAProvButton.Click += new System.EventHandler(this.ShowBOAAProvButton_Click);
			// 
			// boaa_badge_id
			// 
			this.boaa_badge_id.Location = new System.Drawing.Point(108, 511);
			this.boaa_badge_id.Name = "boaa_badge_id";
			this.boaa_badge_id.Size = new System.Drawing.Size(123, 20);
			this.boaa_badge_id.TabIndex = 18;
			this.boaa_badge_id.Text = "4069676";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 514);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 13);
			this.label5.TabIndex = 19;
			this.label5.Text = "BOAA Badge ID";
			// 
			// CMSSyncButton
			// 
			this.CMSSyncButton.Location = new System.Drawing.Point(41, 563);
			this.CMSSyncButton.Name = "CMSSyncButton";
			this.CMSSyncButton.Size = new System.Drawing.Size(149, 23);
			this.CMSSyncButton.TabIndex = 20;
			this.CMSSyncButton.Text = "Tell CMS To Get Prov Data";
			this.CMSSyncButton.UseVisualStyleBackColor = true;
			this.CMSSyncButton.Click += new System.EventHandler(this.PokeCMS_Click);
			// 
			// AuditThisBadgeButton
			// 
			this.AuditThisBadgeButton.Location = new System.Drawing.Point(41, 589);
			this.AuditThisBadgeButton.Name = "AuditThisBadgeButton";
			this.AuditThisBadgeButton.Size = new System.Drawing.Size(149, 23);
			this.AuditThisBadgeButton.TabIndex = 21;
			this.AuditThisBadgeButton.Text = "Audit this Badge";
			this.AuditThisBadgeButton.UseVisualStyleBackColor = true;
			this.AuditThisBadgeButton.Click += new System.EventHandler(this.AuditThisBadgeButton_Click);
			// 
			// ShowCMSButton
			// 
			this.ShowCMSButton.Location = new System.Drawing.Point(41, 614);
			this.ShowCMSButton.Name = "ShowCMSButton";
			this.ShowCMSButton.Size = new System.Drawing.Size(149, 23);
			this.ShowCMSButton.TabIndex = 22;
			this.ShowCMSButton.Text = "Show CMS Prov Data";
			this.ShowCMSButton.UseVisualStyleBackColor = true;
			this.ShowCMSButton.Click += new System.EventHandler(this.ShowCMSButton_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 339);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(94, 13);
			this.label6.TabIndex = 25;
			this.label6.Text = "IWS Person GUID";
			// 
			// GetPersonDocumentsButton
			// 
			this.GetPersonDocumentsButton.Location = new System.Drawing.Point(41, 374);
			this.GetPersonDocumentsButton.Name = "GetPersonDocumentsButton";
			this.GetPersonDocumentsButton.Size = new System.Drawing.Size(149, 23);
			this.GetPersonDocumentsButton.TabIndex = 24;
			this.GetPersonDocumentsButton.Text = "Get Person Signature";
			this.GetPersonDocumentsButton.UseVisualStyleBackColor = true;
			this.GetPersonDocumentsButton.Click += new System.EventHandler(this.GetPersonDocumentsButton_Click);
			// 
			// IWSPersonGUID
			// 
			this.IWSPersonGUID.Location = new System.Drawing.Point(26, 352);
			this.IWSPersonGUID.Name = "IWSPersonGUID";
			this.IWSPersonGUID.Size = new System.Drawing.Size(225, 20);
			this.IWSPersonGUID.TabIndex = 23;
			this.IWSPersonGUID.Text = "BAFA4BE0-2DD3-4D91-A33A-271BF9F22B65";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 256);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(119, 13);
			this.label7.TabIndex = 26;
			this.label7.Text = "IWS Tx Control Number";
			// 
			// TransactionControlNumber
			// 
			this.TransactionControlNumber.Location = new System.Drawing.Point(26, 272);
			this.TransactionControlNumber.Name = "TransactionControlNumber";
			this.TransactionControlNumber.Size = new System.Drawing.Size(225, 20);
			this.TransactionControlNumber.TabIndex = 27;
			this.TransactionControlNumber.Text = "8267c048-ed00-4e40-a3c1-2bc8c5049757";
			// 
			// ReTransmitButton
			// 
			this.ReTransmitButton.Location = new System.Drawing.Point(41, 295);
			this.ReTransmitButton.Name = "ReTransmitButton";
			this.ReTransmitButton.Size = new System.Drawing.Size(149, 23);
			this.ReTransmitButton.TabIndex = 28;
			this.ReTransmitButton.Text = "ReTransmit";
			this.ReTransmitButton.UseVisualStyleBackColor = true;
			this.ReTransmitButton.Click += new System.EventHandler(this.ReTransmitButton_Click);
			// 
			// shapeContainer1
			// 
			this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
			this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.shapeContainer1.Name = "shapeContainer1";
			this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape4,
            this.lineShape3,
            this.lineShape2,
            this.lineShape1});
			this.shapeContainer1.Size = new System.Drawing.Size(1077, 689);
			this.shapeContainer1.TabIndex = 29;
			this.shapeContainer1.TabStop = false;
			// 
			// lineShape4
			// 
			this.lineShape4.Name = "lineShape4";
			this.lineShape4.X1 = 15;
			this.lineShape4.X2 = 231;
			this.lineShape4.Y1 = 498;
			this.lineShape4.Y2 = 498;
			// 
			// lineShape3
			// 
			this.lineShape3.Name = "lineShape3";
			this.lineShape3.X1 = 16;
			this.lineShape3.X2 = 232;
			this.lineShape3.Y1 = 431;
			this.lineShape3.Y2 = 431;
			// 
			// lineShape2
			// 
			this.lineShape2.Name = "lineShape2";
			this.lineShape2.X1 = 17;
			this.lineShape2.X2 = 233;
			this.lineShape2.Y1 = 329;
			this.lineShape2.Y2 = 329;
			// 
			// lineShape1
			// 
			this.lineShape1.Name = "lineShape1";
			this.lineShape1.X1 = 18;
			this.lineShape1.X2 = 234;
			this.lineShape1.Y1 = 241;
			this.lineShape1.Y2 = 241;
			// 
			// GetPersonPortraitButton
			// 
			this.GetPersonPortraitButton.Location = new System.Drawing.Point(41, 400);
			this.GetPersonPortraitButton.Name = "GetPersonPortraitButton";
			this.GetPersonPortraitButton.Size = new System.Drawing.Size(149, 23);
			this.GetPersonPortraitButton.TabIndex = 30;
			this.GetPersonPortraitButton.Text = "Get Person Portrait";
			this.GetPersonPortraitButton.UseVisualStyleBackColor = true;
			this.GetPersonPortraitButton.Click += new System.EventHandler(this.GetPersonPortraitButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1077, 689);
			this.Controls.Add(this.GetPersonPortraitButton);
			this.Controls.Add(this.ReTransmitButton);
			this.Controls.Add(this.TransactionControlNumber);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.GetPersonDocumentsButton);
			this.Controls.Add(this.IWSPersonGUID);
			this.Controls.Add(this.ShowCMSButton);
			this.Controls.Add(this.AuditThisBadgeButton);
			this.Controls.Add(this.CMSSyncButton);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.boaa_badge_id);
			this.Controls.Add(this.ShowBOAAProvButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.ShowCMSProvButton);
			this.Controls.Add(this.cms_card_id);
			this.Controls.Add(this.IDMSTest);
			this.Controls.Add(this.RunDOCM);
			this.Controls.Add(this.RunAuditTest);
			this.Controls.Add(this.AllCMSTests);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.idms_ip_address);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cms_ip_address);
			this.Controls.Add(this.ClearButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.docm_ip_address);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.txtDisplayTrace);
			this.Controls.Add(this.shapeContainer1);
			this.Name = "Form1";
			this.Text = "ISBTest";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtDisplayTrace;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.TextBox docm_ip_address;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ClearButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox cms_ip_address;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox idms_ip_address;
		private System.Windows.Forms.Button AllCMSTests;
		private System.Windows.Forms.Button RunAuditTest;
		private System.Windows.Forms.Button RunDOCM;
		private System.Windows.Forms.Button IDMSTest;
		private System.Windows.Forms.TextBox cms_card_id;
		private System.Windows.Forms.Button ShowCMSProvButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button ShowBOAAProvButton;
		private System.Windows.Forms.TextBox boaa_badge_id;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button CMSSyncButton;
		private System.Windows.Forms.Button AuditThisBadgeButton;
		private System.Windows.Forms.Button ShowCMSButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button GetPersonDocumentsButton;
		private System.Windows.Forms.TextBox IWSPersonGUID;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox TransactionControlNumber;
		private System.Windows.Forms.Button ReTransmitButton;
		private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
		private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
		private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
		private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
		private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
		private System.Windows.Forms.Button GetPersonPortraitButton;
	}
}

