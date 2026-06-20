using System.Collections;

namespace DuplicateFiles.Comparers
{
	internal abstract class ListViewComparer : IComparer
	{
		private int Order { get; } = 1;

		public ListViewComparer(bool ascending)
		{
			if (!ascending)
				Order = -1;
		}

		public int Compare(object? x, object? y)
		{
			if (x == null && y == null) return 0;
			if (x == null) return -Order;
			if (y == null) return Order;

			if (x is FileRefListViewItem fileRefX && y is FileRefListViewItem fileRefY)
			{
				if (fileRefX.Parent == fileRefY.Parent)
					return Order * Compare(fileRefX, fileRefY);

				return Order * Compare(fileRefX.Parent, fileRefY.Parent);
			}
			else if (x is DuplicateListViewItem duplicateXY && y is FileRefListViewItem fileRefYX)
			{
				if (duplicateXY == fileRefYX.Parent)
					return -1;
				return Order * Compare(duplicateXY, fileRefYX.Parent);
			}
			else if (x is FileRefListViewItem fileRefXY && y is DuplicateListViewItem duplicateYX)
			{
				if (fileRefXY.Parent == duplicateYX)
					return 1;
				return Order * Compare(fileRefXY.Parent, duplicateYX);
			}
			else if (x is DuplicateListViewItem duplicateX && y is DuplicateListViewItem duplicateY)
			{
				return Order * Compare(duplicateX, duplicateY);
			}

			throw new NotImplementedException("ListViewComparer.Compare is not implemented yet.");
		}

		protected abstract int Compare(FileRefListViewItem fileRefX, FileRefListViewItem fileRefY);
		protected abstract int Compare(DuplicateListViewItem duplicateX, DuplicateListViewItem duplicateY);
	}
}
