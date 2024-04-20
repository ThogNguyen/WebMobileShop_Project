using PS36400_NguyenLocThong_Assignment.Models;
using PS36400_NguyenLocThong_Assignment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.Arm;

namespace PS36400_NguyenLocThong_Assignment.Areas.Admin.Controllers
{
    [Area("AdminPage")]
    [Route("Admin/{controller}/{action}")]
    public class AdminQLSanPhamController : Controller
    {
        private readonly MobilePhoneContext db;
        private readonly IWebHostEnvironment environment;
        public AdminQLSanPhamController(MobilePhoneContext context, IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.db = context;
        }
        public IActionResult IndexSP(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // Lấy danh sách sản phẩm từ database
            var sanPhams = db.SanPhams.ToList();

            // Chia danh sách sản phẩm thành các trang
            var pagedSanPhams = sanPhams.ToPagedList(pageNumber, pageSize);

            return View(pagedSanPhams);
        }

        [HttpGet]
        public IActionResult CreateSP()
        {
            ViewBag.MaLoaiSp = new SelectList(db.LoaiSanPhams, "MaLoaiSp", "TenLoaiSp");
            return View();
        }

        [HttpPost]
        public IActionResult CreateSP(SanphamVM sp)   
        {
            if(sp.HinhSp == null)
            {
                ModelState.AddModelError("HinhSp", "*Ảnh sản phẩm không được bỏ trống");
            }
            if (!ModelState.IsValid)
            {
                return View(sp);
            }

            string newFileName = Path.GetFileName(sp.HinhSp.FileName);

            string imageFullPath = environment.WebRootPath + "/image/" + newFileName;
            using(var stream = System.IO.File.Create(imageFullPath))
            {
                sp.HinhSp.CopyTo(stream);
            }

            SanPham sanPham = new SanPham()
            {
                TenSp = sp.TenSp,
                GiaSp = sp.GiaSp,
                HinhSp = newFileName,
                Chip = sp.Chip,
                ManHinh = sp.ManHinh,
                Ram = sp.Ram,
                Rom = sp.Rom,
                Soluotban = sp.Soluotban,
                NgaydangSp = DateTime.Now,
                MaLoaiSp = sp.MaLoaiSp
            };

            db.SanPhams.Add(sanPham);
            db.SaveChanges();

            return RedirectToAction("IndexSP");
        }

        [HttpGet]
        public IActionResult EditSP(int id)
        {
            var sp = db.SanPhams.Find(id);

            if(sp == null)
            {
                return RedirectToAction("IndexSP");
            }

            var spVM = new SanphamVM()
            {
                TenSp = sp.TenSp,
                GiaSp = sp.GiaSp,
                Chip = sp.Chip,
                ManHinh = sp.ManHinh,
                Ram = sp.Ram,
                Rom = sp.Rom,
                Soluotban = sp.Soluotban,
                MaLoaiSp = sp.MaLoaiSp
            };

            ViewBag.MaLoaiSp = new SelectList(db.LoaiSanPhams, "MaLoaiSp", "TenLoaiSp");
            ViewData["MaSP"] = sp.MaSp;
            ViewData["HinhSP"] = sp.HinhSp; 
            ViewData["NgayDangSP"] = sp.NgaydangSp;

            return View(spVM);
        }

        [HttpPost]
        public IActionResult EditSP(int id, SanphamVM spVM)
        {
            var sp = db.SanPhams.Find(id);

            if (sp == null)
            {
                return RedirectToAction("IndexSP");
            }

            if(!ModelState.IsValid)
            {
                ViewBag.MaLoaiSp = new SelectList(db.LoaiSanPhams, "MaLoaiSp", "TenLoaiSp");
                ViewData["MaSP"] = sp.MaSp;
                ViewData["HinhSP"] = sp.HinhSp;
                ViewData["NgayDangSP"] = sp.NgaydangSp;

                return View(spVM);
            }

            // upload ảnh mới
            string newFileName = sp.HinhSp;

            if (spVM.HinhSp != null)
            {
                newFileName = Path.GetFileName(spVM.HinhSp.FileName);

                string imageFullPath = environment.WebRootPath + "/image/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    spVM.HinhSp.CopyTo(stream);
                }

                string oldHinhFullPath = environment.WebRootPath + "/image/" + sp.HinhSp;
                System.IO.File.Delete(oldHinhFullPath);
            }

            sp.TenSp = spVM.TenSp;
            sp.GiaSp = spVM.GiaSp;
            sp.HinhSp = newFileName;
            sp.Chip = spVM.Chip;
            sp.ManHinh = spVM.ManHinh;
            sp.Ram = spVM.Ram;
            sp.Rom = spVM.Rom;
            sp.MaLoaiSp = spVM.MaLoaiSp;

            db.SaveChanges();

            return RedirectToAction("IndexSP");
        }

        public IActionResult DeleteSP(int id)
        {
            var sp = db.SanPhams.Find(id);

            if (sp == null)
            {
                return RedirectToAction("IndexSP");
            }

            string imageFullPath = environment.WebRootPath + "/image/" + sp.HinhSp;
            System.IO.File.Delete(imageFullPath);

            db.SanPhams.Remove(sp);
            db.SaveChanges();

            return RedirectToAction("IndexSP");
        }
    }
}
