using static run_runner.Utils;

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
			if(args.Length > 0)
			{
				programName = args[0];
				Debug($"starting {programName}");
			}
			argsOriginal = args;
			ApplicationConfiguration.Initialize();
			pForm = new Form1();
			pForm.centerText.Text = "Watching system ...";
			Application.Run(pForm);
		}
	}
}