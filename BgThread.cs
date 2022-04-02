﻿using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using Newtonsoft.Json.Linq;
using static run_runner.Utils;

namespace run_runner
{
	public class ThreadWork
	{
		public static ServiceController[]? ScServices = null;
		public static Process[]? Procs = null;
		public static Dictionary<string, string> ServicesState = new();
		public static HashSet<int> BeforePids = new();
		public static string[] Cyclers = { "|", "/", "-", "\\" };
		public static string Cycler = " ";
		public static string[]? ServicesStarting;
		public static string CurrentProcessName = "<scanning>";
		public static bool Run = true;
		public static int CycleD = 0;
		public static int ProcessId = 0;
		public static int ExplorerPid = 0, ServicesPid = 0;
		public static long LastWaker = 0;
		public static long ProgramTimeLeft = 0;
		public static long LastDraw = 0;
		public static long StartTime = 0;

		public static bool Done = false;
		public enum SimpleServiceCustomCommands
		{ StopWorker = 128, RestartWorker, CheckWorker }

		public static void DoWork()
		{
			Debug(Stopwatch.IsHighResolution
				? "Operations timed using the system's high-resolution performance counter."
				: "Operations timed using the DateTime class.");

			StartTime = GetTimestamp();
			LastDraw = Stopwatch.GetTimestamp();

			/*
			 ♡
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
		        ░░░░░░░░░▄▄▌▌▄▌▌░░░░░
			*/

			CurrentProcessName = "scanning processes";
			// collect existent 
			foreach(Process p in Process.GetProcesses())
				BeforePids.Add(p.Id);

			Debug($"collected {BeforePids.Count} pids");


			Draw();
			CurrentProcessName = "scanning services";
			ScServices = ServiceController.GetServices();
			foreach(ServiceController scService in ScServices)
			{
				ServiceController sc = new ServiceController(scService.ServiceName);
				ServicesState[sc.ServiceName] = sc.Status.ToString();
			}

			Debug($"got {ServicesState.Count} services");

			int i = 0;

			Procs = Process.GetProcessesByName("services");
			ServicesPid = Procs[0].Id;
			Procs = Process.GetProcessesByName("explorer");
			ExplorerPid = Procs[0].Id;

			Debug($"found services: {ServicesPid} explorer: {ExplorerPid}");
			ProcessId = 0;

			LastWaker = GetTimestamp() + 10;
			CurrentProcessName = $"up: {GetSystemUpTimeInfo()} analyzing";

			while(Run)
			{
				Draw();

				ScServices = ServiceController.GetServices();
				foreach(ServiceController scService in ScServices)
				{
					ServiceController sc = new ServiceController(scService.ServiceName);
					if(!ServicesState.ContainsKey(sc.ServiceName))
					{
						CurrentProcessName = $"<service> {sc.ServiceName}: {sc.Status.ToString()}";
						ServicesState[sc.ServiceName] = sc.Status.ToString();
						Debug($"added {sc.ServiceName} {sc.ServiceName}");
					}

					if(ServicesState.ContainsKey(sc.ServiceName) &&
						ServicesState[sc.ServiceName] != sc.Status.ToString())
					{
						Debug($"changed {sc.ServiceName} {sc.ServiceName} => {sc.Status.ToString()}");
						CurrentProcessName = $"<service> {sc.ServiceName}: {sc.Status.ToString()}";
						ServicesState[sc.ServiceName] = sc.Status.ToString();
					}
				}

				if(GetTimestamp() - LastWaker > 4 && !Done)
				{
					var seconds = GetTimestamp() - StartTime;
					CurrentProcessName = $"takes {seconds} seconds √";
					Done = true;
				}


				if(GetTimestamp() - LastWaker > 7)
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
						//Debug($"error listing for {p.Id}: {e.Message}");
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



				Thread.Sleep(25);
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
