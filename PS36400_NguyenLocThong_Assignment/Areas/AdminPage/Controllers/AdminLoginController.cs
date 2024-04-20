using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace PS36400_NguyenLocThong_Assignment.Areas.Admin.Controllers
{
    [Area("AdminPage")]
    [Route("Admin/{controller}/{action}")]
    public class AdminLoginController : Controller
    {
        private readonly MobilePhoneContext db;

        public AdminLoginController(MobilePhoneContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","AdminTrangChu");
            }
        }

        [HttpPost]
        public IActionResult AdminLogin(Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var data = db.Admins.Where(s => s.Email.Equals(admin.Email) && s.MatKhau.Equals(admin.MatKhau)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    HttpContext.Session.SetString("AdminEmail", data.FirstOrDefault().Email);
                    HttpContext.Session.SetString("AdminName", data.FirstOrDefault().HoTen);
                    return RedirectToAction("Index", "AdminTrangChu");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }


        public IActionResult AdminLogout()
        {
            HttpContext.Session.Remove("AdminEmail");
            HttpContext.Session.Remove("AdminName");
            return RedirectToAction("Index", "AdminTrangChu");
        }
    }
}
