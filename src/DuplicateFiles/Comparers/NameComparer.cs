using System.Collections;

namespace DuplicateFiles.Comparers
{
	internal class NameComparer(bool ascending) : ListViewComparer(ascending)
	{
		protected override int Compare(FileRefListViewItem fileRefX, FileRefListViewItem fileRefY)
		{
			return string.Compare(fileRefX.Text, fileRefY.Text, StringComparison.OrdinalIgnoreCase);
		}

		protected override int Compare(DuplicateListViewItem duplicateX, DuplicateListViewItem duplicateY)
		{
			return string.Compare(duplicateX.Text, duplicateY.Text, StringComparison.OrdinalIgnoreCase);
		}
	}
}