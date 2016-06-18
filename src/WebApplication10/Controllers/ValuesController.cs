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

        static MyDirectories d = new MyDirectories();

        // GET api/values
        [HttpGet]
        public MyDirectories Get()
        {
            int key = 0;
            d.Children.Clear();
            d.CurrentDir = "root";
            foreach (var s in Directory.GetLogicalDrives().ToList())
            {
                d.Children.Add(key, s);
                key++;
                
            }
            return d;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public MyDirectories Get(int id)
        {
            d.Sf_count = 0;
            d.Mf_count = 0;
            d.Lf_count = 0;

            string path;
            if (id == -1)
            {
                if(Directory.GetParent(d.CurrentDir)!=null && d.CurrentDir!="root")
                {
                    path = Directory.GetParent(d.CurrentDir).FullName;
                }
                else {
                    int k = 0;
                    d.Children.Clear();
                    d.Files.Clear();
                    foreach (var s in Directory.GetLogicalDrives().ToList())
                    {
                        d.Children.Add(k, s);
                        k++;
                    }

                    d.CurrentDir = "root";
                    return d;
                }

            }
            else { path = d.Children[id]; }

            d.Children.Clear();
            d.Files = new List<string>();
            int key = 0;

            try
            {
                foreach (var item in Directory.EnumerateDirectories(path))
                {
                    //d.Children.Add(item.Substring(path.Length));
                    d.Children.Add(key, item);
                    key++;
                }

                foreach (var item in Directory.EnumerateFiles(path))
                {
                    d.Files.Add(item.Substring(path.Length));
                }
            }
            catch (IOException io_ex) { }
            catch (UnauthorizedAccessException ua_ex) { }
            
            d.CountFilesSize(path);
            d.CurrentDir = path;
            if (Directory.GetParent(path) != null)
                d.ParentDir = Directory.GetParent(path).FullName;

            return d;
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
