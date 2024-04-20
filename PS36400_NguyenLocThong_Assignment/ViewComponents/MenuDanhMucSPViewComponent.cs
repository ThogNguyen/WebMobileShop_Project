using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace PS36400_NguyenLocThong_Assignment.ViewComponents
{
    public class MenuDanhMucSPViewComponent : ViewComponent
    {
        private readonly MobilePhoneContext db;
        public MenuDanhMucSPViewComponent(MobilePhoneContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.LoaiSanPhams.Select(loaisp => new LoaiSanPham
            {
                MaLoaiSp = loaisp.MaLoaiSp, 
                TenLoaiSp = loaisp.TenLoaiSp
            }).OrderBy(x => x.TenLoaiSp);
            return View(data);
        }
    }
}
