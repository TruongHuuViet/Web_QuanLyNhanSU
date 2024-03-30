using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNS.Controllers
{
    public class KyluatController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index(string searchString)
        {
            ViewBag.KeyWord = searchString;
            var all_kyluat = (from tb_KYLUAT in db.tb_KYLUATs select tb_KYLUAT).OrderBy(m => m.MAKYLUAT);
            if (!string.IsNullOrEmpty(searchString)) all_kyluat = (IOrderedQueryable<tb_KYLUAT>)all_kyluat.Where(a => a.TENNV.Contains(searchString));
            return View(all_kyluat);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_KYLUAT p)
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
                db.tb_KYLUATs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public ActionResult Edit(int id)
        {
            var E_bangkyluat = db.tb_KYLUATs.First(m => m.MAKYLUAT == id);
            return View(E_bangkyluat);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_bangkyluat = db.tb_KYLUATs.First(m => m.MAKYLUAT == id);
            var E_noidung = collection["NOIDUNG"];
            var E_lydo = collection["LYDO"];
            E_bangkyluat.MAKYLUAT = id;
            if (string.IsNullOrEmpty(E_noidung))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_bangkyluat.NOIDUNG = E_bangkyluat.ToString();
                E_bangkyluat.LYDO = E_bangkyluat.ToString();
                UpdateModel(E_bangkyluat);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var D_KYLUAT = db.tb_KYLUATs.First(m => m.MAKYLUAT == id);
            return View(D_KYLUAT);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_KYLUAT = db.tb_KYLUATs.Where(m => m.MAKYLUAT == id).First();
            db.tb_KYLUATs.DeleteOnSubmit(D_KYLUAT);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}