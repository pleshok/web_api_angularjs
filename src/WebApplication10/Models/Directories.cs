using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace WebApplication10.Models
{
    public class MyDirectories
    {
        public string CurrentDir { get; set; }
        public string ParentDir { get; set; }
        public Dictionary<int, List<string>> Children { get; set; }
        public List<string> Files { get; set; }
        public int Sf_count { get; set; }
        public int Mf_count { get; set; }
        public int Lf_count { get; set; }

        public MyDirectories()
        {
            Children = new Dictionary<int, List<string>>();
        }

        public void GetRoot()
        {
            this.Children.Clear();
            this.CurrentDir = "root";
            if (this.Files != null)
                this.Files.Clear();
            int k=0;
            foreach (var s in Directory.GetLogicalDrives().ToList())
            {
                this.Children.Add(k, new List<string> {s, s});
                k++;
            }
        }

        public void CountFilesSize(string path)
        {
            long small = 10485760;
            long medium = 52428800;
            long large = 104857600;
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                IEnumerable<FileInfo> fi = di.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);
                foreach (var file in fi)
                {
                    if (file.Length <= small)
                        this.Sf_count++;
                    else if (small < file.Length && file.Length <= medium)
                        this.Mf_count++;
                    else if (file.Length > large)
                        this.Lf_count++;
                }
                try
                {
                    foreach (var dir in Directory.EnumerateDirectories(path))
                    {
                        this.CountFilesSize(dir);
                    }
                }
                catch (Exception) { }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
        }
    }
}