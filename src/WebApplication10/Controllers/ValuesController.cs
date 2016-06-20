using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebApplication10.Models;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net;

namespace WebApplication10.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static Dictionary<int, string> paths = new Dictionary<int, string> { };
        static Dictionary<int, MyDirectories> cache = new Dictionary<int, MyDirectories> { };

        // GET api/values
        [HttpGet]
        public MyDirectories Get()
        {
            MyDirectories d = new MyDirectories();
            d.GetRoot();
            foreach (var dir in d.Children)
            {
                paths.Add(dir.Key, dir.Value[0]);
            }
            cache.Add(d.CurrentDir.GetHashCode(), d);
            return d;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public MyDirectories Get(int id)
        {
            MyDirectories d = new MyDirectories();

            if (cache.TryGetValue(id, out d))
            {
                return d;
            }
            else
            {
                d = new MyDirectories();
                d.CurrentDir = paths[id];

                if (Directory.GetParent(d.CurrentDir) != null)
                {
                    d.ParentDir = (Directory.GetParent(d.CurrentDir).FullName).GetHashCode();
                }
                else
                {
                    string r = "root";
                    d.ParentDir = r.GetHashCode();
                }

                try
                {
                    foreach (var item in Directory.EnumerateDirectories(d.CurrentDir))
                    {
                        d.Children.Add(item.GetHashCode(), new List<string> { item, item.Substring(item.LastIndexOf(@"\") + 1) });
                    }
                   
                    foreach (var item in Directory.EnumerateFiles(d.CurrentDir))
                    {
                        d.Files.Add(item.Substring(item.LastIndexOf(@"\") + 1));
                    }
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }

                d.CountFilesSize(d.CurrentDir);
                d.key = d.CurrentDir.GetHashCode();

                foreach (var dir in d.Children)
                {
                    paths.Add(dir.Key, dir.Value[0]);
                }
                cache.Add(d.CurrentDir.GetHashCode(), d);

                return d;

            }



        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
