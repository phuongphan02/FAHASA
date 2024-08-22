using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLNSFAHASA.Models;

namespace QLNSFAHASA.Controllers
{
    public class KhachHangsController : Controller
    {
        private QLNSFAHASAEntities db = new QLNSFAHASAEntities();

        // GET: KhachHangs
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        // GET: KhachHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: KHACHHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "MaKH,HoTenKH,DiaChiKH,CMND,SDT,MatKhau,Email")] KhachHang khachHang)
        {
            string maKH = GetNextMaKH(); // Lấy mã MaKH mới từ cơ sở dữ liệu
            khachHang.MaKH = maKH; // Gán giá trị của mã MaKH mới cho thuộc tính MaKH của đối tượng model
            db.KhachHangs.Add(khachHang);
            bool isEmailExists = IsEmailExists(khachHang.Email);
            if (isEmailExists)
            {
                ViewBag.RegisterFail = "Email đã tồn tại trên hệ thống!";
                return View("Register");
            }
            else
            {
                db.SaveChanges();
                return RedirectToAction("Login");
            }
        }

        private string GetNextMaKH()
        {
            var lastMaKH = db.KhachHangs.OrderByDescending(m => m.MaKH).FirstOrDefault();
            if (lastMaKH != null)
            {
                string lastNumber = lastMaKH.MaKH.Substring(2); // Trích xuất phần số của MaKH cuối cùng
                int nextNumber = int.Parse(lastNumber) + 1; // Tăng giá trị số lên một đơn vị
                return "KH" + nextNumber.ToString("D2"); // Tạo mã MaKH mới
            }
            else
            {
                return "KH1"; // Trường hợp cơ sở dữ liệu chưa có mã MaKH nào
            }
        }
        private bool IsEmailExists(string Email)
        {
            var isEmailExists = db.KhachHangs.SingleOrDefault(x => x.Email.Equals(Email));
            if (isEmailExists != null)
                return true;
            else
                return false;
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Login(NhanVien nhanVien, KhachHang khachHang)
        {
            var EmailFormKH = khachHang.Email;
            var matkhauFormKH = khachHang.MatKhau;
            var userCheckKH = db.KhachHangs.SingleOrDefault(x => x.Email.Equals(EmailFormKH) && x.MatKhau.Equals(matkhauFormKH));

            var emailFormNV = nhanVien.Email;
            var matkhauFormNV = nhanVien.MatKhau;
            var userCheckNV = db.NhanViens.SingleOrDefault(x => x.Email.Equals(emailFormNV) && x.MatKhau.Equals(matkhauFormNV));

            if (userCheckKH != null)
            {
                // Lưu thông tin người dùng vào Session.
                Session["KhachHang"] = userCheckKH;

                // Chuyển hướng người dùng tới trang chính của ứng dụng.
                return RedirectToAction("Index", "SanPhams");
            }
            else if (userCheckKH == null && userCheckNV != null)
            {
                // Lưu thông tin người dùng vào Session.
                Session["NhanVien"] = userCheckNV;

                // Chuyển hướng người dùng tới trang chính của ứng dụng.
                return RedirectToAction("Index", "SanPhams");
            }
            else
            {
                ViewBag.LoginFail = "Sai Email hoặc mật khẩu!";
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["KhachHang"] = null;
            Session["NhanVien"] = null;
            return RedirectToAction("Index", "SanPhams");
        }
        // GET: KhachHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,HoTenKH,DiaChiKH,CMND,SDT,MatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
