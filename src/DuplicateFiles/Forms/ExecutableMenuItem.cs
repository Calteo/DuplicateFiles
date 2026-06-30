using System;
using System.Text;

namespace DuplicateFiles.Forms
{
	internal delegate bool PrepareExecuteHandler<T>(out T? data);

	internal interface IExecutableMenuItem
	{
		void Prepare();
		void Execute();
	}

	internal class ExecutableMenuItem<T> : ToolStripMenuItem, IExecutableMenuItem
	{
		public ExecutableMenuItem(string text, PrepareExecuteHandler<T> prepare, Action<T> execute) : base(text)
		{
			PrepareHandler = prepare;
			ExecuteHandler = execute;
		}

		private T? _data;
		private PrepareExecuteHandler<T> PrepareHandler { get; }
		private Action<T> ExecuteHandler { get; }

		public void Execute()
		{
			if (Enabled && _data != null)
				ExecuteHandler(_data);		
		}

		public void Prepare()
		{
			Enabled = PrepareHandler(out _data);
		}

	}
}
