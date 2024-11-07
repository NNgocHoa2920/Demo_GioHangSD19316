using Microsoft.EntityFrameworkCore;

namespace Demo_GioHangSD19316.Models
{
    public class GHangDbContext : DbContext
    {
        //nếu để chuỗi kết nối ở trong clss dbcontext thì bắt buộc
        //phải có contructor k có tham số
        //nếu để chuỗi ở appseting thì có hoặc k cx đc
        public GHangDbContext(DbContextOptions options) : base(options)
        {
        }

        //tạo các db set: có bn class thì có bấy nhiêu db set
        //dbset: đại diện cho 1 thực thê = 1 bảng
        //khi mà crud thì gọi tới db set = gọi tới tên bảng
        public DbSet<Account> Accounts { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<SanPham> SanPhams{ get; set; }
        public DbSet<GHCT> GHCTs { get; set; }

    }
}
