﻿// <auto-generated />
using System;
using ChTi.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChTi.DataBase.Context.Migrations
{
    [DbContext(typeof(ChatContext))]
    partial class ChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ChTi.DataBase.Entity.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("ChTi.DataBase.Entity.ChatsUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ChatsUsers");
                });

            modelBuilder.Entity("ChTi.DataBase.Entity.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("MessageId")
                        .HasColumnType("bigint");

                    b.Property<long>("ReplyMessageId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("ChTi.DataBase.Entity.PvChat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OppsiteUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StarterUserId")
                        .HasColumnType("uuid");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("PvChat");
                });
#pragma warning restore 612, 618
        }
    }
}
