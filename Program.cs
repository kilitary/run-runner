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
			ApplicationConfiguration.Initialize();
			PForm = new Form1();
			PForm.centerText.Text = "Watching system ...";
			Application.Run(PForm);
		}
	}
}