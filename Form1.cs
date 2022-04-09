using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace run_runner
{

	public partial class Form1 : Form
	{
		

		public Form1()
		{
			InitializeComponent();

			ProcessStartInfo start = new ProcessStartInfo();
			string[] newargs = new string[25];
			if(Program.ArgsOriginal.Length > 0)
			{
				Array.ConstrainedCopy(Program.ArgsOriginal, 1, newargs, 0, Program.ArgsOriginal.Length - 1);
				start.FileName = Program.ProgramName;
				start.WindowStyle = ProcessWindowStyle.Normal;
				start.Arguments = String.Join(" ", newargs);
				Process proc = Process.Start(start);
			}

            this.ShowInTaskbar = false;
            this.TopMost = true;

			Thread thread1 = new Thread(ThreadWork.DoWork);
			thread1.Start();
		}

		private void centerText_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void label2_Click(object sender, EventArgs e)
		{
            Environment.Exit(0);
		}

		private void label2_MouseEnter(object sender, EventArgs e)
        {
            
            label2.ForeColor = Color.GreenYellow;
		}

		private void label2_MouseLeave(object sender, EventArgs e)
		{
            label2.ForeColor = Color.Aquamarine;
		}
	}
}