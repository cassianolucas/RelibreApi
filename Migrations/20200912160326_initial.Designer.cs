﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RelibreApi.Data;

namespace RelibreApi.Migrations
{
    [DbContext(typeof(RelibreContext))]
    [Migration("20200912160326_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("RelibreApi.Models.AccessProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Access")
                        .IsRequired()
                        .HasColumnName("access")
                        .HasColumnType("varchar(144)");

                    b.Property<long>("IdProfile")
                        .HasColumnName("id_profile")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IdProfile");

                    b.ToTable("access_profile");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Access = "access.administrator",
                            IdProfile = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Access = "access.default",
                            IdProfile = 2L
                        });
                });

            modelBuilder.Entity("RelibreApi.Models.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<long>("IdPerson")
                        .HasColumnName("id_person")
                        .HasColumnType("bigint");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnName("latitude")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnName("longitude")
                        .HasColumnType("varchar(25)");

                    b.Property<bool>("Master")
                        .HasColumnName("master")
                        .HasColumnType("boolean");

                    b.Property<string>("NickName")
                        .HasColumnName("nick_name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<long?>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("address");
                });

            modelBuilder.Entity("RelibreApi.Models.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("author");
                });

            modelBuilder.Entity("RelibreApi.Models.AuthorBook", b =>
                {
                    b.Property<long>("IdBook")
                        .HasColumnType("bigint");

                    b.Property<long>("IdAuthor")
                        .HasColumnType("bigint");

                    b.HasKey("IdBook", "IdAuthor");

                    b.HasIndex("IdAuthor");

                    b.ToTable("author_book");
                });

            modelBuilder.Entity("RelibreApi.Models.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AverageRating")
                        .HasColumnName("average_rating")
                        .HasColumnType("varchar(144)");

                    b.Property<string>("CodeIntegration")
                        .HasColumnName("code_integration")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Isbn13")
                        .HasColumnName("isbn13")
                        .HasColumnType("varchar(13)");

                    b.Property<string>("MaturityRating")
                        .HasColumnName("maturity_rating")
                        .HasColumnType("varchar(144)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(144)");

                    b.HasKey("Id");

                    b.ToTable("book");
                });

            modelBuilder.Entity("RelibreApi.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("RelibreApi.Models.CategoryBook", b =>
                {
                    b.Property<long>("IdBook")
                        .HasColumnType("bigint");

                    b.Property<long>("IdCategory")
                        .HasColumnType("bigint");

                    b.HasKey("IdBook", "IdCategory");

                    b.HasIndex("IdCategory");

                    b.ToTable("category_book");
                });

            modelBuilder.Entity("RelibreApi.Models.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ExtraLarge")
                        .HasColumnName("extra_large")
                        .HasColumnType("varchar(255)");

                    b.Property<long>("IdLibraryBook")
                        .HasColumnName("id_library_book")
                        .HasColumnType("bigint");

                    b.Property<string>("Large")
                        .HasColumnName("large")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Medium")
                        .HasColumnName("medium")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Small")
                        .HasColumnName("small")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SmallThumbnail")
                        .HasColumnName("small_thumbnail")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Thumbnail")
                        .HasColumnName("thumbnail")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("IdLibraryBook");

                    b.ToTable("image");
                });

            modelBuilder.Entity("RelibreApi.Models.Library", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<long>("IdPerson")
                        .HasColumnName("id_person")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("IdPerson")
                        .IsUnique();

                    b.ToTable("library");
                });

            modelBuilder.Entity("RelibreApi.Models.LibraryBook", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<long>("IdBook")
                        .HasColumnName("id_book")
                        .HasColumnType("bigint");

                    b.Property<long>("IdContact")
                        .HasColumnName("id_contact")
                        .HasColumnType("bigint");

                    b.Property<long>("IdLibrary")
                        .HasColumnName("id_library")
                        .HasColumnType("bigint");

                    b.Property<long>("IdType")
                        .HasColumnName("id_type")
                        .HasColumnType("bigint");

                    b.Property<string>("Reating")
                        .HasColumnName("reating")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("IdBook");

                    b.HasIndex("IdLibrary");

                    b.HasIndex("IdType");

                    b.ToTable("library_book");
                });

            modelBuilder.Entity("RelibreApi.Models.Notification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("RelibreApi.Models.NotificationPerson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("IdNotification")
                        .HasColumnType("bigint");

                    b.Property<long>("IdPerson")
                        .HasColumnType("bigint");

                    b.Property<long?>("NotificationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.HasIndex("PersonId");

                    b.ToTable("NotificationPerson");
                });

            modelBuilder.Entity("RelibreApi.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<string>("Document")
                        .HasColumnName("document")
                        .HasColumnType("varchar")
                        .HasMaxLength(18);

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("varchar")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar")
                        .HasMaxLength(255);

                    b.Property<string>("PersonType")
                        .IsRequired()
                        .HasColumnName("type_person")
                        .HasColumnType("character varying(2)")
                        .HasMaxLength(2);

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("person");
                });

            modelBuilder.Entity("RelibreApi.Models.Phone", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<long>("IdPerson")
                        .HasColumnName("id_person")
                        .HasColumnType("bigint");

                    b.Property<bool>("Master")
                        .HasColumnName("master")
                        .HasColumnType("boolean");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnName("number")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("IdPerson");

                    b.ToTable("phone");
                });

            modelBuilder.Entity("RelibreApi.Models.Profile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(144)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("profile");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            CreatedAt = new DateTime(2020, 9, 12, 13, 3, 25, 0, DateTimeKind.Unspecified),
                            Name = "PJ",
                            UpdatedAt = new DateTime(2020, 9, 12, 13, 3, 25, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            Active = true,
                            CreatedAt = new DateTime(2020, 9, 12, 13, 3, 25, 0, DateTimeKind.Unspecified),
                            Name = "PF",
                            UpdatedAt = new DateTime(2020, 9, 12, 13, 3, 25, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("RelibreApi.Models.Type", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:IdentitySequenceOptions", "'1', '1', '1', '', 'False', '1'")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("type");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2020, 9, 12, 13, 3, 25, 0, DateTimeKind.Unspecified),
                            Description = "Troca",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2020, 9, 12, 13, 3, 25, 0, DateTimeKind.Unspecified),
                            Description = "Doação",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2020, 9, 12, 13, 3, 25, 0, DateTimeKind.Unspecified),
                            Description = "Emprestimo",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("RelibreApi.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnName("login")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<long>("IdPerson")
                        .HasColumnName("id_person")
                        .HasColumnType("bigint");

                    b.Property<long>("IdProfile")
                        .HasColumnName("id_profile")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Login");

                    b.HasIndex("IdPerson");

                    b.HasIndex("IdProfile");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("user");
                });

            modelBuilder.Entity("RelibreApi.Models.AccessProfile", b =>
                {
                    b.HasOne("RelibreApi.Models.Profile", "Profile")
                        .WithMany("AccessProfiles")
                        .HasForeignKey("IdProfile")
                        .HasConstraintName("fk_profile_access_profile_id_profile")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RelibreApi.Models.Address", b =>
                {
                    b.HasOne("RelibreApi.Models.Person", "Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("RelibreApi.Models.AuthorBook", b =>
                {
                    b.HasOne("RelibreApi.Models.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("IdAuthor")
                        .HasConstraintName("fk_author_book_author_id_author")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RelibreApi.Models.Book", "Book")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("IdBook")
                        .HasConstraintName("fk_author_book_book_id_book")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RelibreApi.Models.CategoryBook", b =>
                {
                    b.HasOne("RelibreApi.Models.Book", "Book")
                        .WithMany("CategoryBooks")
                        .HasForeignKey("IdBook")
                        .HasConstraintName("fk_category_book_book_id_book")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RelibreApi.Models.Category", "Category")
                        .WithMany("CategoryBooks")
                        .HasForeignKey("IdCategory")
                        .HasConstraintName("fk_category_book_category_id_category")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RelibreApi.Models.Image", b =>
                {
                    b.HasOne("RelibreApi.Models.LibraryBook", "LibraryBook")
                        .WithMany("Images")
                        .HasForeignKey("IdLibraryBook")
                        .HasConstraintName("fk_library_book_image_id_library_book")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RelibreApi.Models.Library", b =>
                {
                    b.HasOne("RelibreApi.Models.Person", "Person")
                        .WithOne("Library")
                        .HasForeignKey("RelibreApi.Models.Library", "IdPerson")
                        .HasConstraintName("fk_library_person_id_person")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RelibreApi.Models.LibraryBook", b =>
                {
                    b.HasOne("RelibreApi.Models.Book", "Book")
                        .WithMany("LibraryBooks")
                        .HasForeignKey("IdBook")
                        .HasConstraintName("fk_library_book_book_id_book")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RelibreApi.Models.Library", "Library")
                        .WithMany("LibraryBooks")
                        .HasForeignKey("IdLibrary")
                        .HasConstraintName("fk_library_book_library_id_library")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RelibreApi.Models.Type", "Type")
                        .WithMany("LibraryBooks")
                        .HasForeignKey("IdType")
                        .HasConstraintName("fk_library_book_type_id_type")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RelibreApi.Models.NotificationPerson", b =>
                {
                    b.HasOne("RelibreApi.Models.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId");

                    b.HasOne("RelibreApi.Models.Person", "Person")
                        .WithMany("NotificationPeople")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("RelibreApi.Models.Phone", b =>
                {
                    b.HasOne("RelibreApi.Models.Person", "Person")
                        .WithMany("Phones")
                        .HasForeignKey("IdPerson")
                        .HasConstraintName("fk_phone_person_id_person")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RelibreApi.Models.User", b =>
                {
                    b.HasOne("RelibreApi.Models.Person", "Person")
                        .WithMany("Users")
                        .HasForeignKey("IdPerson")
                        .HasConstraintName("fk_user_person_id_person")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RelibreApi.Models.Profile", "Profile")
                        .WithMany("Users")
                        .HasForeignKey("IdProfile")
                        .HasConstraintName("fk_user_profile_id_profile")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
