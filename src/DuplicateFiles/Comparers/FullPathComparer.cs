namespace DuplicateFiles.Comparers
{
	internal class FullPathComparer(bool ascending) : ListViewComparer(ascending)
	{
		protected override int Compare(FileRefListViewItem fileRefX, FileRefListViewItem fileRefY)
		{
			return string.Compare(fileRefX.FullName, fileRefY.FullName, StringComparison.OrdinalIgnoreCase);
		}
		protected override int Compare(DuplicateListViewItem duplicateX, DuplicateListViewItem duplicateY)
		{
			return string.Compare(duplicateX.Name, duplicateY.Name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
