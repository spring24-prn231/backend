using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BusinessObjects.Models;

public partial class BirthdayBlitzContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public BirthdayBlitzContext()
    {
    }

    public BirthdayBlitzContext(DbContextOptions<BirthdayBlitzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Deposit> Deposits { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishType> DishTypes { get; set; }

    public virtual DbSet<ElementType> ElementTypes { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PartyPlan> PartyPlans { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceElement> ServiceElements { get; set; }

    public virtual DbSet<ServiceElementDetail> ServiceElementDetails { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets(Assembly.GetExecutingAssembly(),true)
            .Build();
        return config["ConnectionString:BirthdayBlitz"];
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Deposit__3214EC07C1384199");

            entity.ToTable("Deposit");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Value).HasColumnType("decimal(20, 1)");

            entity.HasOne(d => d.Order).WithMany(p => p.Deposits)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Deposit__OrderId__7D439ABD");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dish__3214EC0718721564");

            entity.ToTable("Dish");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.DishType).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.DishTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dish__DishTypeId__4D94879B");
        });

        modelBuilder.Entity<DishType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DishType__3214EC07541A8E5A");

            entity.ToTable("DishType");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ElementType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ElementT__3214EC0730B40F77");

            entity.ToTable("ElementType");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC070567188F");

            entity.ToTable("Feedback");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Feedback__OrderI__01142BA1");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC0733AC5CC0");

            entity.ToTable("Menu");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Dish).WithMany(p => p.Menus)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__Menu__DishId__6754599E");

            entity.HasOne(d => d.Service).WithMany(p => p.Menus)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Menu__ServiceId__68487DD7");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07D93349DD");

            entity.ToTable("Order");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(20, 1)");

            entity.HasOne(d => d.Service).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__ServiceId__6FE99F9F");

            entity.HasOne(d => d.Slot).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__SlotId__70DDC3D8");
            entity.HasOne(d => d.User).WithMany(p => p.UserOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.Staff).WithMany(p => p.StaffOrders)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC07BB2F37BC");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(20, 1)");
            entity.Property(e => e.Cost).HasColumnType("decimal(20, 1)");
            entity.Property(e => e.Price).HasColumnType("decimal(20, 1)");
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__797309D9");
        });

        modelBuilder.Entity<PartyPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PartyPla__3214EC0778000FA4");

            entity.ToTable("PartyPlan");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TimeEnd).HasColumnType("datetime");
            entity.Property(e => e.TimeStart).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.PartyPlans)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__PartyPlan__Order__75A278F5");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room__3214EC074554CFB0");

            entity.ToTable("Room");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__RoomTypeId__5AEE82B9");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoomType__3214EC076B2D2DCB");

            entity.ToTable("RoomType");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC0734FAED6B");

            entity.ToTable("Service");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Services)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Service__RoomTyp__5EBF139D");
        });

        modelBuilder.Entity<ServiceElement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceE__3214EC07FF86A370");

            entity.ToTable("ServiceElement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ElementType).WithMany(p => p.ServiceElements)
                .HasForeignKey(d => d.ElementTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceEl__Eleme__5441852A");
        });

        modelBuilder.Entity<ServiceElementDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceE__3214EC0775FBEC38");

            entity.ToTable("ServiceElementDetail");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ServiceElement).WithMany(p => p.ServiceElementDetails)
                .HasForeignKey(d => d.ServiceElementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceEl__Servi__628FA481");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceElementDetails)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceEl__Servi__6383C8BA");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Slot__3214EC0768BFFF5B");

            entity.ToTable("Slot");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FromHour)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ToHour)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Room).WithMany(p => p.Slots)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Slot__RoomId__6C190EBB");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Voucher__3214EC07B7349123");

            entity.ToTable("Voucher");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.MaximumValue).HasColumnType("decimal(20, 1)");

            entity.HasOne(d => d.Order).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Voucher__OrderId__05D8E0BE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
