using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Toolbox.CommandLine;

namespace DuplicateFiles
{
	internal class Options
	{
		[Option("folder"), Description("folder to scan"), Position(0)]
		public string? Folder { get; set; }
	}
}
