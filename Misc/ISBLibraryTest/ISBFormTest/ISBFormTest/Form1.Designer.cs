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
			this.ipAddress = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ClearButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtDisplayTrace
			// 
			this.txtDisplayTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDisplayTrace.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDisplayTrace.Location = new System.Drawing.Point(188, 12);
			this.txtDisplayTrace.Multiline = true;
			this.txtDisplayTrace.Name = "txtDisplayTrace";
			this.txtDisplayTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDisplayTrace.Size = new System.Drawing.Size(895, 501);
			this.txtDisplayTrace.TabIndex = 1;
			this.txtDisplayTrace.TextChanged += new System.EventHandler(this.textbox_changed);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(81, 15);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// ipAddress
			// 
			this.ipAddress.Location = new System.Drawing.Point(33, 59);
			this.ipAddress.Name = "ipAddress";
			this.ipAddress.Size = new System.Drawing.Size(123, 20);
			this.ipAddress.TabIndex = 3;
			this.ipAddress.Text = "192.168.60.155:44444";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(20, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "IP:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// ClearButton
			// 
			this.ClearButton.Location = new System.Drawing.Point(81, 490);
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.Size = new System.Drawing.Size(75, 23);
			this.ClearButton.TabIndex = 5;
			this.ClearButton.Text = "Clear";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1095, 525);
			this.Controls.Add(this.ClearButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ipAddress);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.txtDisplayTrace);
			this.Name = "Form1";
			this.Text = "ISBTest";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtDisplayTrace;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.TextBox ipAddress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ClearButton;
	}
}

