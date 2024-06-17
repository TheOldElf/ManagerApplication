using Microsoft.AspNetCore.Mvc;

namespace ManagerApplication.Mvc.Controllers
{
    public abstract class ManagerBaseController : Controller
    {

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
