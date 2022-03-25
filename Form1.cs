using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static run_runner.Utils;

namespace run_runner
{
	public class ThreadWork
	{
		public static void DoWork()
		{
			Thread.Sleep(1500);
			Debug($"leaving run-runner");
			Environment.Exit(0);
		}
	}
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			centerText.Text = $"Loading {run_runner.Program.programName} ...";

			ProcessStartInfo start = new ProcessStartInfo();
			string[] newargs = new string[25];
			Array.ConstrainedCopy(run_runner.Program.argsOriginal, 1, newargs, 0, run_runner.Program.argsOriginal.Length - 1);
			start.FileName = run_runner.Program.programName;
			start.WindowStyle = ProcessWindowStyle.Normal;
			start.Arguments = String.Join(" ", newargs);
			//start.CreateNoWindow = false;
			int exitCode;



			// Run the external process & wait for it to finish
			using(Process proc = Process.Start(start))
			{
				//proc.WaitForExit();

				// Retrieve the app's exit code
				//exitCode = proc.ExitCode;
			}

			Thread thread1 = new Thread(ThreadWork.DoWork);
			thread1.Start();
		}

		private void centerText_Click(object sender, EventArgs e)
		{

		}
	}
}