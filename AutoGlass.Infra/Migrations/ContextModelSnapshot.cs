﻿// <auto-generated />
using System;
using AutoGlass.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AutoGlass.Infra.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("AutoGlass.Domain.Entities.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CNPJ")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("AutoGlass.Domain.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DataFabricacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4");

                    b.Property<Guid?>("IdFornecedor")
                        .HasColumnType("char(36)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdFornecedor");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("AutoGlass.Domain.Entities.Produto", b =>
                {
                    b.HasOne("AutoGlass.Domain.Entities.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("IdFornecedor");

                    b.Navigation("Fornecedor");
                });
#pragma warning restore 612, 618
        }
    }
}