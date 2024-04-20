using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace PS36400_NguyenLocThong_Assignment
{
    public class RegisterController : Controller
    {
        private readonly MobilePhoneContext db;

        public RegisterController(MobilePhoneContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(NguoiDung model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã được sử dụng chưa
                var existingUser = db.NguoiDungs.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
                    return View();
                }

                // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
                model.MatKhau = HashPassword(model.MatKhau);

                // Lưu thông tin người dùng vào cơ sở dữ liệu
                db.NguoiDungs.Add(model);
                db.SaveChanges();

                // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Index", "Login");
            }

            // Nếu dữ liệu không hợp lệ, hiển thị lại trang đăng ký với thông báo lỗi
            return View();
        }

        // Hàm mã hóa mật khẩu sử dụng SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
