using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Models;
using Web.Jwt;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = AuthOptions.AuthSchemes)]
    [Route("[controller]/[action]")]
    public class ImageController : AdvancedController
    {
        [AllowAnonymous]
        [HttpGet("~/[controller]/{path}")]
        public IActionResult Get(string? path)
        {
            if (path == null)
            {
                return List();
            }

            var image = System.IO.File.OpenRead("wwwroot\\img\\" + path);
            return File(image, "image/jpeg");
        }

        [HttpGet]
        [HttpGet(DefaultApiHttpGetTemplate)]
        public IActionResult List()
        {
            return ApiOrView(Directory.GetFiles("wwwroot\\img\\uploaded", "*.*", SearchOption.AllDirectories).Select(x => x.Split('\\', 2)[1]).ToList());
        }

        [HttpGet]
        [HttpGet(DefaultApiHttpGetTemplate)]
        public IActionResult Add()
        {
            return ApiOrView(new Image());
        }

        [HttpPost(Name = "Add")]
        public async Task<IActionResult> Add(Image image)
        {
            string path = Path.Combine("wwwroot\\img\\uploaded", image.Path??image.ImageFile.FileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.ImageFile.CopyToAsync(fileStream);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
