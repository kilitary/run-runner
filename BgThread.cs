using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
