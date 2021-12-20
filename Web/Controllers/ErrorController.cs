using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ErrorController : AdvancedController
    {
        [HttpGet]
        [HttpGet("~/[controller]")]
        public string Index(int code, string message)
        {
            Response.StatusCode = code;
            return message;
        }
    }
}
