using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNS.Controllers
{
    public class ChucVuController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index()
        {
            var all_chucvu = from ChucVu in db.tb_CHUCVUs select ChucVu;
            return View(all_chucvu);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_CHUCVU p)
        {
            var E_tenchucvu = collection["TENCV"];
            if (string.IsNullOrEmpty(E_tenchucvu))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENCV = E_tenchucvu.ToString();
                db.tb_CHUCVUs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Delete(int id)
        {
            var D_ChucVu = db.tb_CHUCVUs.First(m => m.IDCV == id);
            return View(D_ChucVu);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_DanToc = db.tb_CHUCVUs.Where(m => m.IDCV == id).First();
            db.tb_CHUCVUs.DeleteOnSubmit(D_DanToc);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var E_machucvu = db.tb_CHUCVUs.First(m => m.IDCV == id);
            return View(E_machucvu);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_machucvu = db.tb_CHUCVUs.First(m => m.IDCV == id);
            var E_tenchucvu = collection["TENCV"];
            E_machucvu.IDCV = id;
            if (string.IsNullOrEmpty(E_tenchucvu))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_machucvu.TENCV = E_tenchucvu.ToString();
                UpdateModel(E_machucvu);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}