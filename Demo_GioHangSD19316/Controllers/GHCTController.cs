using Demo_GioHangSD19316.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_GioHangSD19316.Controllers
{
    public class GHCTController : Controller
    {
        GHangDbContext _db;
        public GHCTController(GHangDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //Lấy ra userName tương ứng vs phiên đăng nhập
            //cun = UserName
            var acc = HttpContext.Session.GetString("cun");
            if (acc == null)
            {
                return Content("Chưa đăng nhập hoặc phiên đăng nhập hết hạn");
            }
            else
            {
                //lấy ra thông tin của người dùng vs user trên
                var getAcc = _db.Accounts.FirstOrDefault(x => x.UserName == acc);
                //lấy giỏ hàng tương ứng vs người dùng
                var giohang = _db.GioHangs.FirstOrDefault(x => x.AccountId == getAcc.Id);


                //lấy tòa bộ sản phẩn có trong ghct của acc

                var data = _db.GHCTs.Where(x => x.GioHangId == giohang.Id).ToList();
                return View(data);
            }
          
        }
    }
}
