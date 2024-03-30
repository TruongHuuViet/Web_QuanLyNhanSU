using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNS.Models;
using System.Drawing;
using System.Web.WebPages;
namespace QLNS.Controllers
{
    public class NhanVienController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Index(string searchString)
        {
            tb_NHANVIEN nvSession = (tb_NHANVIEN)Session["user"];
            var count = db.PHANQUYENs.Count(m => m.MANV == nvSession.MANV & m.MACHUCNANG == 1);
            if (count == 0)
            {
                return Redirect("https://localhost:44399/BaoLoi/Index");
            }
            ViewBag.KeyWord = searchString;
            var all_nhanvien = from NHANVIEN in db.tb_NHANVIENs select NHANVIEN;
            if (!string.IsNullOrEmpty(searchString)) all_nhanvien = (IOrderedQueryable<tb_NHANVIEN>)all_nhanvien.Where(a => a.HOTEN.Contains(searchString));

            return View(all_nhanvien);
        }
        public ActionResult Detail(int id)
        {
            var D_NhanVien = db.tb_NHANVIENs.Where(m => m.MANV == id).First();
            return View(D_NhanVien);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_NHANVIEN p)
        {
            var E_HOTEN = collection["HOTEN"];
            var E_GIOITINH = collection["GIOITINH"];
            var E_NGAYSINH = Convert.ToDateTime(collection["NGAYSINH"]);
            var E_DIENTHOAI = collection["DIENTHOAI"];
            var E_CCCD = collection["CCCD"];
            var E_EMAIL = collection["EMAIL"];
            var E_DIACHI = collection["DIACHI"];
            var E_HINHANH = collection["HINHANH"];
            var E_IDPB = collection["IDPB"];
            var E_IDBP = collection["IDBP"];
            var E_IDCV = collection["IDCV"];
            var E_IDDT = collection["IDDT"];
            var E_IDTG = collection["IDTG"];
            var E_IDTD = collection["IDTD"];
            if (string.IsNullOrEmpty(E_HOTEN))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.HOTEN = E_HOTEN.ToString();
                p.GIOITINH = E_GIOITINH.ToString();
                p.NGAYSINH = Convert.ToDateTime(collection["NGAYSINH"]);
                p.DIENTHOAI = E_DIENTHOAI.ToString();
                p.CCCD = E_CCCD.ToString();
                p.EMAIL = E_EMAIL.ToString();
                p.DIACHI = E_DIACHI.ToString();
                p.HINHANH = E_HINHANH.ToString();
                p.IDPB = E_IDPB.AsInt();
                p.IDBP = E_IDCV.AsInt();
                p.IDCV = E_IDTG.AsInt();
                p.IDDT = E_IDDT.AsInt();
                p.IDTG = E_IDTG.AsInt();
                p.IDTD = E_IDTD.AsInt();
                db.tb_NHANVIENs.InsertOnSubmit(p);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            var E_nhanvien = db.tb_NHANVIENs.First(m => m.MANV == id);
            return View(E_nhanvien);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_nhanvien = db.tb_NHANVIENs.First(m => m.MANV == id);
            var E_hoten = collection["HOTEN"];
            var E_gioitinh = collection["GIOITINH"];
            var E_ngaysinh = Convert.ToDateTime(collection["NGAYSINH"]);
            var E_dienthoai = collection["DIENTHOAI"];
            var E_cccd = collection["CCCD"];
            var E_email = collection["EMAIL"];
            var E_diachi = collection["DIAHCI"];
            var E_hinhanh = collection["HINHANH"];
            var E_IDPB = collection["IDPB"];
            var E_IDBP = collection["IDBP"];
            var E_IDCV = collection["IDCV"];
            var E_IDDT = collection["IDDT"];
            var E_IDTG = collection["IDTG"];
            var E_IDTD = collection["IDTD"];
            E_nhanvien.MANV = id;
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "don't empty";
            }
            else
            {
                E_nhanvien.HOTEN = E_hoten.ToString();
                E_nhanvien.GIOITINH = E_gioitinh;
                E_nhanvien.NGAYSINH = E_ngaysinh;
                E_nhanvien.DIENTHOAI = E_dienthoai;
                E_nhanvien.CCCD = E_cccd;
                E_nhanvien.DIACHI = E_diachi;
                E_nhanvien.EMAIL = E_diachi;
                E_nhanvien.HINHANH = E_hinhanh;
                E_nhanvien.IDPB = E_IDPB.AsInt();
                E_nhanvien.IDBP = E_IDBP.AsInt();
                E_nhanvien.IDCV = E_IDCV.AsInt();
                E_nhanvien.IDDT = E_IDDT.AsInt();
                E_nhanvien.IDTG = E_IDTG.AsInt();
                E_nhanvien.IDTD = E_IDTD.AsInt();
                UpdateModel(E_nhanvien);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_NhanVien = db.tb_NHANVIENs.First(m => m.MANV == id);
            return View(D_NhanVien);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_NhanVien = db.tb_NHANVIENs.Where(m => m.MANV == id).First();
            db.tb_NHANVIENs.DeleteOnSubmit(D_NhanVien);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}