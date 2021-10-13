﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DisneyAPI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210930010127_Tables")]
    partial class Tables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Models.Genero", b =>
                {
                    b.Property<int>("IdGenero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("IdGenero");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("Models.Pelicula", b =>
                {
                    b.Property<int>("IdPelicula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Calificacion")
                        .HasColumnType("integer");

                    b.Property<int>("IdGenero")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .HasColumnType("text");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("IdPelicula");

                    b.HasIndex("IdGenero");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("Models.Personaje", b =>
                {
                    b.Property<int>("IdPersonaje")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Edad")
                        .HasColumnType("integer");

                    b.Property<string>("Historia")
                        .HasColumnType("text");

                    b.Property<string>("Imagen")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<float>("Peso")
                        .HasColumnType("real");

                    b.Property<int>("idPelicula")
                        .HasColumnType("integer");

                    b.HasKey("IdPersonaje");

                    b.HasIndex("idPelicula");

                    b.ToTable("Personajes");
                });

            modelBuilder.Entity("Models.Rol", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("IdRol");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("idRol")
                        .HasColumnType("integer");

                    b.HasKey("IdUsuario");

                    b.HasIndex("idRol");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Models.Pelicula", b =>
                {
                    b.HasOne("Models.Genero", "genero")
                        .WithMany()
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("genero");
                });

            modelBuilder.Entity("Models.Personaje", b =>
                {
                    b.HasOne("Models.Pelicula", "pelicula")
                        .WithMany()
                        .HasForeignKey("idPelicula")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("pelicula");
                });

            modelBuilder.Entity("Models.Usuario", b =>
                {
                    b.HasOne("Models.Rol", "rol")
                        .WithMany()
                        .HasForeignKey("idRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("rol");
                });
#pragma warning restore 612, 618
        }
    }
}
