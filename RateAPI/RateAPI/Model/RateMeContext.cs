using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RateAPI.Model
{
    public class RateMeContext: DbContext
    {
        public RateMeContext(DbContextOptions<RateMeContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(@"Server=loclhost;database=rateme;uid=root;pwd=;");

        private DbSet<Comments> comments;

        private DbSet<Followings> followings;

        private DbSet<Games> games;

        private DbSet<Users> users;

        public DbSet<Comments> Comments { get => comments; set => comments = value; }
        public DbSet<Followings> Followings { get => followings; set => followings = value; }
        public DbSet<Games> Games { get => games; set => games = value; }
        public DbSet<Users> Users { get => users; set => users = value; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.ToTable("comments");

                entity.HasIndex(e => e.Commenter)
                    .HasName("commenter");

                entity.HasIndex(e => e.Game)
                    .HasName("game");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Commenter)
                    .HasColumnName("commenter")
                    .HasMaxLength(255);

                entity.Property(e => e.Game)
                    .HasColumnName("game")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("text");

                
            });

            modelBuilder.Entity<Followings>(entity =>
            {
                entity.ToTable("followings");

                entity.HasIndex(e => e.Follower)
                    .HasName("follower");

                entity.HasIndex(e => e.Game)
                    .HasName("game");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Follower)
                    .HasColumnName("follower")
                    .HasMaxLength(255);

                entity.Property(e => e.Game)
                    .HasColumnName("game")
                    .HasColumnType("int(11)");
                
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.ToTable("games");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserPass)
                    .IsRequired()
                    .HasColumnName("userPass")
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255);
            });
        }
    }
}
