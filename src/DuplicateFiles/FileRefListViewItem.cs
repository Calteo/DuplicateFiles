namespace DuplicateFiles
{
	internal class FileRefListViewItem : ListViewItem
	{
		public FileRefListViewItem(DuplicateListViewItem parent, FileRef fileRef)
		{
			Parent = parent;
			Name = fileRef.FullName;

			ImageKey = "file";
			FileRef = fileRef;
			Text = fileRef.Name;
			
			this.IndentCount = 1;

			SubItems.Add("");
			SubItems.Add($"{fileRef.FileInfo.Length:#,##0}");
			SubItems.Add(fileRef.FileInfo.FullName).Name="FullPath";
		}

		public DuplicateListViewItem Parent { get; }
		public FileRef FileRef { get; }
		public string FullName => FileRef.FileInfo.FullName;
	}
}
