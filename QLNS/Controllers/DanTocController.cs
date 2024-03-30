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
    public class DanTocController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index()
        {
            tb_NHANVIEN nvSession = (tb_NHANVIEN)Session["user"];
            var count = db.PHANQUYENs.Count(m => m.MANV == nvSession.MANV & m.MACHUCNANG == 3);
            if (count == 0)
            {
                return Redirect("https://localhost:44399/BaoLoi/Index");
            }
            var all_dantoc = from DanToc in db.tb_DANTOCs select DanToc;
            return View(all_dantoc);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_DANTOC p)
        {
            var E_tendantoc = collection["TENDT"];
            if (string.IsNullOrEmpty(E_tendantoc))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENDT = E_tendantoc.ToString();
                db.tb_DANTOCs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Delete(int id)
        {
            var D_DanToc = db.tb_DANTOCs.First(m => m.IDDT == id);
            return View(D_DanToc);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_DanToc = db.tb_DANTOCs.Where(m => m.IDDT == id).First();
            db.tb_DANTOCs.DeleteOnSubmit(D_DanToc);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var E_madantoc = db.tb_DANTOCs.First(m => m.IDDT == id);
            return View(E_madantoc);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_madantoc = db.tb_DANTOCs.First(m => m.IDDT == id);
            var E_tendantoc = collection["TENDT"];
            E_madantoc.IDDT = id;
            if (string.IsNullOrEmpty(E_tendantoc))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_madantoc.TENDT = E_tendantoc.ToString();
                UpdateModel(E_madantoc);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}
        