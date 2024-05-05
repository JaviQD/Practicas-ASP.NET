using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Practicas_ASP.NET.Methods;
using Practicas_ASP.NET.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practicas_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly Jwt _jwt;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, Jwt jwt)
        {
            _logger = logger;
            _jwt = jwt;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTime()
        {
            DateTime time = DateTime.Now;
            string FormattedTime = time.ToString("yyyy-MM-dd");

            // string Response = "{\"Time\": \"" + time + "\"}";
            //return Json(Response);

            var response = new { Time = FormattedTime };
            return Json(response);
 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCredentials credentials)
        {

            // Si las credenciales son v�lidas, se crea un JWT
            var token = _jwt.GenerarToken(credentials.UserName, credentials.Password);

            if(token != null)
                return Ok(new { token });

            else
                return Unauthorized();

        }
    }
}
