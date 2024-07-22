using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        public ApplicationDbContext(DbContextOptions options,
                                     IDomainEventService domainEventService,
                                     ICurrentUserService currentUserService,
                                     IDateTime dateTime
            ) : base(options)
        {
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Vendor> Vendors => Set<Vendor>();

        public DbSet<AdminPurchaseLog> AdminPurchaseLogs => Set<AdminPurchaseLog>();

        public DbSet<AvailableStock> AvailableStocks => Set<AvailableStock>();

        public DbSet<Request> OrderRequests => Set<Request>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now.ToUniversalTime();
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now.ToUniversalTime();
                        break;
                }
            }

            var events = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<ApplicationRole>().Property(x => x.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                    .HasMany(u => u.UserClaims)
                    .WithOne()
                    .HasForeignKey(c => c.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                    .HasMany(u => u.UserRoles)
                    .WithOne()
                    .HasForeignKey(r => r.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>()
                    .HasMany(r => r.Claims)
                    .WithOne()
                    .HasForeignKey(c => c.RoleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>()
                    .HasMany(r => r.UserRoles)
                    .WithOne()
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}
