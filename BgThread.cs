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
		public static string[]? ServicesStarting;
		public static volatile bool Run = true;
		public static HashSet<int> BeforePids = new HashSet<int>();
		public static string CurrentProcessName = "<scanning>";
		public static int CycleD = 0;
		public static long LastWaker = 0;
		public static long ProgramTimeLeft = 0;
		public static long LastDraw = Stopwatch.GetTimestamp();
		public static ServiceController[]? ScServices;
		public static int ExplorerPid = 0, ServicesPid = 0;
		public static Process[]? Procs;
		public static int ProcessId = 0;
		public static bool Done = false;
		public enum SimpleServiceCustomCommands
		{ StopWorker = 128, RestartWorker, CheckWorker }

		public static void DoWork()
		{

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

            // collect existent 
            foreach (Process p in Process.GetProcesses())
                BeforePids.Add(p.Id);

            Debug($"collected {BeforePids.Count} pids");

			int i = 0;

			Procs = Process.GetProcessesByName("services");
			ServicesPid = Procs[0].Id;
			Procs = Process.GetProcessesByName("explorer");
			ExplorerPid = Procs[0].Id;

			Debug($"found services: {ServicesPid} explorer: {ExplorerPid}");
			ProcessId = 0;

            LastWaker = GetTimestamp() + 10;

			while(Run)
            {
                Draw();

				/*ScServices = ServiceController.GetServices();

				//Debug($"got {ScServices.Length} services");

				foreach(ServiceController scService in ScServices)
				{
					ServiceController sc = new ServiceController(scService.ServiceName);
					//Debug($"checking service {sc.ServiceName}");
					switch(sc.Status)
					{
						case ServiceControllerStatus.StartPending:
						case ServiceControllerStatus.StopPending:
						case ServiceControllerStatus.ContinuePending:
						case ServiceControllerStatus.PausePending:
							Debug($"service {scService.ServiceName} шевелица ({sc.Status})");
							//ServicesStarting.Append(sc.DisplayName.Trim());
							CurrentProcessName = $"<service> {scService.ServiceName}";

							break;
						default:
							Debug($"service state: {sc.Status}");
							break;
					}
				}*/

				if(GetTimestamp() - LastWaker > 9)
				{
					CurrentProcessName = $" done √ ";
					Done = true;
				}

				if(GetTimestamp() - LastWaker > 11)
					Run = false;

				/*if(GetTimestamp() - ProgramTimeLeft >= 2 && GetTimestamp() - LastWaker > 5)
				{
					CurrentProcessName = $"<scanning>";
					ProgramTimeLeft = GetTimestamp();

				}
*/
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

						//Debug($"added {p.Id} ({p.ProcessName}), total {BeforePids.Count}");

						LastWaker = GetTimestamp();
						CurrentProcessName = $"#{p.Id} {p.ProcessName}";

						ProgramTimeLeft = GetTimestamp();

						//Thread.Sleep(40);
					}

					Thread.Sleep(5);
					Draw();
				}

				Thread.Sleep(5);
			}

			Debug("leaving Run-runner");
			Environment.Exit(0);
		}

		public static void Draw()
		{
			if(Stopwatch.GetTimestamp() - LastDraw <= 1000000)
				return;

			if(CycleD >= Cyclers.Length)
				CycleD = 0;
			Cycler = Cyclers[CycleD++];

			if(Done)
				Cycler = "";

			Program.PForm.Invoke((MethodInvoker) (() =>
			{
				Program.PForm.BringToFront();
				Program.PForm.centerText.Text = $"{CurrentProcessName} {Cycler}";
				Program.PForm.Visible = true;
			}));

			LastDraw = Stopwatch.GetTimestamp();
		}
	}
}
