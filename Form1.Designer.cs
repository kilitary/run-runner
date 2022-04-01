using System.ComponentModel;

namespace run_runner
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// centerText
			// 
			this.centerText.BackColor = System.Drawing.Color.Black;
			this.centerText.CausesValidation = false;
			this.centerText.Font = new System.Drawing.Font("Liberation Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.centerText.ForeColor = System.Drawing.Color.DarkOrange;
			this.centerText.Location = new System.Drawing.Point(110, 23);
			this.centerText.Name = "centerText";
			this.centerText.Size = new System.Drawing.Size(472, 22);
			this.centerText.TabIndex = 0;
			this.centerText.Text = "centerText";
			this.centerText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.centerText.UseCompatibleTextRendering = true;
			this.centerText.Click += new System.EventHandler(this.centerText_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Source Code Pro", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(12, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Booting:";
			// 
			// Form1
			// 
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.BackgroundImage = global::run_runner.Properties.Resources.E2ABOCSWYAAnMiy;
			this.ClientSize = new System.Drawing.Size(610, 64);
			this.Controls.Add(this.centerText);
			this.Controls.Add(this.label1);
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.TopMost = true;
			this.Click += new System.EventHandler(this.Form1_Click);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public Label centerText;
		private Label label1;
	}
}