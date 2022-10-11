using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace WFM_WebAPI.Models
{
    public class SQL_DBContext : DbContext
    {
        public SQL_DBContext(DbContextOptions<SQL_DBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skillmap>().HasKey(sc => new { sc.employee_id, sc.skillid });

            modelBuilder.Entity<Skillmap>()
                .HasOne(s => s.Skills)
                .WithMany(sm => sm.Skillmaps)
                .HasForeignKey(si => si.skillid);

            modelBuilder.Entity<Skillmap>()
              .HasOne(e => e.Employees)
              .WithMany(sm => sm.Skillmaps)
              .HasForeignKey(si => si.employee_id);



        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Skillmap> Skillmaps { get; set; }

        public DbSet<Employees_Skills> Employees_Skills { get; set; }
        public DbSet<Softlock> Softlock { get; set; }
        public DbSet<SoftlockRequest> SoftlockRequests { get; set; }
        public DbSet<User> Users { get; set; }


    }

}
