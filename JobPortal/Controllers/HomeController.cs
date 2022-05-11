using JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace JobPortal.Controllers
{
    public class HomeController : Controller
    {

        // GET: HomeController
        public ActionResult Index()
        {
            ViewBag.UserTypeId = HttpContext.Session.GetInt32("UserTypeId");
            return View();
        }
        public ActionResult AdminHome()
        {

            return View();
        }
        // GET: HomeController/Details/5
       
      
    }
}
