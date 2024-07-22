using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class AdminPurchaseLogConfiguration: IEntityTypeConfiguration<AdminPurchaseLog>
    {
        public void Configure(EntityTypeBuilder<AdminPurchaseLog> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Vendor)
                    .WithMany(u=>u.AdminPurchaseLogs)
                    .HasForeignKey(t => t.VendorId);
            
            builder.Property(t => t.Quantity)
                    .HasPrecision(0);

            builder.HasOne(t => t.Product)
                    .WithMany(t => t.AdminPurchaseLogs)
                    .HasForeignKey(t => t.ProductId);
        }
    }
}
