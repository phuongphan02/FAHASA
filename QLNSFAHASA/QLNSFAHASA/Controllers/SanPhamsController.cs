using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLNSFAHASA.Models;

namespace QLNSFAHASA.Controllers
{
    public class SanPhamsController : Controller
    {
        private QLNSFAHASAEntities db = new QLNSFAHASAEntities();


        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMucLoaiSanPham).Include(s => s.DonViTinh).Include(s => s.NCC);
            return View(sanPhams.ToList());
        }
        public ActionResult RemoveFromCart(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Lấy sản phẩm từ database
            var product = db.SanPhams.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            // Lấy giỏ hàng từ session
            var cart = Session["cart"] as List<SanPham> ?? new List<SanPham>();

            // Xóa sản phẩm khỏi giỏ hàng
            cart.RemoveAll(p => p.MaSP == id);

            // Lưu giỏ hàng vào session
            Session["cart"] = cart;

            // Chuyển hướng đến trang giỏ hàng
            return RedirectToAction("Cart");
        }

        public ActionResult AddToCart(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Lấy sản phẩm từ database
            var product = db.SanPhams.FirstOrDefault(p => p.MaSP == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            // Lấy giỏ hàng từ session
            var cart = Session["cart"] as List<SanPham> ?? new List<SanPham>();

            // Thêm sản phẩm vào giỏ hàng
            cart.Add(product);

            // Lưu giỏ hàng vào session
            Session["cart"] = cart;

            // Chuyển hướng đến trang giỏ hàng
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            if (Session["KhachHang"] != null)
            {
                // Lấy giỏ hàng từ session
                var cart = Session["cart"] as List<SanPham> ?? new List<SanPham>();

                return View(cart);
            }
            else
            {
                return RedirectToAction("Login", "KhachHangs");

            }
        }


        // GET: SanPhams
        
        public ActionResult IndexAdmin()
        {
            if (Session["NhanVien"] != null)
            {
                var sanPhams = db.SanPhams.Include(p => p.DanhMucLoaiSanPham);
                return View(sanPhams.ToList());
            }
            else
            {
                return RedirectToAction("Index", "SanPhams");

            }
        }
        public ActionResult SanPhamTheoLoai(string maLSP)
        {
            var sanPhams = db.SanPhams.Where(sp => sp.MaLSP == maLSP).ToList();
            return View(sanPhams);
        }
        public ActionResult IndexAdminDSLSP()
        {
            if (Session["NhanVien"] != null)
            {
                var sanPhams = db.SanPhams.Include(c => c.DanhMucLoaiSanPham).Include(c => c.DonViTinh).Include(c => c.NCC);
                return View(sanPhams.ToList());
            }
            else
            {
                return RedirectToAction("Index", "SanPhams");
            }
        }

        public ActionResult Search(string searchString)
        {
            var sanPhams = db.SanPhams.Where(s => s.TenSP.Contains(searchString)).ToList();
            return View(sanPhams);
        }

        public ActionResult SearchAdmin(string searchString)
        {
            var sanPhams = db.SanPhams.Where(s => s.TenSP.Contains(searchString)).ToList();
            return View(sanPhams);
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(db.DanhMucLoaiSanPhams, "MaLSP", "TenLSP");
            ViewBag.MaDVT = new SelectList(db.DonViTinhs, "MaDVT", "DVT");
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,MoTa,DonGia,SoLuong,NXB,ANH,MaNCC,MaLSP,MaDVT")] SanPham sanPham)
        {
            var ANH = Request.Files["ANH"];
            //Lấy thông tin từ input type=file có tên anhBia
            string postedFileName = System.IO.Path.GetFileName(ANH.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            ANH.SaveAs(path);
            if (ModelState.IsValid)
            {
                sanPham.ANH = postedFileName;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLSP = new SelectList(db.DanhMucLoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaDVT = new SelectList(db.DonViTinhs, "MaDVT", "DVT", sanPham.MaDVT);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLSP = new SelectList(db.DanhMucLoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaDVT = new SelectList(db.DonViTinhs, "MaDVT", "DVT", sanPham.MaDVT);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,MoTa,DonGia,SoLuong,NXB,ANH,MaNCC,MaLSP,MaDVT")] SanPham sanPham)
        {
            var ANH = Request.Files["ANH"];
            try
            {
                //Lấy thông tin từ input type=file có tên ANH
                string postedFileName = System.IO.Path.GetFileName(ANH.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                ANH.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLSP = new SelectList(db.DanhMucLoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaDVT = new SelectList(db.DonViTinhs, "MaDVT", "DVT", sanPham.MaDVT);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
