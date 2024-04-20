using Microsoft.AspNetCore.Mvc;
using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace PS36400_NguyenLocThong_Assignment.Areas.Admin.Controllers
{
    [Area("AdminPage")]
    [Route("Admin/{controller}/{action}")]
    public class AdminQLLoaiSanPhamController : Controller
    {
        private readonly MobilePhoneContext db;
        public AdminQLLoaiSanPhamController(MobilePhoneContext context)
        {
            db = context;
        }
        public IActionResult IndexLSP()
        {
            List<LoaiSanPham> loaisp = db.LoaiSanPhams.ToList();
            return View(loaisp);
        }

        [HttpGet]
        public IActionResult CreateLSP()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateLSP(LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSanPhams.Add(loaiSanPham);
                db.SaveChanges();
                return RedirectToAction("IndexLSP");
            }
            return View(loaiSanPham);
        }


        [HttpGet]
        public IActionResult EditLSP(int id)
        {
            var loaiSanPham = db.LoaiSanPhams.Find(id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            return View(loaiSanPham);
        }


        [HttpPost]
        public IActionResult EditLSP(LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexLSP");
            }
            return View(loaiSanPham);
        }

        [HttpGet]
        public IActionResult DeleteLSP(int id)
        {
            var loaiSanPham = db.LoaiSanPhams.Find(id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            db.LoaiSanPhams.Remove(loaiSanPham);
            db.SaveChanges();
            return RedirectToAction("IndexLSP");
        }
    }
}
