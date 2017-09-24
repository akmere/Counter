using Projecter;
using Projecter.Droid;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelperAndroid1))]
namespace Projecter.Droid
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