﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        //Test DbSet
        DbSet<Product> Products { get; }
        DbSet<Vendor> Vendors { get; }
        DbSet<AdminPurchaseLog> AdminPurchaseLogs { get; }
        DbSet<AvailableStock> AvailableStocks { get; }
        DbSet<Request> OrderRequests { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
