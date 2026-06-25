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

			SubItems.AddRange("1");
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

			SubItems[1].Text = Files.Count.ToString("#,##0");

			if (file.Name != Text)
				SubItems[3].Text = @"multiple names";
		}

		internal void Remove(FileRef file)
		{
			Files.Remove(file);
			SubItems[1].Text = Files.Count.ToString("#,##0");
		
			if (Files.Count == 0)
				return;

			if (Files.All(f => f.Name == Text))
				SubItems[3].Text = "";
		}
	}	
}
