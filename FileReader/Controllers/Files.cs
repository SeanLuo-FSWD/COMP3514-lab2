using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using FileReader.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileReader.Controllers
{
    public class Files : Controller
    {
        private IHostingEnvironment Environment;

        public List<TextFile> list;

        public Files(IHostingEnvironment _environment)
        {
            Environment = _environment;
            list = new List<TextFile>();
            string[] filepaths = Directory.GetFiles(Path.Combine(Environment.WebRootPath, "../TextFiles"));

            foreach (string filepath in filepaths)
            {
                //Console.WriteLine(readFile(filepath));

                list.Add(new TextFile { name = Path.GetFileNameWithoutExtension(filepath), content = readFile(filepath) });
            }

        } 

        private string readFile(string filepath)
        {
            var content = "";

            using (StreamReader sr = System.IO.File.OpenText(filepath))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    content = content + s;
                }
            }

            return content;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(list);
        }


        public IActionResult Content(string id)
        {
            TextFile item = null;

            for (int i = 0; i < list.Count; i++)
            {

                if (list[i].name == id)
                {
                    item = list[i];
                }
            }
            return View(item);
        }
    }
}
