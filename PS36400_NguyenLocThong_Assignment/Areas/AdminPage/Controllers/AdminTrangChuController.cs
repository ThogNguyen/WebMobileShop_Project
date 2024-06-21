using PS36400_NguyenLocThong_Assignment.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace _21_PS36400_NguyenLocThong_ASM.Areas.Admin.Controllers
{
    [Area("AdminPage")]
    [Route("Admin")]
    public class AdminTrangChuController : Controller
    {
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
    }
}
