using System.Diagnostics;

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
	}
}