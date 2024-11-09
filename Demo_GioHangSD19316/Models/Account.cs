using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace Demo_GioHangSD19316.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(450, MinimumLength =6, ErrorMessage ="Bắt buộc nhập 6-450 kí tự")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime NgaySinh { get; set; }
        //xxx-xxx-xxxx
        [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$",
     ErrorMessage = "số điên thoại phải đúng format xxx-")]
        //[RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$",
        //   ErrorMessage = "Số điện thoại phải đúng format và có 10 chữ số")]
        public string SDT {  get; set; }
        //đối tượng gh
        public GioHang ? GioHang { get; set; } // đóng vai trò là khóa ngoại
    }
}
