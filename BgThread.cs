using System.Diagnostics;
using System.ServiceProcess;
using static run_runner.Utils;

namespace run_runner
{
	public class ThreadWork
	{
		public static string[] Cyclers = { "/", "|", "\\", "-" };
		public static string Cycler = " ";
		public static string[] ServicesStarting = new string[1];
		public static bool Run = true;
		public static List<int> KnownPids = new List<int>();

		public enum SimpleServiceCustomCommands
		{ StopWorker = 128, RestartWorker, CheckWorker }

		public static void DoWork()
		{

			ServiceController[] scServices = null;
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
					ServicesStarting.Append(sc.DisplayName.Trim());

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

			int till = 40;
			while(till > 0)
			{
				if(ServicesStarting.Length > 0)
				{
					string allServices = "services: " + string.Join(",", ServicesStarting);
					Debug($"temps:{allServices} count: {ServicesStarting.Length}");
				}

				Program.pForm.Invoke((MethodInvoker) (() =>
				{
					Program.pForm.centerText.Text = $"{Program.programName} {Cycler}";
				}));

				Thread.Sleep(20);
				till -= 1;
			}

			Program.pForm.Invoke((MethodInvoker) (() =>
			{
				Program.pForm.centerText.Text = $"{Program.programName} √";
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
			string currentProcessName = "<scanning>";
			int cycleD = 0;

			var procs = Process.GetProcessesByName("explorer");
			servicesPid = procs[0].Id;
			Debug($"found services: {servicesPid}");

			while(Run)
			{
				if(GetTimestamp() >= emptyTimeSeconds - 1)
					Program.pForm.Invoke((MethodInvoker) (() => { Program.pForm.Visible = false; }));
				else
					Program.pForm.Invoke((MethodInvoker) (() => { Program.pForm.Visible = true; }));

				foreach(Process p in Process.GetProcesses())
				{
					if(p.Parent()?.Id == servicesPid)
					{
						if(cycleD >= Cyclers.Length)
							cycleD = 0;
						Cycler = Cyclers[cycleD++];

						Program.pForm.Invoke((MethodInvoker) (() => { Program.pForm.centerText.Text = $"{currentProcessName} {Cycler}"; }));

						if(KnownPids.Contains(p.Id))
							continue;
						else
						{
							KnownPids.Add(p.Id);
							Debug($"added {p.Id}");
							emptyTimeSeconds = GetTimestamp();
							currentProcessName = $"#{p.Id} {p.ProcessName}";
							Program.pForm.centerText.Invoke((MethodInvoker) (() => { Program.pForm.Visible = true; }));
						}
					}

					Thread.Sleep(55);

					/*if(currentProcessName.Length == 0)
						continue;*/


				}

				Thread.Sleep(150);
			}

			Debug("leaving Run-runner");
			Environment.Exit(0);
		}
	}
}
