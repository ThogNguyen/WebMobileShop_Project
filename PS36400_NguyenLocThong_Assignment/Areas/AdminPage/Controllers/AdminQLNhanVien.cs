using Microsoft.AspNetCore.Mvc;
using PS36400_NguyenLocThong_Assignment.Models;

namespace PS36400_NguyenLocThong_Assignment.Areas.AdminPage.Controllers
{
    [Area("AdminPage")]
    [Route("Admin/{controller}/{action}")]
    public class AdminQLNhanVien : Controller
    {
        private readonly MobilePhoneContext db;
        public AdminQLNhanVien(MobilePhoneContext context)
        {
            db = context;
        }
        public IActionResult IndexAdmin()
        {
            List<Models.Admin> admin = db.Admins.ToList();
            return View(admin);
        }

        [HttpGet]
        public IActionResult EditAdmin(int id)
        {
            var admin = db.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }
            ViewData["ID"] = admin.Id;
            return View(admin);
        }
        [HttpPost]
        public IActionResult EditAdmin(Models.Admin model)
        {
            if (!ModelState.IsValid)
            {
                var admin = db.Admins.Find(model.Id);
                if (admin == null)
                {
                    return NotFound();
                }

                // Kiểm tra nếu trường Email trống
                if (string.IsNullOrWhiteSpace(model.Email))
                {
                    ModelState.AddModelError("Email", "Email không được bỏ trống");
                    return View(model);
                }
                else
                {
                    admin.HoTen = model.HoTen;
                    admin.Email = model.Email;
                    admin.Sdt = model.Sdt;
                }
                
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                return RedirectToAction("IndexAdmin"); // Redirect về trang danh sách admin sau khi cập nhật thành công
            }

            // Nếu ModelState không hợp lệ, hiển thị lại form với thông báo lỗi
            return View(model);
        }

        public IActionResult DeleteAdmin(int id)
        {
            var admin = db.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            db.Admins.Remove(admin);
            db.SaveChanges();

            return RedirectToAction("IndexAdmin");
        }

    }
}
