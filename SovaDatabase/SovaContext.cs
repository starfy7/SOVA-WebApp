using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace SovaDatabase
{
    public class SovaContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<SovaUser> SovaUsers { get; set; }
        public DbSet<Posttype> Posttypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql("server=localhost;database=stackoverflow_sample_universal;uid=root;pwd=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Posts Table
            modelBuilder.Entity<Post>().ToTable("posts");
            modelBuilder.Entity<Post>().Property(x => x.PostTypeId).HasColumnName("post_type_id");
            modelBuilder.Entity<Post>().Property(x => x.ParentId).HasColumnName("parent_id");
            modelBuilder.Entity<Post>().Property(x => x.AcceptedAnswerId).HasColumnName("accepted_answer_id");
            modelBuilder.Entity<Post>().Property(x => x.CreationDate).HasColumnName("creation_date");
            modelBuilder.Entity<Post>().Property(x => x.ClosedDate).HasColumnName("closed_date");
            modelBuilder.Entity<Post>().Property(x => x.UserId).HasColumnName("user_id");
            
            //Posttypes Table
            modelBuilder.Entity<Posttype>().ToTable("posttype");

            //SovaUsers Table
            modelBuilder.Entity<SovaUser>().ToTable("sovausers");
            modelBuilder.Entity<SovaUser>().Property(x => x.Id).HasColumnName("userid");
            modelBuilder.Entity<SovaUser>().Property(x => x.Nick).HasColumnName("nickname");
            modelBuilder.Entity<SovaUser>().Property(x => x.Birthday).HasColumnName("dateofbirth");
            modelBuilder.Entity<SovaUser>().Property(x => x.Private).HasColumnName("isprivate");

            //Comments Table
            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("comment_id");
            modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<Comment>().Property(x => x.CreationDate).HasColumnName("creation_date");
            modelBuilder.Entity<Comment>().Property(x => x.UserId).HasColumnName("user_id");

            //History Table
            modelBuilder.Entity<History>().ToTable("history");
            modelBuilder.Entity<History>().HasKey("UserId", "PostId");

            //Users Table
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.CreationDate).HasColumnName("creation_date");

            //Tags Table
            modelBuilder.Entity<Tag>().ToTable("tags");
        }

    }
}
