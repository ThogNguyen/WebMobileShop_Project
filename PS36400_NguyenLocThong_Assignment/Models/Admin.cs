using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PS36400_NguyenLocThong_Assignment.Models;

public partial class Admin
{
    public int Id { get; set; }
    public string? HoTen { get; set; }

    [Required(ErrorMessage = "Email không được bỏ trống")]
    public string? Email { get; set; }

    public string? Sdt { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
    public string? MatKhau { get; set; }
}
