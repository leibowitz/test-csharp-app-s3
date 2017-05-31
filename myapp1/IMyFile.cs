using System;

namespace myapp1
{
	public interface IMyFile
	{
		PCLStorage.IFile GetFile(string path);
	}
}
