using System.Collections;
using System.ComponentModel;
using System.Security.Cryptography;
using DuplicateFiles.Comparers;
using Toolbox.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DuplicateFiles
{
	internal partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			SortFilesListView(0, true);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public required Options Options { get; set; }

		private Dictionary<string, DuplicateListViewItem> Duplicates { get; } = [];

		private void MainFormShown(object sender, EventArgs e)
		{
			if (Options.Folder != null)
			{
				textBoxFolder.Text = Path.GetFullPath(Options.Folder);
			}
		}

		private void ButtonSelectFolderClick(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = textBoxFolder.Text;
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				textBoxFolder.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void WorkerPoolStarted(object sender, EventArgs e)
		{
			textBoxFolder.Enabled = buttonSelectFolder.Enabled = false;
			buttonScan.Text = "Abort";
			timer.Start();
		}

		private void WorkerPoolStopped(object sender, EventArgs e)
		{
			timer.Stop();
			textBoxFolder.Enabled = buttonSelectFolder.Enabled = buttonScan.Enabled = true;
			buttonScan.Text = "Scan";
			labelProgess.Text = "";

			filesListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			filesListView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			filesListView.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void TextBoxFolderTextChanged(object sender, EventArgs e)
		{
			buttonScan.Enabled = Directory.Exists(Folder);
		}

		private int _hashed;
		private int _pending;

		private string Folder
		{
			get
			{
				var folder = textBoxFolder.Text;
				if (folder.EndsWith(':')) folder += Path.DirectorySeparatorChar;
				return Path.GetFullPath(folder);
			}
		}

		private void ButtonScanClick(object sender, EventArgs e)
		{
			if (workerPool.Running)
			{
				buttonScan.Enabled = false;
				buttonScan.Text = "Aborting...";
				workerPool.Cancel();
				return;
			}

			filesListView.Items.Clear();
			Duplicates.Clear();

			_hashed = 0;
			_pending = 0;

			Enqueue(new DirectoryInfo(Folder));
		}

		private void Enqueue(DirectoryInfo directory)
		{
			workerPool.Enqueue<DirectoryInfo>(Scan, directory);
		}

		private void Enqueue(FileInfo file)
		{
			Interlocked.Increment(ref _pending);
			workerPool.Enqueue<FileInfo, FileRef>(CreateFileRef, file, OnFileRefCreated);
		}

		private FileRef CreateFileRef(IWorker worker, FileInfo file)
		{
			var fileRef = new FileRef(file, GetHash(file, SHA256.Create()));

			Interlocked.Decrement(ref _pending);
			Interlocked.Increment(ref _hashed);

			return fileRef;
		}
		private void OnFileRefCreated(IWorkResult<FileInfo, FileRef> result)
		{
			if (Duplicates.TryGetValue(result.Output.Hash, out var duplicateItem))
			{
				duplicateItem.Add(result.Output);
			}
			else
			{
				Duplicates[result.Output.Hash] = duplicateItem = new DuplicateListViewItem(result.Output);
				filesListView.Items.Add(duplicateItem);
			}
		}

		private void Scan(IWorker worker, DirectoryInfo directory)
		{
			var foundFiles = new Dictionary<long, HashSet<FileInfo>>();

			var options = new EnumerationOptions
			{
				IgnoreInaccessible = true,
				RecurseSubdirectories = true,
				ReturnSpecialDirectories = false
			};
			foreach (var file in directory.EnumerateFiles("*", options))
			{
				if (foundFiles.TryGetValue(file.Length, out var fileInfos))
				{
					fileInfos.Add(file);
				}
				else
				{
					foundFiles[file.Length] = [file];
				}
			}

			foreach (var files in foundFiles.Values.Where(h => h.Count > 1))
			{
				foreach (var file in files)
				{
					Enqueue(file);
				}
			}
		}

		private string GetHash(FileInfo fileInfo, SHA256 sha256)
		{
			using var stream = fileInfo.OpenRead();
			var hash = sha256.ComputeHash(stream);
			return Convert.ToHexString(hash);
		}

		private void TimerTick(object sender, EventArgs e)
		{
			labelProgess.Text = $"Hashed {_hashed:#,##0} files, {_pending:#,##0} pending...";
		}

		private void FilesListViewDoubleClick(object sender, EventArgs e)
		{
			var point = filesListView.PointToClient(Cursor.Position);
			var hitInfo = filesListView.HitTest(point);

			if (hitInfo.Item is DuplicateListViewItem duplicateItem)
			{
				duplicateItem.Expanded = !duplicateItem.Expanded;
			}
		}

		private void FilesListViewColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (SortColumnIndex == e.Column)
				SortFilesListView(e.Column, SortColumnIndex != e.Column ? true : !SortOrderAscending);
			else
				SortFilesListView(e.Column, true);
		}

		private int SortColumnIndex { get; set; } = -1;
		private bool SortOrderAscending { get; set; } = true;
		private void SortFilesListView(int columnIndex, bool ascending)
		{
			ListViewComparer comparer = columnIndex switch
			{
				0 => new NameComparer(ascending),
				1 => new SizeComparer(ascending),
				2 => new FullPathComparer(ascending),
				_ => throw new ArgumentOutOfRangeException(nameof(columnIndex))
			};

			filesListView.ListViewItemSorter = comparer;
			filesListView.Sort();

			if (SortColumnIndex >= 0)
			{
				filesListView.Columns[SortColumnIndex].Text = filesListView.Columns[SortColumnIndex].Text.TrimEnd(' ', '▲', '▼');
			}
		
			SortColumnIndex = columnIndex;
			SortOrderAscending = ascending;

			filesListView.Columns[SortColumnIndex].Text += ascending ? " ▲" : " ▼";
		}
	}
}
