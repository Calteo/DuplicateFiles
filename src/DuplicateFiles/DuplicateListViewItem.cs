namespace DuplicateFiles
{
	internal class DuplicateListViewItem : ListViewItem
	{
		private bool expanded;

		public DuplicateListViewItem(FileRef fileRef)
		{
			ImageKey = "folder";
			Hash = fileRef.Hash;
			Files = [fileRef];
			Text = fileRef.Name;

			SubItems.AddRange($"{fileRef.FileInfo.Length:#,##0}");
			SubItems.AddRange("");
		}

		public string Hash { get; }
		public List<FileRef> Files { get; }
		public long Size => Files[0].FileInfo.Length;

		public bool Expanded
		{
			get => expanded;
			set
			{
				expanded = value;

				if (ListView == null)
					return;
			
				var position = ListView.Items.IndexOf(this);

				ListView.BeginUpdate();

				if (expanded)
				{
					for (var i = 0; i < Files.Count; i++)
					{
						ListView.Items.Insert(position + i + 1, new FileRefListViewItem(this, Files[i]));
					}
				}
				else
				{
					for (var i = 0; i < Files.Count; i++)
					{
						ListView.Items.RemoveAt(position + 1);
					}
				}
				ListView.EndUpdate();
			}
		}

		internal void Add(FileRef file)
		{
			Files.Add(file);

			if (file.Name != Text)
				SubItems[2].Text = @"multiple names";
		}
	}	
}
