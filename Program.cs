using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static run_runner.Utils;

namespace run_runner
{
	internal static class Program
	{
		public static Form1? PForm;
		public static string? ProgramName;
		public static string[]? ArgsOriginal;

		[STAThread]
		static void Main(string[] args)
		{
			if(args.Length > 0)
			{
				ProgramName = args[0];
				Debug($"starting {ProgramName}");
			}

			ArgsOriginal = args;
			Debug(Jsonify(args));
			if(args.Length > 0)
			{
				if(args[0] == "-i")
				{
					var curDir = Directory.GetCurrentDirectory();
					var cmd = @$"/create /tn RunRunner /tr {curDir}\run-runner.exe /sc onlogon";

					var ret = Process.Start("schtasks.exe", cmd);
					Debug($"{cmd}: {ret.Id}");
				}
				else if(args[0] == "-u")
				{
					var cmd = "/delete /tn RunRunner /f";
					Process.Start("schtasks.exe", cmd);
				}
			}

			//ApplicationConfiguration.Initialize();
			PForm = new Form1();
			PForm.centerText.Text = "Watching system ...";
			Application.Run(PForm);
		}
	}
}