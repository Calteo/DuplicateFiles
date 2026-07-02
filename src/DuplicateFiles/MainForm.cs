using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using DuplicateFiles.Comparers;
using DuplicateFiles.Forms;
using Microsoft.VisualBasic.FileIO;
using Toolbox.Collection.Generics;
using Toolbox.Forms;


namespace DuplicateFiles
{
	internal partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			SortFilesListView(0, true);

			contextMenuStrip.Items.AddRange(
				new ExecutableMenuItem<string[]>("Open", PrepareOpen, ExecuteOpen),
				new ExecutableMenuItem<string[]>("Open Folder", PrepareOpenFolder, ExecuteOpenFolder),
				new ToolStripSeparator(),
				new ExecutableMenuItem<ListViewItem[]>("Ignore", PrepareIgnore, ExecuteIgnore),
				new ExecutableMenuItem<FileRefListViewItem[]>("Keep", PrepareKeep, ExecuteKeep),
				new ExecutableMenuItem<FileRefListViewItem[]>("Delete", PrepareDelete, ExecuteDelete),
				new ToolStripSeparator(),
				new ExecutableMenuItem<HashSet<string>>("Ignore folder", PrepareIgnoreFolder, ExecuteIgnoreFolder),
				new ExecutableMenuItem<HashSet<string>>("Keep folder", PrepareKeepFolder, ExecuteKeepFolder),
				new ExecutableMenuItem<HashSet<string>>("Delete folder", PrepareDeleteFolder, ExecuteDeleteFolder)
				);
		}

		private bool PrepareDeleteFolder(out HashSet<string>? data)
		{
			var folders = GetSelectedFolders();

			var completeDelete = filesListView.Items.OfType<DuplicateListViewItem>()
				.Any(d => d.Files.All(f => folders.Contains(f.FileInfo.DirectoryName ?? "")));

			data = folders;
			return !completeDelete && data.Count > 0;
		}

		private void ExecuteDeleteFolder(HashSet<string> folders)
		{
			var count = 0;

			ExecuteOnFolder(folders,
				f => count++,
				(d, f) => count++);

			var result = MsgBox.Show(this, $"Are you sure you want to delete the {count:#,##0} selected file(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			ExecuteOnFolder(folders,
				f =>
				{
					DeleteFile(f.FileRef.FileInfo.FullName);
					Remove(f);
				},
				(d, f) =>
				{
					DeleteFile(f.FileInfo.FullName);
					d.Remove(f);
					if (d.Files.Count < 2)
					{
						Remove(d);
					}
				});

		}

		private bool PrepareKeepFolder(out HashSet<string>? data)
		{
			var folders = GetSelectedFolders();

			// disallow keeping folders if any duplicate has multiple files in the selected folders
			var badKeep = filesListView.Items.OfType<DuplicateListViewItem>()
				.Any(d => d.Files.Count(f => folders.Contains(f.FileInfo.DirectoryName ?? "")) > 1);

			data = folders;
			return folders.Count> 0 && !badKeep;		
		}

		private void ExecuteKeepFolder(HashSet<string> folders)
		{
			var count = 0;

			ExecuteOnFolder(folders,
				f => count += f.Parent.Files.Count - 1,
				(d, f) => count += d.Files.Count - 1);

			var result = MsgBox.Show(this, $"Are you sure you want to delete the {count:#,##0} selected file(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			ExecuteOnFolder(folders,
				f =>
				{
					foreach (var fileRef in f.Parent.Files.Where(fr => fr != f.FileRef))
					{
						DeleteFile(fileRef.FileInfo.FullName);					
					}
					Remove(f.Parent);
				},
				(d, f) =>
				{
					foreach (var fileRef in d.Files.Where(fr => fr != f))
					{
						DeleteFile(fileRef.FileInfo.FullName);						
					}
					Remove(d);
				});
		}

		private bool PrepareIgnoreFolder(out HashSet<string>? data)
		{
			data = GetSelectedFolders();
			return data.Count> 0;
		}

		private void ExecuteIgnoreFolder(HashSet<string> folders)
		{
			ExecuteOnFolder(folders,
				f => Remove(f),
				(d, f) =>
				{
					d.Remove(f);
					if (d.Files.Count < 2)
					{
						Remove(d);
					}
				}
				);
		}

		private bool PrepareDelete(out FileRefListViewItem[]? data)
		{
			var byDuplicates = filesListView.SelectedItems.OfType<FileRefListViewItem>()
				.GroupBy(f => f.Parent)
				.Where(g => g.Count()!=g.Key.Files.Count);

			data = [.. byDuplicates.SelectMany(g => g)];
			return data.Length > 0;
		}

		private void ExecuteDelete(FileRefListViewItem[] items)
		{
			var result = MsgBox.Show(this, $"Are you sure you want to delete the {items.Length:#,##0} selected file(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			foreach (var item in items)
			{
				try
				{
					DeleteFile(item.FullPath);
					Remove(item);
				}
				catch (Exception exception)
				{
					MsgBox.Show(this, $"Failed to delete '{item.FullPath}'", "Deleting files", MessageBoxButtons.OK, MessageBoxIcon.Error, exception.ToString());
				}
			}			
		}

		private bool PrepareKeep(out FileRefListViewItem[]? data)
		{
			var fileRefs = filesListView.SelectedItems.OfType<FileRefListViewItem>().GroupBy(f => f.Parent);
			data = fileRefs.Where(g => g.Count() == 1).Select(g => g.First()).ToArray();
			return data.Length > 0;
		}

		private void ExecuteKeep(FileRefListViewItem[] items)
		{
			var duplicates = items.Select(f => f.Parent).ToArray();

			var fileRefsCount = duplicates.SelectMany(d => d.Files).Except(items.Select(f => f.FileRef)).Count();

			var result = MsgBox.Show(this, $"Are you sure you want to delete the {fileRefsCount:#,##0} selected file(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			foreach (var item in items)
			{
				try
				{
					foreach (var fileRef in item.Parent.Files.Where(f => f != item.FileRef))
					{
						DeleteFile(fileRef.FileInfo.FullName);
					}
					Remove(item.Parent);
				}
				catch (Exception exception)
				{
					MsgBox.Show(this, exception.Message, $"Deleting files for keeping '{item.Name}'.", MessageBoxButtons.OK, MessageBoxIcon.Error, exception.ToString());
				}
			}
		}

		private bool PrepareIgnore(out ListViewItem[]? data)
		{
			data = [.. filesListView.SelectedItems.OfType<ListViewItem>()];
			return data.Length > 0;
		}

		private void ExecuteIgnore(ListViewItem[] items)
		{
			Remove(items);
		}

		private bool PrepareOpenFolder(out string[]? data)
		{
			data = [.. filesListView.SelectedItems.OfType<FileRefListViewItem>().Select(f => f.FullPath)];

			return data.Length > 0;
		}

		private void ExecuteOpenFolder(string[] files)
		{
			foreach (var file in files)
			{
				try
				{
					if (File.Exists(file))
					{
						Process.Start("explorer.exe", $"/select,\"{file}\""); // open folder and select file
					}
				}
				catch (Exception ex)
				{
					MsgBox.Show(this, $"Failed to open directory for '{file}'.\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private bool PrepareOpen(out string[]? data)
		{
			data = [.. filesListView.SelectedItems.OfType<FileRefListViewItem>().Select(f => f.FullPath)];
			return data.Length > 0;
		}

		private void ExecuteOpen(string[] files)
		{
			foreach (var file in files)
			{
				try
				{
					Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
				}
				catch (Exception ex)
				{
					MsgBox.Show(this, $"Failed to open file '{file}'.\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public required Options Options { get; set; }

		private Dictionary<string, DuplicateListViewItem> Duplicates { get; } = [];
		private ConcurrentQueue<DuplicateListViewItem> DuplicatesQueue { get; } = new();

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

			while (DuplicatesQueue.TryDequeue(out var duplicateItem))
			{
				filesListView.Items.Add(duplicateItem);
			}

			textBoxFolder.Enabled = buttonSelectFolder.Enabled = buttonScan.Enabled = true;
			buttonScan.Text = "Scan";
			labelProgess.Text = "";

			filesListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			filesListView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
			filesListView.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			filesListView.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
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
				if (duplicateItem.Files.Count == 2)
				{
					DuplicatesQueue.Enqueue(duplicateItem);
				}
			}
			else
			{
				Duplicates[result.Output.Hash] = duplicateItem = new DuplicateListViewItem(result.Output);
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

			while (DuplicatesQueue.TryDequeue(out var duplicateItem))
			{
				filesListView.Items.Add(duplicateItem);
			}
		}

		private void FilesListViewDoubleClick(object sender, EventArgs e)
		{
			var point = filesListView.PointToClient(Cursor.Position);
			var hitInfo = filesListView.HitTest(point);

			if (hitInfo.Item is DuplicateListViewItem duplicateItem)
			{
				duplicateItem.Expanded = !duplicateItem.Expanded;
				filesListView.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
			}
			else if (hitInfo.Item is FileRefListViewItem fileRefItem)
			{
				try
				{
					if (hitInfo.SubItem == null) return;

					if (hitInfo.SubItem.Text == "FullPath")
						Process.Start(new ProcessStartInfo(fileRefItem.FullPath) { UseShellExecute = true });
					else
						Process.Start(new ProcessStartInfo(fileRefItem.FullPath) { UseShellExecute = true });
				}
				catch (Exception ex)
				{
					MsgBox.Show(this, $"Failed to open file '{fileRefItem.FullPath}'.\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
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
				1 => new CountComparer(ascending),
				2 => new SizeComparer(ascending),
				3 => new FullPathComparer(ascending),
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

		private void Remove(IEnumerable<ListViewItem> items)
		{
			filesListView.BeginUpdate();

			var toRemove = items.ToArray();

			toRemove.OfType<DuplicateListViewItem>().ForEach(item => Remove(item));
			toRemove.OfType<FileRefListViewItem>().ForEach(item => Remove(item));

			filesListView.EndUpdate();
		}

		private void Remove(DuplicateListViewItem item)
		{
			if (item.Expanded) item.Expanded = false;
			filesListView.Items.Remove(item);

			Duplicates.Remove(item.Hash);
		}

		void Remove(FileRefListViewItem item)
		{
			if (item.ListView == null) return;

			var duplicateItem = item.Parent;

			duplicateItem.Remove(item.FileRef);

			filesListView.Items.Remove(item);

			if (duplicateItem.Files.Count < 2)
			{
				Remove(duplicateItem);
			}
		}

		private void ContextMenuStripOpening(object sender, CancelEventArgs e)
		{
			foreach (var command in contextMenuStrip.Items.OfType<IExecutableMenuItem>())
				command.Prepare();
		}

		private HashSet<string> GetSelectedFolders()
		{
			return filesListView.SelectedItems
				.OfType<FileRefListViewItem>()
				.Select(f => f.FileRef.FileInfo.DirectoryName)
				.OfType<string>()
				.ToHashSet();
		}

		private void ExecuteOnFolder(HashSet<string> folders, Action<FileRefListViewItem> fileRefAction, Action<DuplicateListViewItem, FileRef> duplicateFileRefAction)
		{
			filesListView.BeginUpdate();

			foreach (var fileItem in filesListView.Items.OfType<FileRefListViewItem>())
			{
				if (fileItem.FileRef.FileInfo.Directory != null && folders.Contains(fileItem.FileRef.FileInfo.Directory.FullName))
				{
					fileRefAction(fileItem);
				}
			}

			foreach (var duplicate in filesListView.Items.OfType<DuplicateListViewItem>().Where(d => !d.Expanded))
			{
				var removeRefs = duplicate.Files
					.Where(f => f.FileInfo.Directory != null && folders.Contains(f.FileInfo.Directory.FullName))
					.ToArray();

				foreach (var fileRef in removeRefs)
				{
					duplicateFileRefAction(duplicate, fileRef);
				}
			}

			filesListView.EndUpdate();
		}

		private void RemoveFolders(Action<FileRefListViewItem> action1, Action<DuplicateListViewItem, FileRef> action2)
		{
			var folders = filesListView.SelectedItems
				.OfType<FileRefListViewItem>()
				.Select(f => f.FileRef.FileInfo.Directory?.FullName)
				.Where(d => d != null)
				.ToHashSet();

			filesListView.BeginUpdate();

			foreach (var fileItem in filesListView.Items.OfType<FileRefListViewItem>())
			{
				if (fileItem.FileRef.FileInfo.Directory != null && folders.Contains(fileItem.FileRef.FileInfo.Directory.FullName))
				{
					action1(fileItem);
				}
			}

			filesListView.EndUpdate();

			foreach (var duplicate in filesListView.Items.OfType<DuplicateListViewItem>().Where(d => !d.Expanded))
			{
				var removeRefs = duplicate.Files
					.Where(f => f.FileInfo.Directory != null && folders.Contains(f.FileInfo.Directory.FullName))
					.ToArray();

				foreach (var fileRef in removeRefs)
				{
					action2(duplicate, fileRef);
				}
			}
		}

		private void KeepFolderCommandClick(object sender, EventArgs e)
		{
		}

		private void DeleteFolderCommandClick(object sender, EventArgs e)
		{
			var count = 0;

			RemoveFolders(
				f => count++,
				(d, f) => count++);

			var result = MsgBox.Show(this, $"Are you sure you want to delete the {count:#,##0} selected file(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			RemoveFolders(
				f =>
				{
					DeleteFile(f.FileRef.FileInfo.FullName);
					Remove(f);
				},
				(d, f) =>
				{
					DeleteFile(f.FileInfo.FullName);
					d.Remove(f);
					if (d.Files.Count < 2)
					{
						Remove(d);
					}
				});
		}

		private void DeleteFile(string filname) => FileSystem.DeleteFile(filname, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);

		private void ContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem is IExecutableMenuItem command)
				command.Execute();
		}
	}
}
