using Microsoft.AspNetCore.Mvc;

namespace adnumaZ.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult HttpError(int statusCode)
        {
            if ((statusCode < 100 || statusCode > 599) || statusCode == 404)
            {
                return View("404ErrorView");
            }

            return View(statusCode);
        }
    }
}
