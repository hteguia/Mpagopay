﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mpagopay.Application.Contrats;
using Mpagopay.Domain.Common;
using Mpagopay.Domain.Entities.BankAccounts;
using Mpagopay.Domain.Entities.Tarification;
using Mpagopay.Domain.Entities.Users;
using Mpagopay.Domain.Entities.VirtualCard;

namespace Mpagopay.Persistence
{
    public class MpagopayDbContext : DbContext
    {
        private readonly ILoggedInUserService? _loggedInUserService;
        public MpagopayDbContext(DbContextOptions<MpagopayDbContext> options):base(options)
        {

        }

        public MpagopayDbContext(DbContextOptions<MpagopayDbContext> options, ILoggedInUserService loggedInUserService) : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<PricingDetail> PricingDetails { get; set; }
        public DbSet<CardRecharge> CardRecharges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CreditBankAccount> CreditBankAccounts { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MpagopayDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate= DateTime.Now;
                        entry.Entity.CreateBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.CreateBy = _loggedInUserService.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
