using hmwk49.data;
using hmwk49.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hmwk49.web.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People; Integrated Security=true;";
        public IActionResult Index()
        {
            PeopleManager manager = new(connectionString);
            PersonViewModel viewModel = new()
            {
                People = manager.GetPeople()
            };
            if (TempData["Message"] != null)
            {
                viewModel.Message = (string)TempData["Message"];
            }

            return View(viewModel);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(List<Person> people)
        {
            PeopleManager manager = new(connectionString);
            manager.AddMany(people);
            var correctGrammer = people.Count == 1 ? "Person was" : "People were";
            TempData["Message"] = $"{people.Count} {correctGrammer} added";

            return Redirect("/home/Index");
        }


    }
}