using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;

namespace Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrderController : AdvancedController
    {
        private readonly DataContext _dataContext;

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public OrderController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [HttpGet("~/[controller]")]
        [HttpGet(DefaultApiHttpGetTemplate)]
        public IActionResult Index()
        {
            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(_dataContext);

            List<BoughtCart> boughtCarts = boughtCartRepository.GetAll().Where(x => x.UserId == UserId).ToList();

            return ApiOrView(boughtCarts);
        }

        [HttpGet]
        public IActionResult PayForCart(long CartId)
        {
            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(_dataContext);
            BoughtCart cart = boughtCartRepository.Get(CartId);
            if(cart.UserId != UserId)
            {
                return Error(401, "Access denied.");
            }


            cart.SetPaidStatusForAllProducts(PaidStatus.Paid);
            boughtCartRepository.Update(cart);
            return Index();
        }
    }
}
