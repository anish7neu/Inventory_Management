﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);

            //builder.HasOne(t => t.AdminPurchaseLog)
            //        .WithMany(t=>t.Products)
            //        .HasForeignKey(t=>t.APLId);

            //builder.HasOne(t => t.Request)
            //        .WithMany(t => t.Products)
            //        .HasForeignKey(t => t.RequestId);

            //builder.Property(t => t.ProductName)
            //        .HasMaxLength(30);
            //builder.Property(t => t.ProductDescription)
            //        .HasMaxLength(100);



        }
    }
}
