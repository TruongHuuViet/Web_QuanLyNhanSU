using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNS.Controllers
{
    public class KhenThuongController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index(string searchString)
        {
            ViewBag.KeyWord = searchString;
            var all_khenthuong = (from tb_KHENTHUONG in db.tb_KHENTHUONGs select tb_KHENTHUONG).OrderBy(m => m.MAKHENTHUONG);
            if (!string.IsNullOrEmpty(searchString)) all_khenthuong = (IOrderedQueryable<tb_KHENTHUONG>)all_khenthuong.Where(a => a.TENNV.Contains(searchString));
            return View(all_khenthuong);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_KHENTHUONG p)
        {
            var E_maNV = collection["MANV"];
            var E_tenNV = collection["TENNV"];
            var E_noidung = collection["NOIDUNG"];
            var E_lydo = collection["LYDO"];
            var E_ngaytao = Convert.ToDateTime(collection["NGAYTAO"]);
            if (string.IsNullOrEmpty(E_maNV))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENNV = E_tenNV.ToString();
                p.NOIDUNG = E_noidung.ToString();
                p.LYDO = E_lydo.ToString();
                p.NGAYTAO = Convert.ToDateTime(collection["NGAYTAO"]);
                db.tb_KHENTHUONGs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(p);
        }

        public ActionResult Edit(int id)
        {
            var E_bangkhenthuong = db.tb_KHENTHUONGs.First(m => m.MAKHENTHUONG == id);
            return View(E_bangkhenthuong);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_bangkhenthuong = db.tb_KHENTHUONGs.First(m => m.MAKHENTHUONG == id);
            var E_noidung = collection["NOIDUNG"];
            var E_lydo = collection["LYDO"];
            E_bangkhenthuong.MAKHENTHUONG = id;
            if (string.IsNullOrEmpty(E_noidung))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_bangkhenthuong.NOIDUNG = E_bangkhenthuong.ToString();
                E_bangkhenthuong.LYDO = E_bangkhenthuong.ToString();
                UpdateModel(E_bangkhenthuong);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var D_KHENTHUONG = db.tb_KHENTHUONGs.First(m => m.MAKHENTHUONG == id);
            return View(D_KHENTHUONG);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_KHENTHUONG = db.tb_KHENTHUONGs.Where(m => m.MAKHENTHUONG == id).First();
            db.tb_KHENTHUONGs.DeleteOnSubmit(D_KHENTHUONG);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}