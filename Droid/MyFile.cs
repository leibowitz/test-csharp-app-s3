using myapp1.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyFile))]
namespace myapp1.Droid
{
	public class MyFile : IMyFile
	{
		public PCLStorage.IFile GetFile(string path)
		{
			return PCLStorage.FileSystem.Current.GetFileFromPathAsync(path).Result;

		}

	}
}