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
    public class TrinhDoController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index()
        {
            var all_trinhdo = from TrinhDo in db.tb_TRINHDOs select TrinhDo;
            return View(all_trinhdo);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_TRINHDO p)
        {
            var E_tentrinhdo = collection["TENTD"];
            if (string.IsNullOrEmpty(E_tentrinhdo))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENTD = E_tentrinhdo.ToString();
                db.tb_TRINHDOs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Delete(int id)
        {
            var D_TrinhDo = db.tb_TRINHDOs.First(m => m.IDTD == id);
            return View(D_TrinhDo);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_TrinhDo = db.tb_TRINHDOs.Where(m => m.IDTD == id).First();
            db.tb_TRINHDOs.DeleteOnSubmit(D_TrinhDo);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var E_Trinhdo = db.tb_TRINHDOs.First(m => m.IDTD == id);
            return View(E_Trinhdo);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_Trinhdo = db.tb_TRINHDOs.First(m => m.IDTD == id);
            var E_Tentrinndo = collection["TENTD"];
            E_Trinhdo.IDTD = id;
            if (string.IsNullOrEmpty(E_Tentrinndo))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_Trinhdo.TENTD = E_Tentrinndo.ToString();
                UpdateModel(E_Trinhdo);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}
