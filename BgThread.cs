using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using Newtonsoft.Json.Linq;
using static run_runner.Utils;

namespace run_runner
{
	public class ThreadWork
	{
		public static string[] Cyclers = { "|", "/", "-", "\\" };
		public static string Cycler = " ";
		public static string[] ServicesStarting = new string[1];
		public static bool Run = true;
		public static List<int> BeforePids = new List<int>();
		public static string CurrentProcessName = "<scanning>";
		public static int CycleD = 0;
		public static long LastInvoked = 999999999999999999;
		public static long LastShift = GetTimestamp();
		public static int ExplorerPid = 0, ServicesPid = 0;
		public static Process[]? Procs;
		public static int ProcessId = 0;
		public static bool Done = false;
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

				Program.PForm.Invoke((MethodInvoker) (() => { Program.PForm.centerText.Text = $"{Program.ProgramName} {Cycler}"; }));

				Thread.Sleep(20);
				till -= 1;
			}

			Program.PForm.Invoke((MethodInvoker) (() => { Program.PForm.centerText.Text = $"{Program.ProgramName} √"; }));

			Thread.Sleep(66);



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


			Procs = Process.GetProcessesByName("services");
			ServicesPid = Procs[0].Id;
			Procs = Process.GetProcessesByName("explorer");
			ExplorerPid = Procs[0].Id;

			Debug($"found services: {ServicesPid} explorer: {ExplorerPid}");
			ProcessId = 0;

			// collect existent 
			foreach(Process p in Process.GetProcesses())
				BeforePids.Add(p.Id);

            Debug($"collected {BeforePids.Count} pids");

			while(Run)
			{
				if(Stopwatch.GetTimestamp() - LastShift >= 2211250)
					Draw();

				if(GetTimestamp() - LastInvoked > 7)
				{
					Program.PForm.Invoke((MethodInvoker) (() =>
					{
						CurrentProcessName = $" done √";
						Done = true;
						Program.PForm.Visible = true;
						Draw();
					}));

				}

				if(GetTimestamp() - LastInvoked > 10)
					Run = false;

				foreach(Process p in Process.GetProcesses())
				{
					try
					{
						var parentProcess = ParentProcess.GetParentProcess(p.Id);
						ProcessId = parentProcess.Id;
					}
					catch(Exception e)
					{
						Debug($"error listing for {p.Id}: {e.Message}");
					}

					if(ProcessId == ServicesPid || ProcessId == ExplorerPid)
					{
						if(BeforePids.Contains(p.Id))
							continue;

						BeforePids.Add(p.Id);

						Debug($"added {p.Id} ({p.ProcessName}), total {BeforePids.Count}");

						LastInvoked = GetTimestamp();
						CurrentProcessName = $"{p.ProcessName} #{p.Id}";


						Thread.Sleep(440);
					}

                    Thread.Sleep(10);
					Draw();
				}

				Thread.Sleep(250);
			}

			Debug("leaving Run-runner");
			Environment.Exit(0);
		}

		public static void Draw()
		{
			if(CycleD >= Cyclers.Length)
				CycleD = 0;
			Cycler = Cyclers[CycleD++];

			if(Done)
				Cycler = "";

			Program.PForm.Invoke((MethodInvoker) (() =>
			{
				Program.PForm.centerText.Text = $"{CurrentProcessName} {Cycler}";
				Program.PForm.Visible = true;
			}));

			LastShift = Stopwatch.GetTimestamp();
		}
	}
}
