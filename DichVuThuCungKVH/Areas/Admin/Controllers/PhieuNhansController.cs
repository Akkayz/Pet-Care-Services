using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DichVuThuCungKVH.Model;

namespace DichVuThuCungKVH.Areas.Admin.Controllers
{
    public class PhieuNhansController : Controller
    {
        private readonly DACSEntities db = new DACSEntities();

        // GET: Admin/PhieuNhans
        public ActionResult Index()
        {
            var phieuNhans = db.PhieuNhans.Include(p => p.SuDungDichVu).Include(p => p.ThuCung);
            return View(phieuNhans.ToList());
        }

        // GET: Admin/PhieuNhans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhan phieuNhan = db.PhieuNhans.Find(id);
            if (phieuNhan == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhan);
        }

        public ActionResult Create(int? maSDDV)
        {
            if (maSDDV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.MaSDDV = maSDDV;
            ViewBag.MaTC = new SelectList(db.ThuCungs, "MaTC", "TenTC");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieu,MaTC,MaSDDV,TinhTrangTruocTiepNhan,TinhTrangSauTiepNhan,NguoiGiao,NguoiNhan,NgayNhan,TinhTrangDichVu,NgayTra,NguoiTra,GhiChu")] PhieuNhan phieuNhan, int? maSDDV)
        {
            if (maSDDV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                // Code lưu thông tin PhieuNhan vào cơ sở dữ liệu
                db.PhieuNhans.Add(phieuNhan);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = phieuNhan.MaPhieu });
            }

            ViewBag.MaSDDV = new SelectList(db.SuDungDichVus, "MaSDDV", "GhiChu", phieuNhan.MaSDDV);
            ViewBag.MaTC = new SelectList(db.ThuCungs, "MaTC", "TenTC", phieuNhan.MaTC);
            return View(phieuNhan);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhan phieuNhan = db.PhieuNhans.Find(id);
            if (phieuNhan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSDDV = new SelectList(db.SuDungDichVus, "MaSDDV", "GhiChu", phieuNhan.MaSDDV);
            ViewBag.MaTC = new SelectList(db.ThuCungs, "MaTC", "TenTC", phieuNhan.MaTC);
            return View(phieuNhan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieu,MaTC,MaSDDV,TinhTrangTruocTiepNhan,TinhTrangSauTiepNhan,NguoiGiao,NguoiNhan,NgayNhan,TinhTrangDichVu,NgayTra,NguoiTra,GhiChu")] PhieuNhan phieuNhan)
        {
            if (ModelState.IsValid)
            {
                // Your logic to update the edited PhieuNhan
                // ...

                return RedirectToAction("Details", new { id = phieuNhan.MaPhieu });
            }

            ViewBag.MaSDDV = new SelectList(db.SuDungDichVus, "MaSDDV", "GhiChu", phieuNhan.MaSDDV);
            ViewBag.MaTC = new SelectList(db.ThuCungs, "MaTC", "TenTC", phieuNhan.MaTC);
            return View(phieuNhan);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhan phieuNhan = db.PhieuNhans.Find(id);
            if (phieuNhan == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuNhan phieuNhan = db.PhieuNhans.Find(id);
            db.PhieuNhans.Remove(phieuNhan);
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

        public ActionResult PhieuCuaLuotSDDV(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the service usage with the provided ID
            SuDungDichVu suDungDichVu = db.SuDungDichVus.Find(id);
            if (suDungDichVu == null)
            {
                return HttpNotFound();
            }

            // Retrieve and return the list of receiving vouchers associated with this service usage
            var phieuNhans = db.PhieuNhans.Where(pn => pn.MaSDDV == id).ToList();

            return View(phieuNhans);
        }
    }
}