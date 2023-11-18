using Microsoft.EntityFrameworkCore;
using MixedAssessmentEcom.Domain.Configurations;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
              
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<UserOtp> UserOtps { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<MasterCart> Carts { set; get; }
        public DbSet<CartDetail> CartDetails { set; get; }
        public DbSet<PaymentCard> PaymentCards { set; get; }
        public DbSet<SalesMaster> SalesMasters { set; get; }
        public DbSet<SalesDetail> SalesDetails { set; get; }
        public DbSet<OrderHistory> OrderHistory { set; get; }
        public DbSet<Discount> DiscountDetails { set; get; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // user context

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(e => e.UserId);
                builder.Property(e => e.UserId).UseIdentityColumn();
                builder.Property(e => e.Firstname);
                builder.Property(e => e.Lastname);
                builder.Property(e => e.Email);
                builder.Property(e => e.UserType);
                builder.Property(e => e.DOB);
                builder.Property(e => e.Username);
                builder.Property(e => e.Password);
                builder.Property(e => e.MobileNumber);
                builder.Property(e => e.Address);
                builder.Property(e => e.Zipcode).HasMaxLength(6);
                builder.Property(e => e.Country);
                builder.Property(e => e.State);
                builder.Property(e => e.ProfileImage);



               // builder.HasOne<UserOtp>(e => e.Otp).
               //WithOne(e => e.User).
               //HasForeignKey<UserOtp>(d => d.UserId)
               //.IsRequired();


                builder.HasMany<UserOtp>(a => a.Otp).
             WithOne(s => s.User).
             HasForeignKey(s => s.UserId);
            });
            

        
            // country context 

            modelBuilder.Entity<Country>(countryBuilder =>
            {
                countryBuilder.HasKey(s => s.CountryId);
                countryBuilder.Property(e => e.CountryId).UseIdentityColumn();
                countryBuilder.Property(e => e.CountryName);


              countryBuilder.HasMany<State>(a => a.States).
              WithOne(s => s.Country).
              HasForeignKey(s => s.CountryId);
            });
           

            // state code context

            modelBuilder.Entity<State>(builder =>
            {
                builder.HasKey(s => s.StateId);
                builder.Property(e => e.StateId).UseIdentityColumn();
                builder.Property(e => e.StateName);   
            });

            modelBuilder.Entity<UserOtp>(builder =>
            {
                builder.HasKey(p => p.OtpId);
                builder.Property(e => e.OtpId).UseIdentityColumn();
                builder.Property(p=>p.OtpName);
                builder.Property(p => p.ExpireTimeOtp);
                builder.Property(p => p.CreatedTimeOtp);
               


            });

            modelBuilder.Entity<Product>(skillBulder =>
            {
                skillBulder.HasKey(s => s.ProductId);
                skillBulder.Property(e => e.ProductId).UseIdentityColumn();
                skillBulder.Property(e => e.ProductName);
                skillBulder.Property(e => e.ProductCode);
                skillBulder.Property(e => e.ProductImage);
                skillBulder.Property(e => e.Category);
                skillBulder.Property(e => e.Brand);
                skillBulder.Property(e => e.SellingPrice);
                skillBulder.Property(e => e.PurchasePrice);
                skillBulder.Property(e => e.SellingDate);
                skillBulder.Property(e => e.Stock);
                skillBulder.Property(e => e.DiscountId);




                // skillBulder.HasOne<CartDetail>(e => e.CartDetail).
                //WithOne(e => e.Product).
                //HasForeignKey<CartDetail>(d => d.ProductId)
                //.IsRequired();



            });

            //base.OnModelCreating(modelBuilder);
            //new CountryConfiguration (modelBuilder).Seed();


            ///  Cart table 
            modelBuilder.Entity<MasterCart>(countryBuilder =>
            {
                countryBuilder.HasKey(s => s.CartId);
                countryBuilder.Property(e => e.CartId).UseIdentityColumn();
                countryBuilder.Property(e => e.UserId);
                countryBuilder.Property(e => e.IsPaymentDone);


                //countryBuilder.HasMany<CartDetail>(a => a.CartDetails).
                //WithOne(s => s.Cart).
                //HasForeignKey(s => s.CartId);
            });


            // cart details relation 

            modelBuilder.Entity<CartDetail>(builder =>
            {
                builder.HasKey(s => s.CartDetailId);
                builder.Property(e => e.CartDetailId).UseIdentityColumn();
                builder.Property(e => e.Quantity);

                // cart details table has one to many relation with product table

                //builder.HasMany<Product>(a => a.Products).
                //WithOne(s => s.CartDetail).
                //HasForeignKey(s => s.CartDetailId);

               
            });

            modelBuilder.Entity<PaymentCard>(builder =>
            {
                builder.HasKey(s => s.CardId);
                builder.Property(e => e.CardId).UseIdentityColumn();
                builder.Property(e => e.CardNumber);
                builder.Property(e =>e.ExpiryDate);
                builder.Property(e => e.CVV);
            });
            // make a context part for Sales Master'

            modelBuilder.Entity<SalesMaster>(builder =>
            {
                builder.HasKey(s => s.SaleMasterId);
                builder.Property(e => e.SaleMasterId).UseIdentityColumn();
                builder.Property(e => e.InvoiceId);
                builder.Property(e => e.UserId);
                builder.Property(e => e.InvoiceDate);
                builder.Property(e => e.Subtotal);
                builder.Property(e => e.DeliveryAddress);
                builder.Property(e => e.DeliveryZipcode);
                builder.Property(e => e.DeliveryCountry);
                builder.Property(e => e.DeliveryState);

             //   builder.HasOne<SalesDetail>(e => e.SalesDetails).
             //WithOne(e => e.SalesMaster).
             //HasForeignKey<SalesDetail>(d => d.InvoiceId)
             //.IsRequired();
            });


            //Sales dedtails Context

            modelBuilder.Entity<SalesDetail>(builder =>
            {
                builder.HasKey(s => s.SaleDetailId);
                builder.Property(e => e.SaleDetailId).UseIdentityColumn();
                builder.Property(e => e.ProductId);
                builder.Property(e => e.ProductCode);
                builder.Property(e => e.SalesQuantity);
                builder.Property(e => e.NewStock);
                builder.Property(e => e.SellingPrice);
            });

            modelBuilder.Entity<OrderHistory>(builder =>
            {
                builder.HasKey(s => s.OrderId);
                builder.Property(e => e.OrderId).UseIdentityColumn();
                builder.Property(e => e.UserId);
                builder.Property(e => e.InvoiceNumber);
                builder.Property(e => e.DeliveryDate);
                builder.Property(e => e.ProductImage);
                builder.Property(e => e.ProductName);
                builder.Property(e => e.Qty);
                builder.Property(e => e.Price);

            });

            modelBuilder.Entity<Discount>(builder =>
            {
                builder.HasKey(s => s.DiscountId);
                builder.Property(e => e.DiscountId).UseIdentityColumn();
                builder.Property(e => e.DiscountName);
                builder.Property(e => e.DiscountRate);
                builder.Property(e => e.Status);
            });
        }
    }
}
