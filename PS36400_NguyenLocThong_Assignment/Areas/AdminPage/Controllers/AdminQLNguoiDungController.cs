using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PS36400_NguyenLocThong_Assignment.Areas.Admin.Controllers
{
    [Area("AdminPage")]
    [Route("Admin/{controller}/{action}")]
    public class AdminQLNguoiDungController : Controller
    {
        private readonly MobilePhoneContext db;
        public AdminQLNguoiDungController(MobilePhoneContext context)
        {
            db = context;
        }
        public IActionResult IndexUser()
        {
            List<NguoiDung> user = db.NguoiDungs.ToList();
            return View(user);
        }

        public IActionResult DeleteUser(int id)
        {
            var user = db.NguoiDungs.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.NguoiDungs.Remove(user);
            db.SaveChanges();
            return RedirectToAction("IndexUser");
        }

    }
}
