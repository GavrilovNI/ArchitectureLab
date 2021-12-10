using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ErrorController : AdvancedController
    {
        public string Index(int code, string message)
        {
            Response.StatusCode = code;
            return message;
        }
    }
}
