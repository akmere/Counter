using Concepter;
using Concepter.Droid;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelperAndroid1))]
namespace Concepter.Droid
{
    public class FileHelperAndroid1 : IFileHelper1
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(path, filename);
        }
    }
}