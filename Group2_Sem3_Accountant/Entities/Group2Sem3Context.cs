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

    public virtual DbSet<Fund> Funds { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Totalsalarysheet> Totalsalarysheets { get; set; }

    public virtual DbSet<Typefinancein> Typefinanceins { get; set; }

    public virtual DbSet<Typefinanceout> Typefinanceouts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\MSSQLSERVER01;Initial Catalog=Group2_sem3;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Advance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__advances__3213E83FD7519406");

            entity.ToTable("advances");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
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
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserCreateId).HasColumnName("user_create_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Staff).WithMany(p => p.Advances)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__advances__staff___5165187F");

            entity.HasOne(d => d.UserCreate).WithMany(p => p.AdvanceUserCreates)
                .HasForeignKey(d => d.UserCreateId)
                .HasConstraintName("FK__advances__user_c__70DDC3D8");

            entity.HasOne(d => d.User).WithMany(p => p.AdvanceUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__advances__user_i__6477ECF3");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83FF76D7B32");

            entity.ToTable("departments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Allowance)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("allowance");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Financein>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__financei__3213E83FF6E7FA22");

            entity.ToTable("financeins");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
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
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserCreateId).HasColumnName("user_create_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Typefinancein).WithMany(p => p.Financeins)
                .HasForeignKey(d => d.TypefinanceinId)
                .HasConstraintName("FK__financein__typef__534D60F1");

            entity.HasOne(d => d.UserCreate).WithMany(p => p.FinanceinUserCreates)
                .HasForeignKey(d => d.UserCreateId)
                .HasConstraintName("FK__financein__user___6E01572D");

            entity.HasOne(d => d.User).WithMany(p => p.FinanceinUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__financein__user___52593CB8");
        });

        modelBuilder.Entity<Financeout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__financeo__3213E83F4E6DAA2B");

            entity.ToTable("financeouts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
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
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserCreateId).HasColumnName("user_create_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Typefinanceout).WithMany(p => p.Financeouts)
                .HasForeignKey(d => d.TypefinanceoutId)
                .HasConstraintName("FK__financeou__typef__5535A963");

            entity.HasOne(d => d.UserCreate).WithMany(p => p.FinanceoutUserCreates)
                .HasForeignKey(d => d.UserCreateId)
                .HasConstraintName("FK__financeou__user___6EF57B66");

            entity.HasOne(d => d.User).WithMany(p => p.FinanceoutUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__financeou__user___5441852A");
        });

        modelBuilder.Entity<Fund>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fund__3213E83FA82BF73A");

            entity.ToTable("fund");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("amount");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payrolls__3213E83FC32263F8");

            entity.ToTable("payrolls");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualWorkday).HasColumnName("actual_workday");
            entity.Property(e => e.ActualWorkingDaySalary)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("actual_working_day_salary");
            entity.Property(e => e.Allowance)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("allowance");
            entity.Property(e => e.BaseSalary)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("base_salary");
            entity.Property(e => e.Bonus)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("bonus");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Insurance)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("insurance");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PaidLeaver)
                .HasDefaultValueSql("((0))")
                .HasColumnName("paid_leaver");
            entity.Property(e => e.Reduce)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("reduce");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalSalary)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("total_salary");
            entity.Property(e => e.UnionDues)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("union_dues");
            entity.Property(e => e.UnpaidLeaver)
                .HasDefaultValueSql("((0))")
                .HasColumnName("unpaid_leaver");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserCreateId).HasColumnName("user_create_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Workday).HasColumnName("workday");

            entity.HasOne(d => d.Staff).WithMany(p => p.Payrolls)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__payrolls__staff___693CA210");

            entity.HasOne(d => d.UserCreate).WithMany(p => p.PayrollUserCreates)
                .HasForeignKey(d => d.UserCreateId)
                .HasConstraintName("FK__payrolls__user_c__6FE99F9F");

            entity.HasOne(d => d.User).WithMany(p => p.PayrollUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__payrolls__user_i__6A30C649");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__position__3213E83F548AECFD");

            entity.ToTable("positions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Coefficient).HasColumnName("coefficient");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__staffs__3213E83F6B289543");

            entity.ToTable("staffs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Basesalary)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("basesalary");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
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
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staffs__departme__571DF1D5");

            entity.HasOne(d => d.Position).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staffs__position__5629CD9C");
        });

        modelBuilder.Entity<Totalsalarysheet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__totalsal__3213E83F1B5CA2BF");

            entity.ToTable("totalsalarysheets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((2))")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Typefinancein>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__typefina__3213E83F8E695F6E");

            entity.ToTable("typefinanceins");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Typefinanceout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__typefina__3213E83F4A03DD6B");

            entity.ToTable("typefinanceouts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FE0C06969");

            entity.ToTable("users");

            entity.HasIndex(e => e.Telephone, "UQ__users__61AE339B9837E24F").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616457445CA6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
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
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
