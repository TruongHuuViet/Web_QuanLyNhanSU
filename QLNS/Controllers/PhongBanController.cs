using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNS.Controllers
{
    public class PhongBanController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index()
        {
            var all_phongban = from PhongBan in db.tb_PHONGBANs select PhongBan;
            return View(all_phongban);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_PHONGBAN p)
        {
            var E_tenphongban = collection["TENPB"];
            if (string.IsNullOrEmpty(E_tenphongban))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENPB = E_tenphongban.ToString();
                db.tb_PHONGBANs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Delete(int id)
        {
            var D_PhongBan = db.tb_PHONGBANs.First(m => m.IDPB == id);
            return View(D_PhongBan);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_PhongBan = db.tb_PHONGBANs.Where(m => m.IDPB == id).First();
            db.tb_PHONGBANs.DeleteOnSubmit(D_PhongBan);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var E_maphongban = db.tb_PHONGBANs.First(m => m.IDPB == id);
            return View(E_maphongban);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_maphongban = db.tb_PHONGBANs.First(m => m.IDPB == id);
            var E_tenphongban = collection["TENPB"];
            E_maphongban.IDPB = id;
            if (string.IsNullOrEmpty(E_tenphongban))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_maphongban.TENPB = E_tenphongban.ToString();
                UpdateModel(E_maphongban);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}