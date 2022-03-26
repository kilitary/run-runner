namespace run_runner
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.centerText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// centerText
			// 
			this.centerText.BackColor = System.Drawing.Color.Gold;
			this.centerText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.centerText.CausesValidation = false;
			this.centerText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.centerText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.centerText.Font = new System.Drawing.Font("Source Code Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.centerText.ForeColor = System.Drawing.Color.Black;
			this.centerText.Location = new System.Drawing.Point(0, 0);
			this.centerText.Name = "centerText";
			this.centerText.Size = new System.Drawing.Size(382, 41);
			this.centerText.TabIndex = 0;
			this.centerText.Text = "centerText";
			this.centerText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.centerText.UseCompatibleTextRendering = true;
			this.centerText.Click += new System.EventHandler(this.centerText_Click);
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(382, 41);
			this.Controls.Add(this.centerText);
			this.Cursor = System.Windows.Forms.Cursors.No;
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.TopMost = true;
			this.ResumeLayout(false);

		}

		#endregion

		public Label centerText;
	}
}