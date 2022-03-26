using System.ServiceProcess;
using static run_runner.Utils;

namespace run_runner
{
	public class ThreadWork
	{
		public static void DoWork()
		{

			ServiceController[] services = ServiceController.GetServices();

			Thread.Sleep(1500);
			Debug($"leaving run-runner");
			Environment.Exit(0);
		}
	}
}
