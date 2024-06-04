﻿// <auto-generated />
using EFDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFDatabase.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20240604190558_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFDatabase.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Date")
                        .HasColumnType("int")
                        .HasColumnName("message_date");

                    b.Property<int>("FromId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit")
                        .HasColumnName("is_sent");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("message_text");

                    b.Property<int>("ToId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("message_pk");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("EFDatabase.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("FullName");

                    b.HasKey("Id")
                        .HasName("user_pk");

                    b.HasIndex("FullName")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("EFDatabase.Message", b =>
                {
                    b.HasOne("EFDatabase.User", "From")
                        .WithMany("MessagesFrom")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("message_from_user_fk");

                    b.HasOne("EFDatabase.User", "To")
                        .WithMany("MessagesTo")
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("message_to_user_fk");

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("EFDatabase.User", b =>
                {
                    b.Navigation("MessagesFrom");

                    b.Navigation("MessagesTo");
                });
#pragma warning restore 612, 618
        }
    }
}
