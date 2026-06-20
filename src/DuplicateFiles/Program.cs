using System.Web;
using Toolbox.CommandLine;
using Toolbox.Forms;

namespace DuplicateFiles
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{			
			var parser = Parser.Create<Options>();
			var result = parser.Parse(args);

			result
				.OnError(OnError)
				.OnHelp(OnHelp)
				.On<Options>(Run);
		}

		private static int Run(Options options)
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new MainForm { Options = options });

			return 0;
		}

		private static int OnHelp(ParseResult result)
		{
			MsgBox.Show(null, result.GetHelpText(), "DuplicateFiles - Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
			return 0;
		}


		private static int OnError(ParseResult result)
		{
			MsgBox.Show(null, result.Text, "DuplicateFiles - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return 1;
		}
	}
}