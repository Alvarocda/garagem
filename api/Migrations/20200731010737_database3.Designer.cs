﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api.Data;

namespace api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200731010737_database3")]
    partial class database3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("api.Models.Fabricante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AtualizadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CriadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DesativadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DesativadoPor")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("fabricantes");
                });

            modelBuilder.Entity("api.Models.Imagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AtualizadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CriadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DesativadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DesativadoPor")
                        .HasColumnType("integer");

                    b.Property<string>("URL")
                        .HasColumnType("text");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("imagens");
                });

            modelBuilder.Entity("api.Models.Modelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AtualizadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CriadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DesativadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DesativadoPor")
                        .HasColumnType("integer");

                    b.Property<int>("FabricanteId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("FabricanteId");

                    b.ToTable("modelos");
                });

            modelBuilder.Entity("api.Models.TipoVeiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AtualizadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CriadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DesativadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DesativadoPor")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("tipos_veiculo");
                });

            modelBuilder.Entity("api.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AtualizadoPor")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Chave")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CriadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DesativadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DesativadoPor")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<byte[]>("Senha")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("api.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Ano")
                        .IsRequired()
                        .HasColumnType("character varying(9)")
                        .HasMaxLength(9);

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AtualizadoPor")
                        .HasColumnType("integer");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CriadoPor")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DesativadoEm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DesativadoPor")
                        .HasColumnType("integer");

                    b.Property<int?>("FabricanteId")
                        .HasColumnType("integer");

                    b.Property<int>("KM")
                        .HasColumnType("integer");

                    b.Property<int>("ModeloId")
                        .HasColumnType("integer");

                    b.Property<string>("Observacao")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("character varying(1)")
                        .HasMaxLength(1);

                    b.Property<int>("TipoVeiculoId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("FabricanteId");

                    b.HasIndex("ModeloId");

                    b.HasIndex("TipoVeiculoId");

                    b.ToTable("veiculos");
                });

            modelBuilder.Entity("api.Models.Modelo", b =>
                {
                    b.HasOne("api.Models.Fabricante", "Fabricante")
                        .WithMany("Modelos")
                        .HasForeignKey("FabricanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.Veiculo", b =>
                {
                    b.HasOne("api.Models.Fabricante", "Fabricante")
                        .WithMany()
                        .HasForeignKey("FabricanteId");

                    b.HasOne("api.Models.Modelo", "Modelo")
                        .WithMany()
                        .HasForeignKey("ModeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.TipoVeiculo", "TipoVeiculo")
                        .WithMany()
                        .HasForeignKey("TipoVeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
