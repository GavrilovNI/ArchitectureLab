using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;

namespace Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public OrderController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            BoughtProductRepository boughtProductRepository = new BoughtProductRepository(_dataContext);

            List<BoughtProduct> boughtProducts = boughtProductRepository.GetAll().Where(x => x.UserId == UserId).ToList();

            return View(boughtProducts);
        }
    }
}
