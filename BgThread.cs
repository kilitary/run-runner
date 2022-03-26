using static run_runner.Utils;
using System;
using System.Threading;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace run_runner
{
	public class ThreadWork
	{
		public static string[] cyclers = new string[] { "/", "|", "\\", "-" };
		public static string cycler = " ";
		public static string[] servicesStarting = new string[0];
		public static bool run = true;
		public static List<int> knownPids = new List<int>();

		public enum SimpleServiceCustomCommands
		{ StopWorker = 128, RestartWorker, CheckWorker };

		public static void DoWork()
		{

			ServiceController[] scServices;
			scServices = ServiceController.GetServices();

			foreach(ServiceController scTemp in scServices)
			{

				// Display properties for the Simple Service sample
				// from the ServiceBase example.
				ServiceController sc = new ServiceController(scTemp.ServiceName);
				/*Debug($"{scTemp.ServiceName} Status = " + sc.Status);
				Debug($"{scTemp.ServiceName} Can Pause and Continue = " + sc.CanPauseAndContinue);
				Debug($"{scTemp.ServiceName} Can ShutDown = " + sc.CanShutdown);
				Debug($"{scTemp.ServiceName} Can Stop = " + sc.CanStop);*/
				/*if(sc.Status == ServiceControllerStatus.Stopped)
				{
					sc.Start();
					while(sc.Status == ServiceControllerStatus.Stopped)
					{
						Thread.Sleep(1000);
						sc.Refresh();
					}
				}*/

				if(sc.Status == ServiceControllerStatus.StartPending || sc.Status == ServiceControllerStatus.StopPending)
				{
					Debug($"service {scTemp.ServiceName} шевелица ({sc.Status})");
					servicesStarting.Append(sc.DisplayName.Trim());

				}

				/*// Issue custom commands to the service
				// enum SimpleServiceCustomCommands
				//    { StopWorker = 128, RestartWorker, CheckWorker };
				sc.ExecuteCommand((int) SimpleServiceCustomCommands.StopWorker);
				sc.ExecuteCommand((int) SimpleServiceCustomCommands.RestartWorker);
				sc.Pause();
				while(sc.Status != ServiceControllerStatus.Paused)
				{
					Thread.Sleep(1000);
					sc.Refresh();
				}
				Debug("Status = " + sc.Status);
				sc.Continue();
				while(sc.Status == ServiceControllerStatus.Paused)
				{
					Thread.Sleep(1000);
					sc.Refresh();
				}
				Debug("Status = " + sc.Status);
				sc.Stop();
				while(sc.Status != ServiceControllerStatus.Stopped)
				{
					Thread.Sleep(1000);
					sc.Refresh();
				}
				Debug("Status = " + sc.Status);
				String[] argArray = new string[] { "ServiceController arg1", "ServiceController arg2" };
				sc.Start(argArray);
				while(sc.Status == ServiceControllerStatus.Stopped)
				{
					Thread.Sleep(1000);
					sc.Refresh();
				}
				Debug("Status = " + sc.Status);*/
				// Display the event log entries for the custom commands
				// and the start arguments.
				/*	EventLog el = new EventLog("Application");
					EventLogEntryCollection elec = el.Entries;
					foreach(EventLogEntry ele in elec)
					{
						if(ele.Source.IndexOf("SimpleService.OnCustomCommand") >= 0 |
							ele.Source.IndexOf("SimpleService.Arguments") >= 0)
							Debug($"msg: {ele.Message}");
					}*/
			}
			int cycleD = 0;
			int till = 40;
			string temps = "";

			while(till > 0)
			{
				if(servicesStarting.Length > 0)
				{
					temps = "services: " + string.Join(",", servicesStarting);
					Debug($"temps:{temps} count: {servicesStarting.Length}");
				}

				run_runner.Program.pForm.Invoke((MethodInvoker) (() =>
				{
					run_runner.Program.pForm.centerText.Text = $"{run_runner.Program.programName} {cycler} {temps}";
				}));

				Thread.Sleep(20);
				till -= 1;
			}

			run_runner.Program.pForm.Invoke((MethodInvoker) (() =>
			{
				run_runner.Program.pForm.centerText.Text = $"{run_runner.Program.programName} √";
			}));

			Thread.Sleep(266);

			int servicesPid = 0;
			long emptyTimeSeconds = 0;

			/*♡
	░ ♡ ▄▀▀▀▄░░♡░
	 ▄███▀░◐░░░▌░░░░░░░
		░░░░▌░░░░░▐░░░░░░░
		░░░░▐░░░░░▐░░░░░░░
		░░░░▌░░░░░▐▄▄░░░░░
		░░░░▌░░░░▄▀▒▒▀▀▀▀▄
		░░░▐░░░░▐▒▒▒▒▒▒▒▒▀▀▄
		░░░▐░░░░▐♡▄▒▒▒▒▒▒▒▒▒▀▄
		░░░░▀▄░░░░▀▄▒▒▒▒▒▒▒▒▒▒▀▄
		░░░░░░▀▄▄▄▄▄█▄▄▄▄▄▄▄▄▄▄▄▀▄
		░░░░░░░░░░░▌▌░▌▌░░░░░
		░░░░░░░░░░░▌▌░▌▌░░░░░
		░░░░░░░░░▄▄▌▌▄▌▌░░░░░*/
			int i = 0;
			while(run)
			{
				Thread.Sleep(350);

				if(GetTimestamp() >= emptyTimeSeconds + 1)
				{
					run_runner.Program.pForm.Visible = false;
				}

				Process[] processCollection = Process.GetProcesses();
				foreach(Process p in processCollection)
				{
					if(knownPids.Contains(p.Id))
						continue;

					knownPids.Add(p.Id);

					if(servicesPid == 0 && p.ProcessName.Contains("services", StringComparison.OrdinalIgnoreCase))
					{
						servicesPid = p.Id;
						Debug($"found services: {servicesPid}");
					}

					if(p.Parent()?.Id == servicesPid)
					{
						if(++cycleD >= cyclers.Length)
							cycleD = 0;
						cycler = cyclers[cycleD];

						emptyTimeSeconds = GetTimestamp();
						
						run_runner.Program.pForm.centerText.Invoke((MethodInvoker) (() =>
						{
							run_runner.Program.pForm.Visible = true;
							run_runner.Program.pForm.centerText.Text = $"{p.ProcessName} {cycler} {temps}";
						}));
					}

					//Debug($"{p.ProcessName}");
				}
			}

			Debug($"leaving run-runner");
			Environment.Exit(0);
		}
	}
}
