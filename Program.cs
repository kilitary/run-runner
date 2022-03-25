using static run_runner.Utils;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Threading;
using System;

namespace run_runner
{
	internal static class Program
	{
		public static Form1? pForm;
		public static string? programName;
		public static string[]? argsOriginal;
		[STAThread]
		static void Main(string[] args)
		{
			programName = args[0];
			Debug($"starting {programName}");
			argsOriginal = args;
			ApplicationConfiguration.Initialize();
			pForm = new Form1();
			//pForm.centerText.Text = $"Running {programName} ...";
			Application.Run(pForm);

		}
	}
}