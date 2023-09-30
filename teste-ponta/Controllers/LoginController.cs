using Microsoft.AspNetCore.Mvc;

namespace TestePonta.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
