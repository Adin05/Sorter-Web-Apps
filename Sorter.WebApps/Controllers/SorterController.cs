using Microsoft.AspNetCore.Mvc;
using Sorter.WebApps.Models;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace Sorter.WebApps.Controllers
{
    public class SorterController : Controller
    {
        public IActionResult Index(bool isDESC)
        {
            var nameViewModel = new List<NameViewModel>();
            var listName = new List<string>(){
                "Orson Milka Iddins",
                "Erna Dorey Battelle",
                "Chaunce Franzel",
                "Odetta Sue Kaspar",
                "Roy Ketti Kopfen",
                "Madel Bordie Mapplebeck",
                "Selle Bellison Flori",
                "Leonerd Adda Mitchell Monaghan",
                "Debra Micheli",
                "Hailey Avie Annakin"

            };

            nameViewModel = SortedFunc(listName, isDESC);

            return View(nameViewModel);
        }

        public List<NameViewModel> SortedFunc(List<string> listName, bool sortDESC = false)
        {
            var listOfLastName = new List<string>();
            var listNewSortName = new List<NameViewModel>();
            foreach (var name in listName)
            {
                var names = name.Split(' ');
                listOfLastName.Add(names[names.Length - 1]);
            }
            listOfLastName.Sort();

            if (sortDESC)
            {
                listOfLastName = listOfLastName.OrderByDescending(x => x).ToList();
            }

            var result = listName.Where(m => m.Contains("Micheli")).Select(m => m).FirstOrDefault();

            foreach (var lastName in listOfLastName)
            {
                var name = listName.Where(m => m.Contains(lastName)).Select(m => new NameViewModel { Name = m }).First();
                listNewSortName.Add(name);
            }

            //using (StreamWriter writer = new StreamWriter("./Download/listName.txt"))
            //{
            //    foreach (var item in listNewSortName)
            //    {
            //        writer.WriteLine(item.Name);
            //    }
            //}

            return listNewSortName;
        }

        public IActionResult Save(string nameViewModels)
        {
            using (StreamWriter writer = new StreamWriter("./Download/listName.txt"))
            {
                foreach (var item in nameViewModels.Split(','))
                {
                    writer.WriteLine(item);
                }
            }
            return NoContent();
        }
    }
}
