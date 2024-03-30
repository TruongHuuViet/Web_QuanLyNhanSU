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
    public class ProfileController : Controller
    {
        dbQLNSDataContext db = new dbQLNSDataContext();
        public ActionResult Detail(int id)
        {
            var D_NhanVien = db.tb_NHANVIENs.Where(m => m.MANV == id).First();
            return View(D_NhanVien);
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
            var E_diachi = collection["DIACHI"];
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
    }
}
