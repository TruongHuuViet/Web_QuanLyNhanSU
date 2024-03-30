using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace QLNS.Controllers
{
    public class DonDuyetController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index()
        {
            var all_Don = from DONDUYET in db.DONDUYETs select DONDUYET;
            return View(all_Don);
        }
        public ActionResult AllDon()
        {
            var all_Don = from DONDUYET in db.DONDUYETs select DONDUYET;
            return View(all_Don);
        }
        public ActionResult NopDon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NopDon(FormCollection collection,DONDUYET p)
        {
            var E_tenNV = collection["HOTEN"];
            var E_maNV = collection["MANV"];
            var E_NGAYSINH = Convert.ToDateTime(collection["NGAYSINH"]);
            var E_NGAYXINNGHI = Convert.ToDateTime(collection["NGAYXINNGHI"]);
            var E_SDT = collection["SDT"];
            var E_PHONGBAN = collection["PHONGBAN"];
            var E_LYDO = collection["LYDO"];
            var E_NGAYLAPPHIEU = DateTime.Now;
            DateTime dateOnly = E_NGAYLAPPHIEU.Date;
            if (string.IsNullOrEmpty(E_maNV))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.HOTEN = E_tenNV.ToString();
                p.MANV = E_maNV.AsInt();
                p.NGAYSINH = Convert.ToDateTime(collection["NGAYSINH"]);
                p.NGAYXINNGHI = Convert.ToDateTime(collection["NGAYXINNGHI"]);
                p.PHONGBAN = E_PHONGBAN.ToString();
                p.LYDO = E_LYDO.ToString();
                p.SDT = E_SDT.ToString();
                p.NGAYLAPPHIEU = DateTime.Now;
                db.DONDUYETs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("NopDon");
        }
        public ActionResult DuyetDon(int id)
        {
            var E_Don = db.DONDUYETs.First(m => m.MADON == id);
            return View(E_Don);
        }
        [HttpPost]
        public ActionResult DuyetDon(int id, FormCollection collection)
        {
            var E_Don = db.DONDUYETs.First(m => m.MADON == id);
            var E_TINHTRANG = collection["TINHTRANG"];
            E_Don.MADON = id;
            if (string.IsNullOrEmpty(E_TINHTRANG))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_Don.TINHTRANG = E_TINHTRANG.ToString();
                UpdateModel(E_Don);
                db.SubmitChanges();
                return RedirectToAction("AllDon");
            }
            return this.DuyetDon(id);
        }
        public ActionResult Detail(int id)
        {
            var D_Don = db.DONDUYETs.Where(m => m.MADON == id).First();
            return View(D_Don);
        }

    }
}