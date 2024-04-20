using Microsoft.AspNetCore.Mvc;
using PS36400_NguyenLocThong_Assignment.Models;

namespace _21_PS36400_NguyenLocThong_ASM.Controllers
{
    public class TimKiemSPController : Controller
    {
        private readonly MobilePhoneContext db;
        public TimKiemSPController(MobilePhoneContext context)
        {
            db = context;
        }
        public IActionResult Index(string timkiem)
        {
            var listsp = db.SanPhams.AsQueryable();


            if (string.IsNullOrEmpty(timkiem))
            {
                ViewBag.NullQuery = "Không tìm thấy sản phẩm bạn tìm";
                return View();
            }

            listsp = listsp.Where(x => x.TenSp.Contains(timkiem));


            var result = listsp.Select(x => new SanPham
            {
                MaSp = x.MaSp,
                HinhSp = x.HinhSp,
                TenSp = x.TenSp,
                GiaSp = x.GiaSp,
                Chip = x.Chip,
                ManHinh = x.ManHinh,
                Ram = x.Ram,
                Rom = x.Rom
            });
            ViewBag.Query = timkiem;

            if (!result.Any())
            {
                ViewBag.NullQuery = "Không tìm thấy sản phẩm bạn cần tìm";
                return View();
            }

            return View(result);
        }
    }
}
