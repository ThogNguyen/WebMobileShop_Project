using System;
using System.Collections.Generic;

namespace PS36400_NguyenLocThong_Assignment.Models;

public partial class LoaiSanPham
{
    public int MaLoaiSp { get; set; }

    public string? TenLoaiSp { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
