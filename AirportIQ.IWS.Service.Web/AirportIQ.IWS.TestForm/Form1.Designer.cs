namespace AirportIQ.IWS.TestForm
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
         this.label1 = new System.Windows.Forms.Label();
         this.cmdTestAPISetup = new System.Windows.Forms.Button();
         this.lblApiSetupFeedback = new System.Windows.Forms.Label();
         this.lblDbSetupFeedback = new System.Windows.Forms.Label();
         this.cmdTestDbSetup = new System.Windows.Forms.Button();
         this.label4 = new System.Windows.Forms.Label();
         this.lblTestBCResult = new System.Windows.Forms.Label();
         this.rtbBDResults = new System.Windows.Forms.RichTextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.cmdBGStatusTestButton = new System.Windows.Forms.Button();
         this.rtbBCStatus = new System.Windows.Forms.RichTextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.cmdBGResultTestButton = new System.Windows.Forms.Button();
         this.cmdProvByBOAABadgeID = new System.Windows.Forms.Button();
         this.rtbProvByBOAA = new System.Windows.Forms.RichTextBox();
         this.button1 = new System.Windows.Forms.Button();
         this.label5 = new System.Windows.Forms.Label();
         this.richTextBox1 = new System.Windows.Forms.RichTextBox();
         this.test_fps = new System.Windows.Forms.Button();
         this.fp_label = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(12, 17);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(79, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "Test API Setup";
         // 
         // cmdTestAPISetup
         // 
         this.cmdTestAPISetup.Location = new System.Drawing.Point(143, 12);
         this.cmdTestAPISetup.Name = "cmdTestAPISetup";
         this.cmdTestAPISetup.Size = new System.Drawing.Size(75, 23);
         this.cmdTestAPISetup.TabIndex = 1;
         this.cmdTestAPISetup.Text = "Test";
         this.cmdTestAPISetup.UseVisualStyleBackColor = true;
         this.cmdTestAPISetup.Click += new System.EventHandler(this.cmdTestAPISetup_Click);
         // 
         // lblApiSetupFeedback
         // 
         this.lblApiSetupFeedback.AutoSize = true;
         this.lblApiSetupFeedback.Location = new System.Drawing.Point(264, 17);
         this.lblApiSetupFeedback.Name = "lblApiSetupFeedback";
         this.lblApiSetupFeedback.Size = new System.Drawing.Size(0, 13);
         this.lblApiSetupFeedback.TabIndex = 2;
         // 
         // lblDbSetupFeedback
         // 
         this.lblDbSetupFeedback.AutoSize = true;
         this.lblDbSetupFeedback.Location = new System.Drawing.Point(264, 46);
         this.lblDbSetupFeedback.Name = "lblDbSetupFeedback";
         this.lblDbSetupFeedback.Size = new System.Drawing.Size(0, 13);
         this.lblDbSetupFeedback.TabIndex = 5;
         // 
         // cmdTestDbSetup
         // 
         this.cmdTestDbSetup.Location = new System.Drawing.Point(143, 41);
         this.cmdTestDbSetup.Name = "cmdTestDbSetup";
         this.cmdTestDbSetup.Size = new System.Drawing.Size(75, 23);
         this.cmdTestDbSetup.TabIndex = 4;
         this.cmdTestDbSetup.Text = "Test";
         this.cmdTestDbSetup.UseVisualStyleBackColor = true;
         this.cmdTestDbSetup.Click += new System.EventHandler(this.cmdTestDbSetup_Click);
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(12, 46);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(77, 13);
         this.label4.TabIndex = 3;
         this.label4.Text = "Test DB Setup";
         // 
         // lblTestBCResult
         // 
         this.lblTestBCResult.AutoSize = true;
         this.lblTestBCResult.Location = new System.Drawing.Point(13, 103);
         this.lblTestBCResult.Name = "lblTestBCResult";
         this.lblTestBCResult.Size = new System.Drawing.Size(97, 13);
         this.lblTestBCResult.TabIndex = 6;
         this.lblTestBCResult.Text = "Test BC Result call";
         // 
         // rtbBDResults
         // 
         this.rtbBDResults.BackColor = System.Drawing.SystemColors.Control;
         this.rtbBDResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.rtbBDResults.Location = new System.Drawing.Point(239, 107);
         this.rtbBDResults.Name = "rtbBDResults";
         this.rtbBDResults.Size = new System.Drawing.Size(287, 18);
         this.rtbBDResults.TabIndex = 8;
         this.rtbBDResults.Text = "";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(13, 73);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(97, 13);
         this.label2.TabIndex = 10;
         this.label2.Text = "Test BC Status call";
         this.label2.Click += new System.EventHandler(this.label2_Click);
         // 
         // cmdBGStatusTestButton
         // 
         this.cmdBGStatusTestButton.Location = new System.Drawing.Point(144, 73);
         this.cmdBGStatusTestButton.Name = "cmdBGStatusTestButton";
         this.cmdBGStatusTestButton.Size = new System.Drawing.Size(75, 22);
         this.cmdBGStatusTestButton.TabIndex = 9;
         this.cmdBGStatusTestButton.Text = "Test";
         this.cmdBGStatusTestButton.Click += new System.EventHandler(this.cmdBGStatusTestButton_Click);
         // 
         // rtbBCStatus
         // 
         this.rtbBCStatus.BackColor = System.Drawing.SystemColors.Control;
         this.rtbBCStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.rtbBCStatus.Location = new System.Drawing.Point(237, 74);
         this.rtbBCStatus.Name = "rtbBCStatus";
         this.rtbBCStatus.Size = new System.Drawing.Size(287, 27);
         this.rtbBCStatus.TabIndex = 11;
         this.rtbBCStatus.Text = "";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(14, 135);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(94, 13);
         this.label3.TabIndex = 12;
         this.label3.Text = "Test ProvByBOAA";
         this.label3.Click += new System.EventHandler(this.label3_Click);
         // 
         // cmdBGResultTestButton
         // 
         this.cmdBGResultTestButton.Location = new System.Drawing.Point(144, 103);
         this.cmdBGResultTestButton.Name = "cmdBGResultTestButton";
         this.cmdBGResultTestButton.Size = new System.Drawing.Size(75, 22);
         this.cmdBGResultTestButton.TabIndex = 0;
         this.cmdBGResultTestButton.Text = "Test";
         this.cmdBGResultTestButton.Click += new System.EventHandler(this.cmdBGResultTestButton_Click);
         // 
         // cmdProvByBOAABadgeID
         // 
         this.cmdProvByBOAABadgeID.Location = new System.Drawing.Point(144, 134);
         this.cmdProvByBOAABadgeID.Name = "cmdProvByBOAABadgeID";
         this.cmdProvByBOAABadgeID.Size = new System.Drawing.Size(75, 22);
         this.cmdProvByBOAABadgeID.TabIndex = 13;
         this.cmdProvByBOAABadgeID.Text = "Test";
         this.cmdProvByBOAABadgeID.Click += new System.EventHandler(this.cmdProvByBOAABadgeID_Click);
         // 
         // rtbProvByBOAA
         // 
         this.rtbProvByBOAA.BackColor = System.Drawing.SystemColors.Control;
         this.rtbProvByBOAA.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.rtbProvByBOAA.Location = new System.Drawing.Point(236, 137);
         this.rtbProvByBOAA.Name = "rtbProvByBOAA";
         this.rtbProvByBOAA.Size = new System.Drawing.Size(287, 18);
         this.rtbProvByBOAA.TabIndex = 14;
         this.rtbProvByBOAA.Text = "";
         this.rtbProvByBOAA.TextChanged += new System.EventHandler(this.rtbProvByBOAA_TextChanged);
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(143, 162);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 22);
         this.button1.TabIndex = 16;
         this.button1.Text = "Test";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(13, 163);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(70, 13);
         this.label5.TabIndex = 15;
         this.label5.Text = "Expire Badge";
         // 
         // richTextBox1
         // 
         this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
         this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.richTextBox1.Location = new System.Drawing.Point(239, 163);
         this.richTextBox1.Name = "richTextBox1";
         this.richTextBox1.Size = new System.Drawing.Size(287, 18);
         this.richTextBox1.TabIndex = 17;
         this.richTextBox1.Text = "";
         // 
         // test_fps
         // 
         this.test_fps.Location = new System.Drawing.Point(143, 191);
         this.test_fps.Name = "test_fps";
         this.test_fps.Size = new System.Drawing.Size(75, 23);
         this.test_fps.TabIndex = 18;
         this.test_fps.Text = "Test FP\'ed";
         this.test_fps.TextAlign = System.Drawing.ContentAlignment.TopLeft;
         this.test_fps.UseVisualStyleBackColor = true;
         this.test_fps.Click += new System.EventHandler(this.test_fps_Click);
         // 
         // fp_label
         // 
         this.fp_label.AutoSize = true;
         this.fp_label.Location = new System.Drawing.Point(228, 191);
         this.fp_label.Name = "fp_label";
         this.fp_label.Size = new System.Drawing.Size(44, 13);
         this.fp_label.TabIndex = 19;
         this.fp_label.Text = "fp_label";
         this.fp_label.Click += new System.EventHandler(this.fp_label_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(538, 233);
         this.Controls.Add(this.fp_label);
         this.Controls.Add(this.test_fps);
         this.Controls.Add(this.richTextBox1);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.rtbProvByBOAA);
         this.Controls.Add(this.cmdProvByBOAABadgeID);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.rtbBCStatus);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.cmdBGStatusTestButton);
         this.Controls.Add(this.rtbBDResults);
         this.Controls.Add(this.lblTestBCResult);
         this.Controls.Add(this.cmdBGResultTestButton);
         this.Controls.Add(this.lblDbSetupFeedback);
         this.Controls.Add(this.cmdTestDbSetup);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.lblApiSetupFeedback);
         this.Controls.Add(this.cmdTestAPISetup);
         this.Controls.Add(this.label1);
         this.Name = "Form1";
         this.Text = "Test AirportIQ API and Webservice";
         this.Load += new System.EventHandler(this.Form1_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdTestAPISetup;
        private System.Windows.Forms.Label lblApiSetupFeedback;
        private System.Windows.Forms.Label lblDbSetupFeedback;
        private System.Windows.Forms.Button cmdTestDbSetup;
				private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblTestBCResult;
		private System.Windows.Forms.RichTextBox rtbBDResults;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdBGStatusTestButton;
		private System.Windows.Forms.RichTextBox rtbBCStatus;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdBGResultTestButton;
		private System.Windows.Forms.Button cmdProvByBOAABadgeID;
		private System.Windows.Forms.RichTextBox rtbProvByBOAA;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox richTextBox1;
      private System.Windows.Forms.Button test_fps;
      private System.Windows.Forms.Label fp_label;
   }
}

