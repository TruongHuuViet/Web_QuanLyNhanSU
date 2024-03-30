using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNS.Controllers
{
    public class BoPhanController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index()
        {
            var all_bophan = from BoPhan in db.tb_BOPHANs select BoPhan;
            return View(all_bophan);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_BOPHAN p)
        {
            var E_tenbophan = collection["TENBP"];
            if (string.IsNullOrEmpty(E_tenbophan))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENBP = E_tenbophan.ToString();
                db.tb_BOPHANs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Delete(int id)
        {
            var D_BoPhan = db.tb_BOPHANs.First(m => m.IDBP == id);
            return View(D_BoPhan);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_BoPhan = db.tb_BOPHANs.Where(m => m.IDBP == id).First();
            db.tb_BOPHANs.DeleteOnSubmit(D_BoPhan);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var E_mabophan = db.tb_BOPHANs.First(m => m.IDBP == id);
            return View(E_mabophan);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_mabophan = db.tb_BOPHANs.First(m => m.IDBP == id);
            var E_tenbophan = collection["TENBP"];
            E_mabophan.IDBP = id;
            if (string.IsNullOrEmpty(E_tenbophan))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_mabophan.TENBP = E_tenbophan.ToString();
                UpdateModel(E_mabophan);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}
