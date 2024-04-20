using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace PS36400_NguyenLocThong_Assignment
{
    public class TrangChuController : Controller
    {
        private readonly MobilePhoneContext db;
        public TrangChuController(MobilePhoneContext context)
        {
            db = context;
        }

        public IActionResult Index(int? id)
        {
            var listsp = db.SanPhams.AsQueryable();

            if (id.HasValue)
            {
                listsp = listsp.Where(x => x.MaLoaiSp == id.Value);
            }

            var result = listsp.Select(x => new SanPham
            {
                MaSp = x.MaSp,
                HinhSp = x.HinhSp,
                TenSp = x.TenSp,
                GiaSp = x.GiaSp,
                Chip = x.Chip,
                ManHinh = x.ManHinh,
                Ram = x.Ram,
                Rom = x.Rom,
                Soluotban = x.Soluotban,
                NgaydangSp = x.NgaydangSp
            });

            // Truyền tham số maloai vào view data để sử dụng trong view
            ViewData["MaLoai"] = id;

            return View(result);
        }
    }
}
