using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PS36400_NguyenLocThong_Assignment.Models;

public partial class NguoiDung
{
    public int MaNguoiDung { get; set; }

    [Required(ErrorMessage = "Họ và tên không được bỏ trống")]
    [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
    public string? HoTen { get; set; }
    [Required(ErrorMessage = "Email không được bỏ trống")]
    [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
    public string? Sdt { get; set; }
    [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
    [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
    public string? DiaChi { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
    [MinLength(6, ErrorMessage = "Tối thiểu 6 kí tự")]
    [MaxLength(12, ErrorMessage = "Tối đa 12 kí tự")]
    [DataType(DataType.Password)]
    public string? MatKhau { get; set; }
    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
