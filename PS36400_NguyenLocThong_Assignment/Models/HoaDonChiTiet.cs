using System;
using System.Collections.Generic;

namespace PS36400_NguyenLocThong_Assignment.Models;

public partial class HoaDonChiTiet
{
    public int MaHdct { get; set; }

    public int? MaSp { get; set; }

    public int? MaHd { get; set; }

    public int? TongTien { get; set; }

    public int? Soluong { get; set; }

    public virtual HoaDon? MaHdNavigation { get; set; }

    public virtual SanPham? MaSpNavigation { get; set; }
}
