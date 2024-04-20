using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace _21_PS36400_NguyenLocThong_ASM.Controllers
{
    public class DanhMucSPController : Controller
    {
        private readonly MobilePhoneContext db;
        public DanhMucSPController(MobilePhoneContext context)
        {
            db = context;
        }


        // Danh mục sản phẩm
        public IActionResult Index(string loai, int? id, string sortOrder, string priceRange, string screenSize,int page = 1, int pageSize = 6)
        {
            var listsp = db.SanPhams.AsQueryable();

            // Tìm MaLoaiSp tương ứng với tên loại sản phẩm
            if (!string.IsNullOrEmpty(loai))
            {
                var loaiSp = db.LoaiSanPhams.FirstOrDefault(x => x.TenLoaiSp == loai);
                if (loaiSp != null)
                {
                    id = loaiSp.MaLoaiSp;
                }
            }

            // Lọc sản phẩm theo MaLoaiSp
            if (id.HasValue)
            {
                listsp = listsp.Where(x => x.MaLoaiSp == id.Value);
            }


            ViewBag.SortNewSP = string.IsNullOrEmpty(sortOrder) ? "sanphammoi" : "sanphammoi";
            ViewBag.SortBestSeller = (sortOrder == "sanphambanchay" ? "sanphambanchay" : "sanphambanchay");
            ViewBag.SortPriceUp = (sortOrder == "giatang" ? "giatang" : "giatang");
            ViewBag.SortPriceDown = (sortOrder == "giagiam" ? "giagiam" : "giagiam");

            // Sắp xếp sản phẩm
            switch (sortOrder)
            {
                case "sanphammoi":
                    listsp = listsp.OrderByDescending(x => x.NgaydangSp);
                    break;
                case "sanphambanchay":
                    listsp = listsp.OrderByDescending(x => x.Soluotban);
                    break;
                case "giatang":
                    listsp = listsp.OrderBy(x => x.GiaSp);
                    break;
                case "giagiam":
                    listsp = listsp.OrderByDescending(x => x.GiaSp);
                    break;
                default:
                    listsp = listsp.OrderByDescending(x => x.NgaydangSp);
                    break;
            }

            // Lọc sản phẩm theo giá
            switch (priceRange)
            {
                case "down2Mil":
                    listsp = listsp.Where(x => x.GiaSp < 2000000);
                    break;
                case "2to4Mil":
                    listsp = listsp.Where(x => x.GiaSp >= 2000000 && x.GiaSp < 4000000);
                    break;
                case "4to7Mil":
                    listsp = listsp.Where(x => x.GiaSp >= 4000000 && x.GiaSp < 7000000);
                    break;
                case "up7Mil":
                    listsp = listsp.Where(x => x.GiaSp >= 7000000);
                    break;
                default:
                    break;
            }

            // Lọc sản phẩm theo kích thước màn hình
            switch (screenSize)
            {
                case "6den7inches":
                    listsp = listsp.Where(x => x.ManHinh >= 6 && x.ManHinh <= 7);
                    break;
                case "tren7inches":
                    listsp = listsp.Where(x => x.ManHinh > 7 || x.ManHinh == null);
                    break;
                default:
                    break;
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
                Rom = x.Rom
            }).ToPagedList(page, pageSize);

            // Truyền tham số maloai vào view data để sử dụng trong view
            ViewData["TenLoai"] = loai;
            ViewData["SortOrder"] = sortOrder;
            ViewData["PriceRange"] = priceRange;
            ViewData["ScreenSize"] = screenSize;

            return View(result);
        }

        // Sản phẩm chi tiết
        public async Task<IActionResult> DetailProduct(int id)
        {
            var productDetail = await db.SanPhams.FirstOrDefaultAsync(p => p.MaSp == id);

            return View(productDetail);
        }


        // Phương thức cập nhật số lượt bán của sản phẩm
        public void UpdateProductSales(int productId, int quantity)
        {
            var product = db.SanPhams.Find(productId);
            if (product != null)
            {
                product.Soluotban += quantity;
                db.SaveChanges();
            }
        }

    }


}

