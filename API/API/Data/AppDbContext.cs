﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.FileExtensions;

namespace Glomad.Models
{
    public static class ContextExtensions
    {
        public static void AddOrUpdate(this AppDbContext ctx, object entity)
        {
            var entry = ctx.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    ctx.Add(entity);
                    break;
                case EntityState.Modified:
                    ctx.Update(entity);
                    break;
                case EntityState.Added:
                    ctx.Add(entity);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserInterest>()
                .HasKey(ui => new { ui.InterestId, ui.UserId });
            builder.Entity<UserInterest>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.Interests)
                .HasForeignKey(ui => ui.UserId);
            builder.Entity<UserInterest>()
                .HasOne(ui => ui.Interest)
                .WithMany(i => i.UserInterests)
                .HasForeignKey(ui => ui.InterestId);

            base.OnModelCreating(builder);
        }

        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Embassy> Embassy { get; set; }
        public DbSet<VisaEmbassy> VisaEmbassy { get; set; }
        public DbSet<VisaAcceptance> VisaAcceptance { get; set; }
        public DbSet<Power> Power { get; set; }
        public DbSet<CityWeather> CityWeather { get; set; }
        public DbSet<ClimateData> ClimateData { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Interest> Interest { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<VisaDoc> VisaDoc { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Visa> Visa { get; set; }
        public DbSet<NoVisaEntry> NoVisaEntry { get; set; }
    }
}
