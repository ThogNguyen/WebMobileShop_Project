using PS36400_NguyenLocThong_Assignment.Models;
using PS36400_NguyenLocThong_Assignment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PS36400_NguyenLocThong_Assignment.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using _21_PS36400_NguyenLocThong_ASM.Controllers;

namespace PS36400_NguyenLocThong_Assignment.Controllers
{
    public class CartController : Controller
    {
        private readonly MobilePhoneContext db;
        public CartController(MobilePhoneContext context)
        {
            db = context;
        }

        const string CartKey = "MyCart";
        public List<CartVM> cart => HttpContext.Session.Get<List<CartVM>>
            (CartKey) ?? new List<CartVM>();

        [HttpGet]
        public IActionResult Index()
        {
            var giohang = cart;
            if (giohang.Count == 0)
            {
                ViewBag.EmptyCartMessage = "Giỏ hàng của bạn trống";
                return View(cart);
            }

            return View(cart);
        }

        public IActionResult addtoCart(int id, int soluong = 1)
        {
            var giohang = cart;
            var item = giohang.SingleOrDefault(x => x.MaSp == id);

            if (item == null)
            {
                var sp = db.SanPhams.SingleOrDefault(x => x.MaSp == id);
                if (sp != null)
                {
                    item = new CartVM
                    {
                        MaSp = sp.MaSp,
                        TenSp = sp.TenSp,
                        Hinh = sp.HinhSp,
                        Giatien = (int)sp.GiaSp,
                        Soluong = soluong
                    };
                    giohang.Add(item);
                }
            }
            else
            {
                item.Soluong += soluong;
            }

            HttpContext.Session.Set(CartKey, giohang);

            return RedirectToAction("Index");
        }

        public IActionResult removeCart(int id)
        {
            var giohang = cart;
            var item = giohang.SingleOrDefault(x => x.MaSp == id);

            if (item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set(CartKey, giohang);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetInt32("idUser") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(cart);
        }

        [HttpPost]
        public IActionResult Checkout(CheckOutVM model)
        {
            if (ModelState.IsValid)
            {
                var customerID = HttpContext.Session.GetInt32("idUser");

                if (customerID.HasValue)
                {
                    var kh = db.NguoiDungs.FirstOrDefault(x => x.MaNguoiDung == customerID);

                    if (kh != null)
                    {
                        var hoadon = new HoaDon
                        {
                            MaNguoiDung = customerID,
                            ThoiGianDat = DateTime.Now,
                            TenKhachHang = model.HoTen,
                            DiaChiGiaoHang = model.DiaChi,
                            DienThoai = model.SoDT,
                            Ghichu = model.GhiChu
                        };

                        db.Database.BeginTransaction();
                        try
                        {
                            db.Database.CommitTransaction();
                            db.Add(hoadon);
                            db.SaveChanges();

                            var hdct = new List<HoaDonChiTiet>();
                            foreach(var item in cart)
                            {
                                hdct.Add(new HoaDonChiTiet()
                                {
                                    MaHd = hoadon.MaHd,
                                    Soluong = item.Soluong,
                                    TongTien = item.Giatien,
                                    MaSp = item.MaSp,
                                });
                                // Cập nhật số lượt bán của sản phẩm
                                // Gọi phương thức từ SanPhamController
                                var danhMucSP = new DanhMucSPController(db);
                                danhMucSP.UpdateProductSales(item.MaSp, item.Soluong);
                            }
                            TempData["MaHD"] = hoadon.MaHd;
                            TempData["ThoiGianDat"] = hoadon.ThoiGianDat;


                            db.AddRange(hdct);
                            db.SaveChanges();
                            HttpContext.Session.Set(CartKey, new List<CartVM>());
                            return RedirectToAction("ShoppingSuccess", "Cart");
                        }
                        catch
                        {
                            db.Database.RollbackTransaction();
                        }
                    }
                }
            }
            return View(cart);
        }

        public IActionResult ShoppingSuccess()
        {
            return View();
        }
    }
}
