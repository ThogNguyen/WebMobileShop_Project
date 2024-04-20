using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using PS36400_NguyenLocThong_Assignment.ViewModels;

namespace _21_PS36400_NguyenLocThong_ASM.Controllers
{
    public class LoginController : Controller
    {
        private readonly MobilePhoneContext db;

        public LoginController(MobilePhoneContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Đăng nhập
        [HttpPost]
        public IActionResult Index(NguoiDung user)
        {
            if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.MatKhau))
            {
                var f_password = HashPassword(user.MatKhau);
                var data = db.NguoiDungs.Where(s => s.Email.Equals(user.Email) && s.MatKhau.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    HttpContext.Session.SetString("FullName", data.FirstOrDefault().HoTen);
                    HttpContext.Session.SetInt32("idUser", data.FirstOrDefault().MaNguoiDung);
                    // sử dụng mã người dùng để check thanh toán
                    return RedirectToAction("Index", "TrangChu");
                }
                else
                {
                    TempData["error"] = "Đăng Nhập Thất Bại!";
                    return RedirectToAction("Index", "Login");
                }
            }
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

        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("FullName");
            HttpContext.Session.Remove("idUser");
            return RedirectToAction("Index", "Login"); // Chuyển hướng về trang chủ sau khi đăng xuất
        }

    }
}
