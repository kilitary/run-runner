﻿using System.ComponentModel;

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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// centerText
			// 
			this.centerText.BackColor = System.Drawing.Color.Black;
			this.centerText.CausesValidation = false;
			this.centerText.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.centerText.ForeColor = System.Drawing.Color.DarkOrange;
			this.centerText.Location = new System.Drawing.Point(110, 14);
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
			this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "BOOTING";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Black;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.ForeColor = System.Drawing.Color.Aquamarine;
			this.label2.Location = new System.Drawing.Point(554, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "dismiss";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Black;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.ForeColor = System.Drawing.Color.DarkCyan;
			this.label3.Location = new System.Drawing.Point(565, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "axis9";
			// 
			// Form1
			// 
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.BackgroundImage = global::run_runner.Properties.Resources.E2ABOCSWYAAnMiy;
			this.ClientSize = new System.Drawing.Size(610, 50);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.centerText);
			this.Controls.Add(this.label1);
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ss";
			this.TopMost = true;
			this.Click += new System.EventHandler(this.Form1_Click);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public Label centerText;
		private Label label1;
		private Label label2;
		private Label label3;
	}
}