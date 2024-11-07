using Demo_GioHangSD19316.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GioHangSD19316.Configurations
{
    public class SanPhamConfig : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder< SanPham> builder)
        {
            builder.HasKey(x=>x.SanPhamId);
        }
    }
}
