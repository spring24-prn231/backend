﻿// <auto-generated />
using System;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObjects.Migrations
{
    [DbContext(typeof(BirthdayBlitzContext))]
    [Migration("20240228070713_AlterOrder")]
    partial class AlterOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObjects.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Deposit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(20, 1)");

                    b.HasKey("Id")
                        .HasName("PK__Deposit__3214EC07C1384199");

                    b.HasIndex("OrderId");

                    b.ToTable("Deposit", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("DishTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Dish__3214EC0718721564");

                    b.HasIndex("DishTypeId");

                    b.ToTable("Dish", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.DishType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__DishType__3214EC07541A8E5A");

                    b.ToTable("DishType", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.ElementType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__ElementT__3214EC0730B40F77");

                    b.ToTable("ElementType", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte?>("RatingStar")
                        .HasColumnType("tinyint");

                    b.HasKey("Id")
                        .HasName("PK__Feedback__3214EC070567188F");

                    b.HasIndex("OrderId");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<Guid?>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK__Menu__3214EC0733AC5CC0");

                    b.HasIndex("DishId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Menu", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Contract")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SlotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(20, 1)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK__Order__3214EC07D93349DD");

                    b.HasIndex("ServiceId");

                    b.HasIndex("SlotId");

                    b.HasIndex("StaffId");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(20, 1)");

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(20, 1)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(20, 1)");

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__OrderDet__3214EC07BB2F37BC");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.PartyPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TimeEnd")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("TimeStart")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("PK__PartyPla__3214EC0778000FA4");

                    b.HasIndex("OrderId");

                    b.ToTable("PartyPlan", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("RoomNo")
                        .HasColumnType("int");

                    b.Property<Guid>("RoomTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK__Room__3214EC074554CFB0");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Room", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.RoomType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__RoomType__3214EC076B2D2DCB");

                    b.ToTable("RoomType", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("RoomTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserID");

                    b.HasKey("Id")
                        .HasName("PK__Service__3214EC0734FAED6B");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Service", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.ServiceElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ElementTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK__ServiceE__3214EC07FF86A370");

                    b.HasIndex("ElementTypeId");

                    b.ToTable("ServiceElement", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.ServiceElementDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<Guid>("ServiceElementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK__ServiceE__3214EC0775FBEC38");

                    b.HasIndex("ServiceElementId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceElementDetail", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Slot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("FromHour")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ToHour")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Slot__3214EC0768BFFF5B");

                    b.HasIndex("RoomId");

                    b.ToTable("Slot", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Code")
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("varchar(16)");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("MaximumValue")
                        .HasColumnType("decimal(20, 1)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK__Voucher__3214EC07B7349123");

                    b.HasIndex("OrderId");

                    b.ToTable("Voucher", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Models.Deposit", b =>
                {
                    b.HasOne("BusinessObjects.Models.Order", "Order")
                        .WithMany("Deposits")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Deposit__OrderId__7D439ABD");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.Models.Dish", b =>
                {
                    b.HasOne("BusinessObjects.Models.DishType", "DishType")
                        .WithMany("Dishes")
                        .HasForeignKey("DishTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__Dish__DishTypeId__4D94879B");

                    b.Navigation("DishType");
                });

            modelBuilder.Entity("BusinessObjects.Models.Feedback", b =>
                {
                    b.HasOne("BusinessObjects.Models.Order", "Order")
                        .WithMany("Feedbacks")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Feedback__OrderI__01142BA1");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.Models.Menu", b =>
                {
                    b.HasOne("BusinessObjects.Models.Dish", "Dish")
                        .WithMany("Menus")
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK__Menu__DishId__6754599E");

                    b.HasOne("BusinessObjects.Models.Service", "Service")
                        .WithMany("Menus")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("FK__Menu__ServiceId__68487DD7");

                    b.Navigation("Dish");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("BusinessObjects.Models.Order", b =>
                {
                    b.HasOne("BusinessObjects.Models.Service", "Service")
                        .WithMany("Orders")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("FK__Order__ServiceId__6FE99F9F");

                    b.HasOne("BusinessObjects.Models.Slot", "Slot")
                        .WithMany("Orders")
                        .HasForeignKey("SlotId")
                        .HasConstraintName("FK__Order__SlotId__70DDC3D8");

                    b.HasOne("BusinessObjects.Models.ApplicationUser", "Staff")
                        .WithMany("StaffOrders")
                        .HasForeignKey("StaffId");

                    b.HasOne("BusinessObjects.Models.ApplicationUser", "User")
                        .WithMany("UserOrders")
                        .HasForeignKey("UserId");

                    b.Navigation("Service");

                    b.Navigation("Slot");

                    b.Navigation("Staff");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObjects.Models.OrderDetail", b =>
                {
                    b.HasOne("BusinessObjects.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderDeta__Order__797309D9");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.Models.PartyPlan", b =>
                {
                    b.HasOne("BusinessObjects.Models.Order", "Order")
                        .WithMany("PartyPlans")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__PartyPlan__Order__75A278F5");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.Models.Room", b =>
                {
                    b.HasOne("BusinessObjects.Models.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__Room__RoomTypeId__5AEE82B9");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("BusinessObjects.Models.Service", b =>
                {
                    b.HasOne("BusinessObjects.Models.RoomType", "RoomType")
                        .WithMany("Services")
                        .HasForeignKey("RoomTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__Service__RoomTyp__5EBF139D");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("BusinessObjects.Models.ServiceElement", b =>
                {
                    b.HasOne("BusinessObjects.Models.ElementType", "ElementType")
                        .WithMany("ServiceElements")
                        .HasForeignKey("ElementTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__ServiceEl__Eleme__5441852A");

                    b.Navigation("ElementType");
                });

            modelBuilder.Entity("BusinessObjects.Models.ServiceElementDetail", b =>
                {
                    b.HasOne("BusinessObjects.Models.ServiceElement", "ServiceElement")
                        .WithMany("ServiceElementDetails")
                        .HasForeignKey("ServiceElementId")
                        .IsRequired()
                        .HasConstraintName("FK__ServiceEl__Servi__628FA481");

                    b.HasOne("BusinessObjects.Models.Service", "Service")
                        .WithMany("ServiceElementDetails")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("FK__ServiceEl__Servi__6383C8BA");

                    b.Navigation("Service");

                    b.Navigation("ServiceElement");
                });

            modelBuilder.Entity("BusinessObjects.Models.Slot", b =>
                {
                    b.HasOne("BusinessObjects.Models.Room", "Room")
                        .WithMany("Slots")
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK__Slot__RoomId__6C190EBB");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BusinessObjects.Models.Voucher", b =>
                {
                    b.HasOne("BusinessObjects.Models.Order", "Order")
                        .WithMany("Vouchers")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Voucher__OrderId__05D8E0BE");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BusinessObjects.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BusinessObjects.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("BusinessObjects.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Models.ApplicationUser", b =>
                {
                    b.Navigation("StaffOrders");

                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("BusinessObjects.Models.Dish", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("BusinessObjects.Models.DishType", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("BusinessObjects.Models.ElementType", b =>
                {
                    b.Navigation("ServiceElements");
                });

            modelBuilder.Entity("BusinessObjects.Models.Order", b =>
                {
                    b.Navigation("Deposits");

                    b.Navigation("Feedbacks");

                    b.Navigation("OrderDetails");

                    b.Navigation("PartyPlans");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("BusinessObjects.Models.Room", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("BusinessObjects.Models.RoomType", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("BusinessObjects.Models.Service", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Orders");

                    b.Navigation("ServiceElementDetails");
                });

            modelBuilder.Entity("BusinessObjects.Models.ServiceElement", b =>
                {
                    b.Navigation("ServiceElementDetails");
                });

            modelBuilder.Entity("BusinessObjects.Models.Slot", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
