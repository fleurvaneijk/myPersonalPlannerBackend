﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Migrations
{
    [DbContext(typeof(MariaDBContext))]
    [Migration("20200603105247_all")]
    partial class all
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MyPersonalPlannerBackend.Model.Planner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Owner")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Planners");
                });

            modelBuilder.Entity("MyPersonalPlannerBackend.Model.PlannerItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDone")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PlannerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("User")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PlannerId");

                    b.ToTable("PlannerItems");
                });

            modelBuilder.Entity("MyPersonalPlannerBackend.Model.PlannerUser", b =>
                {
                    b.Property<int>("PlannerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PlannerId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PlannerUsers");
                });

            modelBuilder.Entity("MyPersonalPlannerBackend.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AgendaLink")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyPersonalPlannerBackend.Model.PlannerItem", b =>
                {
                    b.HasOne("MyPersonalPlannerBackend.Model.Planner", "Planner")
                        .WithMany("PlannerItems")
                        .HasForeignKey("PlannerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyPersonalPlannerBackend.Model.PlannerUser", b =>
                {
                    b.HasOne("MyPersonalPlannerBackend.Model.Planner", "Planner")
                        .WithMany("PlannerUsers")
                        .HasForeignKey("PlannerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPersonalPlannerBackend.Model.User", "User")
                        .WithMany("PlannerUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
