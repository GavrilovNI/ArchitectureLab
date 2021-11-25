using Microsoft.AspNetCore.Mvc;
using Web.Data.Interfaces;
using Web.Data.Models;

namespace Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly IAllCars _allCars;
        private readonly ICarsCategory _carsCategory;

        public CarsController(IAllCars allCars, ICarsCategory carsCategory)
        {
            _allCars = allCars;
            _carsCategory = carsCategory;
        }

        [Route("")]
        [Route("Cars/List")]
        public ViewResult List()
        {
            ViewBag.Title = "Это тайтл";
            var cars = _allCars.Cars;
            return View(new SomeModel() { cars = cars, Name = "its name"});
        }

        //[HttpGet]
        [Route("Cars/Test")]
        //[Route("Cars/Test/{id}")]
        public ActionResult Test(int id)
        {
            Console.Out.WriteLine(id);
            return View();
            //return RedirectToAction("List");
        }

        public ActionResult TestPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TestPost(SomeModel model)
        {
            bool valid = ModelState.IsValid;
            return View();
        }
    }
}
