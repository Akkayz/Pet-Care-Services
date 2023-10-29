// HomeController.cs
using DichVuThuCungKVH.Model;
using System.Linq;
using System.Web.Mvc;

namespace DichVuThuCungKVH.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private DACSEntities db = new DACSEntities();

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            TaiKhoan ad = db.TaiKhoans.SingleOrDefault(n => n.TenTK == UserName && n.MatKhau == Password && n.LoaiTK == "1");
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View();
            }
        }
    }
}