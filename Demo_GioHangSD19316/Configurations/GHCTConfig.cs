using Demo_GioHangSD19316.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GioHangSD19316.Configurations
{
    public class GHCTConfig : IEntityTypeConfiguration<GHCT>
    {
        public void Configure(EntityTypeBuilder<GHCT> builder)
        {
            builder.HasKey(x => x.Id);
            // thiêt lập mqh 1-n vs gió hàng
            builder.HasOne(x => x.GioHang)
                .WithMany(x => x.GHCTs)
                .HasForeignKey(x => x.GioHangId);

            //Thiest lập mqh 1-n SanPham
            builder.HasOne(x => x.SanPham)
                .WithMany(x => x.GHCTs)
                .HasForeignKey(x => x.SanPhamId);
        }
    }
}
