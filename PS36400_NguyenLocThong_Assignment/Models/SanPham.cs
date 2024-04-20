using System;
using System.Collections.Generic;

namespace PS36400_NguyenLocThong_Assignment.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string? TenSp { get; set; }

    public int? GiaSp { get; set; }

    public string? HinhSp { get; set; }

    public string? Chip { get; set; }

    public double? ManHinh { get; set; }

    public int? Ram { get; set; }

    public int? Rom { get; set; }

    public int? Soluotban { get; set; }

    public DateTime? NgaydangSp { get; set; }

    public int? MaLoaiSp { get; set; }

    public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();

    public virtual LoaiSanPham? MaLoaiSpNavigation { get; set; }
}
