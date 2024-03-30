using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QLNS.Controllers
{
    public class LoginController : Controller
    {
        dbQLNSDataContext data = new dbQLNSDataContext();
        public ActionResult Index()
        {

                return View();
            
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(String email, String password)
        {
            dbQLNSDataContext db = new dbQLNSDataContext();
            var nhanVien = db.tb_NHANVIENs.SingleOrDefault(m => m.EMAIL.ToLower() == email.ToLower() && m.MATKHAU == password);

            if (nhanVien != null)
            {
                Session["user"] = nhanVien;

                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["error"] = "Tài khoản đăng nhập không đúng !!!";
                return View();
            }
        }
        public ActionResult DangXuat()
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap");
        }
    }
}