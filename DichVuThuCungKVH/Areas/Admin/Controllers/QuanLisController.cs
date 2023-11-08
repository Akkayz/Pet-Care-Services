using DichVuThuCungKVH.Model;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DichVuThuCungKVH.Areas.Admin.Controllers
{
    public class QuanLisController : Controller
    {
        private DACSEntities db = new DACSEntities();

        // GET: Admin/QuanLis
        public ActionResult Index()
        {
            var quanLis = db.QuanLis.Include(q => q.TaiKhoan);
            return View(quanLis.ToList());
        }

        // GET: Admin/QuanLis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLi quanLi = db.QuanLis.Find(id);
            if (quanLi == null)
            {
                return HttpNotFound();
            }
            return View(quanLi);
        }

        // GET: Admin/QuanLis/Create
        public ActionResult Create()
        {
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenTK");
            return View();
        }

        // POST: Admin/QuanLis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQL,TenQL,ChucVu,ChiNhanh,NamSinh,SDT,DiaChi,GioiTinh,MaTK")] QuanLi quanLi)
        {
            if (ModelState.IsValid)
            {
                db.QuanLis.Add(quanLi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenTK", quanLi.MaTK);
            return View(quanLi);
        }

        // GET: Admin/QuanLis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLi quanLi = db.QuanLis.Find(id);
            if (quanLi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenTK", quanLi.MaTK);
            return View(quanLi);
        }

        // POST: Admin/QuanLis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQL,TenQL,ChucVu,ChiNhanh,NamSinh,SDT,DiaChi,GioiTinh,MaTK")] QuanLi quanLi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanLi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenTK", quanLi.MaTK);
            return View(quanLi);
        }

        // GET: Admin/QuanLis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLi quanLi = db.QuanLis.Find(id);
            if (quanLi == null)
            {
                return HttpNotFound();
            }
            return View(quanLi);
        }

        // POST: Admin/QuanLis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuanLi quanLi = db.QuanLis.Find(id);
            db.QuanLis.Remove(quanLi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}