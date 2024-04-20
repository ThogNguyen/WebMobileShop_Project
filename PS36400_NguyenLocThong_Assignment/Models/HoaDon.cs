using System;
using System.Collections.Generic;

namespace PS36400_NguyenLocThong_Assignment.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int? MaNguoiDung { get; set; }

    public DateTime? ThoiGianDat { get; set; }

    public string? TenKhachHang { get; set; }

    public string? DiaChiGiaoHang { get; set; }

    public string? DienThoai { get; set; }

    public string? Ghichu { get; set; }

    public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();

    public virtual NguoiDung? MaNguoiDungNavigation { get; set; }
}
