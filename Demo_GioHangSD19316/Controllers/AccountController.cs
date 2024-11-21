using Demo_GioHangSD19316.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_GioHangSD19316.Controllers
{
    public class AccountController : Controller
    {
        //gọi class đại diện cho csdl vào đây để sử dụng
        private readonly GHangDbContext _db;
        //Tiêm DI: tiêm csdl vào trong controler
        public AccountController(GHangDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //lấy giá trị session đã lưu . lấy thông tin ngừi dùng đăng nhập
            //key:cun. vlue: username
    
            var sesionData = HttpContext.Session.GetString("cun");
            //nếu k đăng nhập thì k cho xem thông tin bên trong
            if(sesionData == null)
            {
                return Content("Đăng nhập đi bạn ê!!!!");
            }
            else
            {
                ViewData["mess"] = $"chào mừng {sesionData} đã đến với chúng tôi";
            }
            //lấy tòa bộ dữ liệu của account
            var accData = _db.Accounts.ToList();
            
            return View(accData);
        }

        //Chức năng đăng kí
        //tạo ra form đăng kí
        [HttpGet] // nếu k ns gì thì mặc định là httpget
        public IActionResult DangKy()
        {
            return View();
        }
        //xử lí logic, tương tac vs db
        public IActionResult DangKy(Account acc)
        {
            try
            {
                //tạo mới 1 account
                _db.Accounts.Add(acc);
                //khi tạo 1 acc đồng thời sẽ tạo 1 giỏ hàng
                GioHang gioHang = new GioHang()  
                {
                    UserName = acc.UserName,
                    AccountId = acc.Id
                };
                //add giổ hàng
                _db.GioHangs.Add(gioHang);
                _db.SaveChanges();
                TempData["Status"] = " Chúc mừng bạn đã tạo tài khoản thành công";
                return RedirectToAction("Login");
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }

        //đăng nhập
        //mở view login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string userName, string passWord)
        {
            //check xem username và pas có nhập hay
            if (userName == null || passWord == null)
            {
                return View();// nếu k nhập thông tin thì giữ nguyên view
            }
            //nếu nhập thì tìm kiếm xem có đúng tk, mk không
            var acc = _db.Accounts.ToList().FirstOrDefault(x => x.UserName == userName && x.Password == passWord); ;
            if (acc == null)
            {
                return Content("Tài khoản hoặc mk không chính xác");
            }
            else
            {
                //lưu dữ liệu login vào sesion vs key là cun
                HttpContext.Session.SetString("cun", userName);
                return RedirectToAction("Index", "SanPham");
            }
            return View();
        }
    }
}
