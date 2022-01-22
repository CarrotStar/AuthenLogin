using AuthenLoginDeploy.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AuthenLoginDeploy.Models
{
    public partial class AuthenDbContext : DbContext
    {
        //public AuthenDbContext()
        //{
        //}

        public AuthenDbContext(DbContextOptions<AuthenDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUsers> TblUsers { get; set; }
        public virtual DbSet<TblHistoryUser> TblHistoryUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySQL("Server=localhost;User=root;Password=;Database=authendb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("User_Id");

                entity.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("FirstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LastName");

              

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("UserName");


                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Password"); 

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Salt"); 

                entity.Property(e => e.Profileimg)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Profileimg");

                entity.Property(e => e.EditCount)
                  .HasColumnType("int(11)")
                  .HasColumnName("EditCount");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                  .HasColumnName("CreateDate");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                  .HasColumnName("UpdateDate");
            });

            modelBuilder.Entity<TblHistoryUser>(entity =>
            {
                entity.ToTable("History_users");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("Id");                
                
                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("User_Id");

                entity.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("FirstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LastName");



                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("UserName");


                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Password"); ;

                entity.Property(e => e.Salt)
                     .IsRequired()
                     .HasMaxLength(255)
                     .HasColumnName("Salt");

                entity.Property(e => e.Profileimg)
                    .IsRequired()
                    .HasColumnName("Profileimg");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                  .HasColumnName("UpdateDate");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
