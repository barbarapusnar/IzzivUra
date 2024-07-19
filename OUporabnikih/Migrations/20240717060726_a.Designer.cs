﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace OUporabnikih.Migrations
{
    [DbContext(typeof(PodatkiDb))]
    [Migration("20240717060726_a")]
    partial class a
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Uporabnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashiranoGeslo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("JeAktiven")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Ime")
                        .IsUnique();

                    b.ToTable("Todos");
                });
#pragma warning restore 612, 618
        }
    }
}
