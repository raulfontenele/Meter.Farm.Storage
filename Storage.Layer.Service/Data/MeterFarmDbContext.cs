using Meter.Farm.DTO.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Layer.Service.Data
{
    public class MeterFarmDbContext : DbContext
    {
        public MeterFarmDbContext(DbContextOptions<MeterFarmDbContext> options)
            : base(options)
        {
        }

        public DbSet<CommandStorageObject> CommandStorageObjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommandStorageObject>().ToTable("CommandStorageObjects");
        }
    }
}
