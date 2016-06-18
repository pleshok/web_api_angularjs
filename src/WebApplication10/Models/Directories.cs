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
        public Dictionary<int, string> Children { get; set; }
        public List<string> Files { get; set; }
        public int Sf_count { get; set; }
        public int Mf_count { get; set; }
        public int Lf_count { get; set; }

        public MyDirectories()
        {
            Children = new Dictionary<int, string>();
        }
    }
}
