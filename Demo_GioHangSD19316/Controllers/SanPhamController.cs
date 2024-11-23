using Demo_GioHangSD19316.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;


namespace Demo_GioHangSD19316.Controllers
{
    public class SanPhamController : Controller
    {
        //gọi class đại diện cho csdl 
        GHangDbContext _db;
        public SanPhamController(GHangDbContext db)
        {
            _db = db;   
        }
        public IActionResult Index(string name, int page=3, int pageSize=2)
        {
            //lấy giá trị session có tên là ussername
            var session = HttpContext.Session.GetString("cun");
            if (session == null)
            {
                TempData["mess"] = "Chưa đăng nhập cháu ơi";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewData["mess1"] = $"Mời {session} xem sản phẩm";
                //var spList = _db.SanPhams.ToList(); // lay toan bo danh sach

                //return View(spList);
            }
            var lst =_db.SanPhams.ToPagedList(page,pageSize);

            //CHUC NANG TIM KIEMM
            
            //check xem 
            //CHECK NAME COS DC NHAP HAY K
            if(string.IsNullOrEmpty(name))
            {
                return View(lst);
            }
            else
            {
                var search = _db.SanPhams
                    .Where(x => x.SanPhamName.ToLower().Contains(name.ToLower()))
                    .ToList();
                if(search.Count ==0)
                {
                    return View(lst) ;

                }
                else
                {
                    return View(search);
                }
            }
           

             
        }
        public IActionResult Create()  //Tạ ra view creaate
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SanPham sanPham)  //xử lí logic
        {
            _db.SanPhams.Add(sanPham);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

        //tạo ra form edit có chứa dữ liệu của đối tượng cân edit
        public IActionResult Edit(Guid id)
        {
            //tìm kiếm đối tượng cần edit
            var sp = _db.SanPhams.Find(id);
            return View(sp);
        }
        [HttpPost]
        public IActionResult Edit(SanPham sp)
        {
            _db.SanPhams.Update(sp);
            _db.SaveChanges();
            return RedirectToAction("edit");
        }

        public IActionResult Details(Guid id) 
        {
            //tìm kiếm đối tượng cần edit
            var sp = _db.SanPhams.Find(id);
            return View(sp);
        }

        //thêm sản phẩm vào giỏ hàng 
        public IActionResult AddToCart(Guid id, int soLuong)//id này là ip của sp được add
        {
            //Lấy ra userName tương ứng vs phiên đăng nhập
            //cun = UserName
            var acc = HttpContext.Session.GetString("cun");
            if(acc == null)
            {
                return Content("Chưa đăng nhập hoặc phiên đăng nhập hết hạn");
            }
            //lấy ra thông tin của người dùng vs user trên
            var getAcc = _db.Accounts.FirstOrDefault(x => x.UserName == acc);
            //lấy giỏ hàng tương ứng vs người dùng
            var giohang = _db.GioHangs.FirstOrDefault(x => x.AccountId == getAcc.Id);
            if(giohang==null)
            {
                return Content("Chưa có giỏ hàng");
            }

            //lấy tòa bộ sản phẩn có trong ghct của acc

            var accCart = _db.GHCTs.Where(x => x.GioHangId == giohang.Id).ToList();

            //DUYỆT GHCT
            bool check = false;
            Guid idGHCT = Guid.NewGuid();

            foreach (var item in accCart)
            {
                if(item.SanPhamId == id)  //nếu sp bị trùng
                {
                    check = true;
                    idGHCT = item.Id; //laasy ra id để tí wnax update
                    break;
                }    
            }
            //nếu sp chưa đc chọn
            if (!check)
            {
                //tạo 1 ghct mới
                GHCT ghct = new GHCT()
                {
                    SanPhamId = id,
                    GioHangId= giohang.Id,
                    SoLuong= soLuong,
                };
                _db.GHCTs.Add(ghct);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var ghctUpdate = _db.GHCTs.FirstOrDefault(x => x.Id == idGHCT);
                ghctUpdate.SoLuong = ghctUpdate.SoLuong + soLuong;
                _db.GHCTs.Update(ghctUpdate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }


            

        }

    }
}
