
using Demo_GioHangSD19316.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GioHangSD19316.Configuration
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
           //thiết lập khóa chính
           builder.HasKey(x => x.Id);
            //cconfig cho các thuộc tính

            //thuộc tính pass => matkhau  có 256 kí tư
            builder.Property(x => x.Password).IsRequired()
                .HasColumnName("MatKhau")
                .HasColumnType("varchar(255)");
            //thiết lập name nvarchar(256)
            builder.Property(x => x.Name).IsRequired()
                .IsUnicode(true)
                .IsFixedLength(true)
                .HasMaxLength(256);
            //thiết lâp mqh 1-1 vs gh
            //hasone: chỉ ra mqh đầuu 1
            //withone: chỉ ra bảng 1

            builder.HasOne(x => x.GioHang)
                .WithOne(x => x.Account)
                .HasForeignKey<GioHang>(x => x.AccountId);

        }
    }
}
