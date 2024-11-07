using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_GioHangSD19316.Models
{
    public class GioHang
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        //khởi tạo 1 đói tượng account để thể hiện 1-1 vs GH
        public Account? Account { get; set; }
        [ForeignKey("Account")]
        public Guid? AccountId { get; set; } // khóa ngoại 
        //thiết lập 1 gh co n ghct
        public List<GHCT> GHCTs { get; set; }
    }
}
