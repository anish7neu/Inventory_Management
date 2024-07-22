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
    internal class RequestConfiguration: IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Product)
                   .WithMany(t => t.Requests)
                   .HasForeignKey(t => t.ProductId);

            builder.Property(t => t.Remarks)
                   .HasMaxLength(100);

            builder.Property(t=>t.Quantity)
                   .HasPrecision(0);
        }
    }
}
