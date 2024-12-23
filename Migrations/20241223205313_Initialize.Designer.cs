﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Trackit.Infra.Data;

#nullable disable

namespace Trackit.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241223205313_Initialize")]
    partial class Initialize
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Trackit.Core.Entities.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Size")
                        .HasColumnType("real");

                    b.Property<Guid?>("TicketId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TicketId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Trackit.Core.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Trackit.Core.Entities.Tech", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Techs");
                });

            modelBuilder.Entity("Trackit.Core.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OpenId")
                        .HasColumnType("uuid");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<bool>("Recurrent")
                        .HasColumnType("boolean");

                    b.Property<string>("SmallId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("OpenId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Trackit.Core.Entities.TicketAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<Guid?>("TicketId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TicketId1")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TicketId2")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TicketId3")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TicketId");

                    b.HasIndex("TicketId1");

                    b.HasIndex("TicketId2");

                    b.HasIndex("TicketId3");

                    b.ToTable("TicketAction");
                });

            modelBuilder.Entity("Trackit.Core.Entities.Attachment", b =>
                {
                    b.HasOne("Trackit.Core.Entities.Tech", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trackit.Core.Entities.Ticket", null)
                        .WithMany("Attachments")
                        .HasForeignKey("TicketId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Trackit.Core.Entities.Ticket", b =>
                {
                    b.HasOne("Trackit.Core.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trackit.Core.Entities.Tech", "Open")
                        .WithMany()
                        .HasForeignKey("OpenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Open");
                });

            modelBuilder.Entity("Trackit.Core.Entities.TicketAction", b =>
                {
                    b.HasOne("Trackit.Core.Entities.Tech", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trackit.Core.Entities.Ticket", null)
                        .WithMany("Feedbacks")
                        .HasForeignKey("TicketId");

                    b.HasOne("Trackit.Core.Entities.Ticket", null)
                        .WithMany("Finish")
                        .HasForeignKey("TicketId1");

                    b.HasOne("Trackit.Core.Entities.Ticket", null)
                        .WithMany("Notes")
                        .HasForeignKey("TicketId2");

                    b.HasOne("Trackit.Core.Entities.Ticket", null)
                        .WithMany("Progress")
                        .HasForeignKey("TicketId3");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Trackit.Core.Entities.Ticket", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Feedbacks");

                    b.Navigation("Finish");

                    b.Navigation("Notes");

                    b.Navigation("Progress");
                });
#pragma warning restore 612, 618
        }
    }
}
