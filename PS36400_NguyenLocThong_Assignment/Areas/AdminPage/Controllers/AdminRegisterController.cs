using Microsoft.AspNetCore.Mvc;
using PS36400_NguyenLocThong_Assignment.Models;

namespace PS36400_NguyenLocThong_Assignment.Areas.AdminPage.Controllers
{
    [Area("AdminPage")]
    [Route("Admin/{controller}/{action}")]
    public class AdminRegisterController : Controller
    {
        private readonly MobilePhoneContext db;

        public AdminRegisterController(MobilePhoneContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminRegister(Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã được sử dụng chưa
                var existingAdmin = db.Admins.FirstOrDefault(x => x.Email == admin.Email);
                if (existingAdmin != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
                    return View(admin); // Trả về view với dữ liệu đã nhập và thông báo lỗi
                }

                // Lưu thông tin người dùng vào cơ sở dữ liệu
                db.Admins.Add(admin);
                db.SaveChanges();

                // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("AdminLogin", "AdminLogin");
            }

            // Nếu dữ liệu không hợp lệ, hiển thị lại trang đăng ký với thông báo lỗi
            return View(admin);
        }
    }
}
