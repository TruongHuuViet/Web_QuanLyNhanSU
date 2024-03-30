using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNS.Controllers
{
    public class TonGiaoController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index()
        {
            var all_tongiao = from TonGiao in db.tb_TONGIAOs select TonGiao;
            return View(all_tongiao);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_TONGIAO p)
        {
            var E_tentongiao = collection["TenTG"];
            if (string.IsNullOrEmpty(E_tentongiao))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENTG = E_tentongiao.ToString();
                db.tb_TONGIAOs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Delete(int id)
        {
            var D_TonGiao = db.tb_TONGIAOs.First(m => m.IDTG == id);
            return View(D_TonGiao);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_TonGiao = db.tb_TONGIAOs.Where(m => m.IDTG == id).First();
            db.tb_TONGIAOs.DeleteOnSubmit(D_TonGiao);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var E_matongiao = db.tb_TONGIAOs.First(m => m.IDTG == id);
            return View(E_matongiao);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_matongiao = db.tb_TONGIAOs.First(m => m.IDTG == id);
            var E_tentongiao = collection["TENTG"];
            E_matongiao.IDTG = id;
            if (string.IsNullOrEmpty(E_tentongiao))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_matongiao.TENTG = E_tentongiao.ToString();
                UpdateModel(E_matongiao);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}