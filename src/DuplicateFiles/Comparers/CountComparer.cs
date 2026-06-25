using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateFiles.Comparers
{
	internal class CountComparer(bool ascending) : ListViewComparer(ascending)
	{
		protected override int Compare(FileRefListViewItem fileRefX, FileRefListViewItem fileRefY)
		{
			return string.Compare(fileRefX.Text, fileRefY.Text, StringComparison.OrdinalIgnoreCase);
		}

		protected override int Compare(DuplicateListViewItem duplicateX, DuplicateListViewItem duplicateY)
		{
			return duplicateX.Files.Count.CompareTo(duplicateY.Files.Count);
		}
	}
}
