using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static run_runner.Utils;

namespace run_runner
{

	public partial class Form1 : Form
	{
		

		public Form1()
		{
			InitializeComponent();

			

			ProcessStartInfo start = new ProcessStartInfo();
			string[] newargs = new string[25];
			Array.ConstrainedCopy(run_runner.Program.argsOriginal, 1, newargs, 0, run_runner.Program.argsOriginal.Length - 1);
			start.FileName = run_runner.Program.programName;
			start.WindowStyle = ProcessWindowStyle.Normal;
			start.Arguments = String.Join(" ", newargs);
			Process proc = Process.Start(start);

			Thread thread1 = new Thread(run_runner.ThreadWork.DoWork);
			thread1.Start();
		}

		private void centerText_Click(object sender, EventArgs e)
		{

		}
	}
}