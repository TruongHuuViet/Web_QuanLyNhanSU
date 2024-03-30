using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace QLNS.Controllers
{
    public class BangLuongController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index(string searchString)
        {
            tb_NHANVIEN nvSession = (tb_NHANVIEN)Session["user"];
            var count = db.PHANQUYENs.Count(m => m.MANV == nvSession.MANV & m.MACHUCNANG == 2);
            if (count == 0)
            {
                return Redirect("https://localhost:44399/BaoLoi/Index");
            }
            ViewBag.KeyWord = searchString;
            var all_BANGLUONGs = (from tb_BANGLUONG in db.BANGLUONGs select tb_BANGLUONG).OrderBy(m => m.MALUONG);
            if (!string.IsNullOrEmpty(searchString)) all_BANGLUONGs = (IOrderedQueryable<BANGLUONG>)all_BANGLUONGs.Where(a => a.TENNV.Contains(searchString));
            return View(all_BANGLUONGs);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, BANGLUONG p)
        {
            var E_tenNV = collection["TENNV"];
            var E_maNV = collection["MANV"];
            var E_songaylam = collection["SONGAYLAM"];
            var E_hesoluong = collection["HESOLUONG"];
            var E_thuong = collection["THUONG"];
            var E_tongluong = collection["TONGLUONG"];
            if (string.IsNullOrEmpty(E_maNV))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENNV = E_tenNV.ToString();
                p.SONGAYLAM = E_songaylam.AsInt();
                p.HESOLUONG = E_hesoluong.AsFloat();
                p.THUONG = E_thuong.AsFloat();
                p.TONGLUONG = E_songaylam.AsFloat()*E_hesoluong.AsFloat()+E_thuong.AsFloat();
                
                p.MANV = E_maNV.AsInt();
                db.BANGLUONGs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public ActionResult Delete(int id)
        {
            var BANGLUONG = db.BANGLUONGs.First(m => m.MALUONG == id);
            return View(BANGLUONG);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var BANGLUONG = db.BANGLUONGs.Where(m => m.MALUONG == id).First();
            db.BANGLUONGs.DeleteOnSubmit(BANGLUONG);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var E_BANGLUONG = db.BANGLUONGs.First(m => m.MALUONG == id);
            return View(E_BANGLUONG);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_BANGLUONG = db.BANGLUONGs.First(m => m.MALUONG == id);
            var E_songaylam = collection["SONGAYLAM"];
            var E_Thuong = collection["THUONG"];
            var E_Tongluong = collection["TONGLUONG"];

            E_BANGLUONG.MALUONG = id;
            if (string.IsNullOrEmpty(E_songaylam))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_BANGLUONG.THUONG = E_Thuong.AsFloat();
                E_BANGLUONG.SONGAYLAM = E_songaylam.AsInt();
                E_BANGLUONG.TONGLUONG = E_Tongluong.AsFloat();
                UpdateModel(E_BANGLUONG);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

    }
}