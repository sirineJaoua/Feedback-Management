using FeebackApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FeebackApplication.DAL
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext() : base("FeebackContext") { }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackDetail> FeedbackDetails{ get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Statue> Status { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }

        public DbSet<NewModel> NewModels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

       
    }

    }
