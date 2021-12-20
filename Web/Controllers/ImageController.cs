using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]/[action]")]
    public class ImageController : AdvancedController
    {
        [HttpGet]
        [HttpGet("~/[controller]")]
        public IActionResult Index()
        {
            return View(Directory.GetFiles("wwwroot\\img\\uploaded", "*.*", SearchOption.AllDirectories).Select(x => x.Split('\\', 2)[1]).ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Image());
        }

        [HttpPost]
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
