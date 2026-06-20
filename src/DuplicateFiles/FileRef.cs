using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateFiles
{
	internal class FileRef(FileInfo fileInfo, string hash) 
	{
		public FileInfo FileInfo { get; } = fileInfo;
		public string Hash { get; set; } = hash;
		public string Name => FileInfo.Name;
	}
}
