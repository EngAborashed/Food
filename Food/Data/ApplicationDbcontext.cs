using Microsoft.EntityFrameworkCore;
using Food.Models;
using System.Numerics;

namespace Food.Data

{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        { 
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder option)
        //{

        //    option.UseSqlServer("Data Source=DESKTOP-JJAKEH0\\" +
        //        "SQL2022;Initial Catalog=Test1;Integrated Security=True;" +
        //        "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
        //        "Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}
        protected void OnModelCriating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer_Item>()
                .HasOne(b => b.customer)
                .WithMany(ba => ba.customer_Items)
                .HasForeignKey(bi => bi.CustomerId);
            modelBuilder.Entity<Customer_Item>()
                .HasOne(b => b.Items)
                .WithMany(ba => ba.customer_Items)
                .HasForeignKey(bi => bi.ItemsId);
            //modelBuilder.Entity<Customer>()
            //     .Property(b => b.FullName).HasComputedColumnSql("[FirstName]+' '+[LastName]");

        }
       
       
        protected ApplicationDbcontext()
        {
        }

       
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Items>Items { get; set; }
        public DbSet<Payment> Payment { get; set; } 
        public DbSet<Order>Orders { get; set; }
        public DbSet<Customer_Feedback> CustomerFeedbacks { get; set; }
       
       

    } 
         




}



