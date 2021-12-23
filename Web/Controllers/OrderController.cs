using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;
using Web.Jwt;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = AuthOptions.AuthSchemes)]
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

            List<BoughtCart> boughtCarts = boughtCartRepository.GetAll()
                                                               .Include(c => c.BoughtProducts)
                                                               .ThenInclude(p => p.Product)
                                                               .Where(c => c.UserId == UserId)
                                                               .ToList();

            return ApiOrView(boughtCarts);
        }

        [HttpGet]
        public IActionResult PayForOrder(long cartId)
        {
            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(_dataContext);
            BoughtCart cart = boughtCartRepository.Get(cartId);
            if(cart.UserId != UserId)
            {
                return Error(HttpStatusCode.Unauthorized, "Access denied.");
            }

            if(cart.IsFullyPaid())
            {
                return Error(HttpStatusCode.BadRequest, "Order is fully paid.");
            }


            cart.SetPaidStatusForAllProducts(PaidStatus.Paid);
            boughtCartRepository.Update(cart);
            return LocalRedirect("~/Order");
        }
    }
}
