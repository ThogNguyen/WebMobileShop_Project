using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PS36400_NguyenLocThong_Assignment.Models;
using Microsoft.AspNetCore.Http;

namespace PS36400_NguyenLocThong_Assignment.ViewModels
{
    public class SanphamVM
    {

        [Required(ErrorMessage = "*Tên sản phẩm không được bỏ trống")]
        public string? TenSp { get; set; }

        [Required(ErrorMessage = "*Giá sản phẩm không được bỏ trống")]
        public int? GiaSp { get; set; }

        public IFormFile? HinhSp { get; set; }

        public string? Chip { get; set; }

        public double? ManHinh { get; set; }

        public int? Ram { get; set; }

        public int? Rom { get; set; }

        public int? Soluotban { get; set; }

        public DateTime? NgaydangSp { get; set; }

        public int? MaLoaiSp { get; set; }
    }
}
