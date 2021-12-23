using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Data.Models;
using Web.Jwt;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = AuthOptions.AuthSchemes)]
    [Route("[controller]/[action]")]
    public class ImageController : AdvancedController
    {
        public ImageController(DataContext dataContext, UserManager<User> userManager, SignInManager<User> signInManager) : base(dataContext, userManager, signInManager)
        {
        }

        [AllowAnonymous]
        [HttpGet("~/[controller]/{path}")]
        public IActionResult Get(string? path)
        {
            if (path == null)
            {
                return List();
            }

            FileStream image;
            try
            {
                image = System.IO.File.OpenRead("wwwroot\\img\\" + path);
            }
            catch (FileNotFoundException ex)
            {
                return Error(HttpStatusCode.BadRequest, "File not found.");
            }
            return File(image, "image/jpeg");
        }

        [HttpGet]
        public IActionResult List()
        {
            return ApiOrView(Directory.GetFiles("wwwroot\\img\\uploaded", "*.*", SearchOption.AllDirectories).Select(x => x.Split('\\', 2)[1]).ToList());
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public async Task<IActionResult> List([FromBody] LoginModel loginModel)
        {
            var userId = await GetUserId(loginModel, "Admin");
            if (userId == null)
                return Unauthorized();

            return List();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return ApiOrView(new Image());
        }

        [AllowAnonymous]
        [HttpPost(Name = "Add")]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public async Task<IActionResult> Add(Image image, [FromBody] LoginModel loginModel)
        {
            var userId = await GetUserId(loginModel, "Admin");
            if (userId == null)
                return Unauthorized();

            string path = Path.Combine("wwwroot\\img\\uploaded", image.Path??image.ImageFile.FileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.ImageFile.CopyToAsync(fileStream);
            }

            return LocalRedirectApi("~/Image/List");
        }

    }
}
