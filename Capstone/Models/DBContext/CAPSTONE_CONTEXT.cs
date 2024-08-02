using Capstone.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Models.DBContext
{
    public class CAPSTONE_CONTEXT : DbContext
    {
        public CAPSTONE_CONTEXT()
        {
        }

        public CAPSTONE_CONTEXT(DbContextOptions<CAPSTONE_CONTEXT> options) : base(options)
        { }


        public virtual DbSet<COUNTRY> COUNTRY { get; set; }
        public virtual DbSet<CITY> CITY { get; set; }
        public virtual DbSet<DISTRICT> DISTRICT { get; set; }
        public virtual DbSet<MATERIAL> MATERIAL { get; set; }
        public virtual DbSet<MATERIAL_GROUP> MATERIAL_GROUP { get; set; }
        public virtual DbSet<MATERIAL_TYPE> MATERIAL_TYPE { get; set; }
        public virtual DbSet<ORDER_STATUS> ORDER_STATUS { get; set; }
        public virtual DbSet<ORDER_DETAIL_STATUS> ORDER_DETAIL_STATUS { get; set; }
        public virtual DbSet<ORDER_ADDRESS> ORDER_ADDRESS { get; set; }
        public virtual DbSet<WAREHOUSE> WAREHOUSE { get; set; }
        public virtual DbSet<WAREHOUSE_ADDRESS> WAREHOUSE_ADDRESS { get; set; }
        public virtual DbSet<WH_MATERIAL> WH_MATERIAL { get; set; }
        public virtual DbSet<FACTORY_DEALER> FACTORY_DEALER { get; set; }
        public virtual DbSet<FACTORY_PRODUCT> FACTORY_PRODUCT { get; set; }


        public DbSet<ORDERS> Orders { get; set; }
        public DbSet<ORDER_DETAIL> ORDER_DETAILs { get; set; }
        public DbSet<REQUESTS> Requests { get; set; }
        public DbSet<REQUESTS_DETAILS> REQUESTS_DETAILS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ORDER_DETAIL>()
                .HasOne<ORDERS>(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(f => f.ID_ORDER)
                .IsRequired();

            modelBuilder.Entity<REQUESTS_DETAILS>()
                .HasOne<REQUESTS>(rd => rd.Request)
                .WithMany(r => r.RequestDetails)
                .HasForeignKey(rd => rd.ID_REQUEST)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
