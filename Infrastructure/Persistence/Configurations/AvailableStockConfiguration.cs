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
    public class AvailableStockConfiguration : IEntityTypeConfiguration<AvailableStock>
    {
        public void Configure(EntityTypeBuilder<AvailableStock> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Product)
                    .WithMany(t => t.AvailableStocks)
                    .HasForeignKey(t => t.ProductId);

            //Self - referencing config
            builder.HasOne(a => a.UpdatedStock)
                    .WithOne()
                    .HasForeignKey<AvailableStock>(a => a.SRId)
                    .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(t => t.TotalQuantity)
                    .HasPrecision(0);


        }
    }
}
