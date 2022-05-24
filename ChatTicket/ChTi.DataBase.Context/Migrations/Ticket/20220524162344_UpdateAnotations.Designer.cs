﻿// <auto-generated />
using System;
using ChTi.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChTi.DataBase.Context.Migrations.Ticket
{
    [DbContext(typeof(TicketContext))]
    [Migration("20220524162344_UpdateAnotations")]
    partial class UpdateAnotations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChTi.DataBase.Entity.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid");

                    b.Property<string>("FileToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("ChTi.DataBase.Entity.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("FromUserId")
                        .HasColumnType("uuid");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ToUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Ticket");
                });
#pragma warning restore 612, 618
        }
    }
}
