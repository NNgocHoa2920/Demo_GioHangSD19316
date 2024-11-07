namespace Demo_GioHangSD19316.Models
{
    public class SanPham
    {
        public Guid SanPhamId { get; set; }
        public string SanPhamName { get; set; }
        public decimal Price {  get; set; }

        //thiết lập mqh 1-n
        //Icolection, ilist, colection, list: dùng để biểu thị nhiều
        //ghcts: navigation dùng để gọi đến khi cần
        public List<GHCT> GHCTs { get; set; }
    }
}
