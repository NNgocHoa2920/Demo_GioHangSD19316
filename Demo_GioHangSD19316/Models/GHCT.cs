namespace Demo_GioHangSD19316.Models
{
    public class GHCT
    {
        public Guid Id { get; set; }
        public Guid? SanPhamId { get; set; } //khóa ngọi
        public Guid? GioHangId { get; set; }//khóa ngoại
        //thể hiện bảng 1
        public GioHang? GioHang { get; set; }
        public SanPham? SanPham { get; set; }
        public int SoLuong {  get; set; }

    }
}
