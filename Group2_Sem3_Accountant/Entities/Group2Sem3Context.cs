using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Group2_Sem3_Accountant.Entities;

public partial class Group2Sem3Context : DbContext
{
    public static string connectionString;
    public Group2Sem3Context()
    {
    }

    public Group2Sem3Context(DbContextOptions<Group2Sem3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Advance> Advances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Financein> Financeins { get; set; }

    public virtual DbSet<Financeout> Financeouts { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Typefinancein> Typefinanceins { get; set; }

    public virtual DbSet<Typefinanceout> Typefinanceouts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__advances__3213E83FC9992790");

            entity.ToTable("advances");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("amount");
            entity.Property(e => e.DateOfAdvances)
                .HasColumnType("date")
                .HasColumnName("date_of_advances");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((0))")
                .HasColumnName("status");

            entity.HasOne(d => d.Staff).WithMany(p => p.Advances)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__advances__staff___20C1E124");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83F889AEB26");

            entity.ToTable("departments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Financein>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__financei__3213E83F35618322");

            entity.ToTable("financeins");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Document)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("document");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((0))")
                .HasColumnName("status");
            entity.Property(e => e.TypefinanceinId).HasColumnName("typefinancein_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Typefinancein).WithMany(p => p.Financeins)
                .HasForeignKey(d => d.TypefinanceinId)
                .HasConstraintName("FK__financein__typef__300424B4");

            entity.HasOne(d => d.User).WithMany(p => p.Financeins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__financein__user___2F10007B");
        });

        modelBuilder.Entity<Financeout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__financeo__3213E83F79473D98");

            entity.ToTable("financeouts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Document)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("document");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((0))")
                .HasColumnName("status");
            entity.Property(e => e.TypefinanceoutId).HasColumnName("typefinanceout_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Typefinanceout).WithMany(p => p.Financeouts)
                .HasForeignKey(d => d.TypefinanceoutId)
                .HasConstraintName("FK__financeou__typef__398D8EEE");

            entity.HasOne(d => d.User).WithMany(p => p.Financeouts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__financeou__user___38996AB5");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__position__3213E83F0DA596EB");

            entity.ToTable("positions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__staffs__3213E83FF8BDBAB0");

            entity.ToTable("staffs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.JoinDate)
                .HasColumnType("date")
                .HasColumnName("join_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telephone");

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staffs__departme__1CF15040");

            entity.HasOne(d => d.Position).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staffs__position__1BFD2C07");
        });

        modelBuilder.Entity<Typefinancein>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__typefina__3213E83F8EBB0D99");

            entity.ToTable("typefinanceins");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Typefinanceout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__typefina__3213E83F26A60F65");

            entity.ToTable("typefinanceouts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FCD4AB7BE");

            entity.ToTable("users");

            entity.HasIndex(e => e.Telephone, "UQ__users__61AE339BC13D0AD3").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__users__AB6E61641C58C621").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Permission)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("permission");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((0))")
                .HasColumnName("status");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telephone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
