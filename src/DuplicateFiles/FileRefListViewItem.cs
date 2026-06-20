using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateFiles
{
	internal class FileRefListViewItem : ListViewItem
	{
		public FileRefListViewItem(DuplicateListViewItem parent, FileRef fileRef)
		{
			Parent = parent;

			ImageKey = "file";
			FileRef = fileRef;
			Text = fileRef.Name;
			
			this.IndentCount = 1;

			SubItems.AddRange($"{fileRef.FileInfo.Length:#,##0}");
			SubItems.Add(fileRef.FileInfo.FullName);			
		}

		public DuplicateListViewItem Parent { get; }
		public FileRef FileRef { get; }
		public string FullPath => FileRef.FileInfo.FullName;
	}
}
