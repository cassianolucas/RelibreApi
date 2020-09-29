using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RelibreApi.Migrations
{
    public partial class initiaç : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    code_integration = table.Column<string>(type: "varchar(255)", nullable: true),
                    isbn13 = table.Column<string>(type: "varchar(13)", nullable: true),
                    title = table.Column<string>(type: "varchar(144)", nullable: false),
                    maturity_rating = table.Column<string>(type: "varchar(144)", nullable: true),
                    average_rating = table.Column<string>(type: "varchar(144)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contact",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "email_verification",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    login = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_verification = table.Column<string>(type: "varchar(36)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_verification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "varchar", maxLength: 255, nullable: true),
                    document = table.Column<string>(type: "varchar", maxLength: 18, nullable: true),
                    type_person = table.Column<string>(maxLength: 2, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profile",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    name = table.Column<string>(type: "varchar(144)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(type: "varchar(255)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "author_book",
                columns: table => new
                {
                    IdBook = table.Column<long>(nullable: false),
                    IdAuthor = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_book", x => new { x.IdBook, x.IdAuthor });
                    table.ForeignKey(
                        name: "fk_author_book_author_id_author",
                        column: x => x.IdAuthor,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_author_book_book_id_book",
                        column: x => x.IdBook,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "category_book",
                columns: table => new
                {
                    IdBook = table.Column<long>(nullable: false),
                    IdCategory = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_book", x => new { x.IdBook, x.IdCategory });
                    table.ForeignKey(
                        name: "fk_category_book_book_id_book",
                        column: x => x.IdBook,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_category_book_category_id_category",
                        column: x => x.IdCategory,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_person = table.Column<long>(type: "bigint", nullable: false),
                    nick_name = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    latitude = table.Column<string>(type: "varchar(25)", nullable: false),
                    longitude = table.Column<string>(type: "varchar(25)", nullable: false),
                    master = table.Column<bool>(type: "boolean", nullable: false),
                    PersonId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "library",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_person = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_library", x => x.id);
                    table.ForeignKey(
                        name: "fk_library_person_id_person",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationPerson",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IdNotification = table.Column<long>(nullable: false),
                    IdPerson = table.Column<long>(nullable: false),
                    NotificationId = table.Column<long>(nullable: true),
                    PersonId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationPerson_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationPerson_person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "phone",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_person = table.Column<long>(type: "bigint", nullable: false),
                    number = table.Column<string>(type: "varchar(50)", nullable: false),
                    master = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phone", x => x.id);
                    table.ForeignKey(
                        name: "fk_phone_person_id_person",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "access_profile",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    access = table.Column<string>(type: "varchar(144)", nullable: false),
                    id_profile = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_profile", x => x.id);
                    table.ForeignKey(
                        name: "fk_profile_access_profile_id_profile",
                        column: x => x.id_profile,
                        principalTable: "profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    login = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    id_profile = table.Column<long>(type: "bigint", nullable: false),
                    id_person = table.Column<long>(type: "bigint", nullable: false),
                    login_verified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.login);
                    table.ForeignKey(
                        name: "fk_user_person_id_person",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_profile_id_profile",
                        column: x => x.id_profile,
                        principalTable: "profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "library_book",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    id_library = table.Column<long>(type: "bigint", nullable: false),
                    id_contact = table.Column<long>(type: "bigint", nullable: false),
                    id_book = table.Column<long>(type: "bigint", nullable: false),
                    reating = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_library_book", x => x.id);
                    table.ForeignKey(
                        name: "fk_library_book_book_id_book",
                        column: x => x.id_book,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_library_book_contact_id_contact",
                        column: x => x.id_contact,
                        principalTable: "contact",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_library_book_library_id_library",
                        column: x => x.id_library,
                        principalTable: "library",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    thumbnail = table.Column<string>(type: "varchar(255)", nullable: true),
                    small = table.Column<string>(type: "varchar(255)", nullable: true),
                    medium = table.Column<string>(type: "varchar(255)", nullable: true),
                    large = table.Column<string>(type: "varchar(255)", nullable: true),
                    small_thumbnail = table.Column<string>(type: "varchar(255)", nullable: true),
                    extra_large = table.Column<string>(type: "varchar(255)", nullable: true),
                    id_library_book = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_library_book_image_id_library_book",
                        column: x => x.id_library_book,
                        principalTable: "library_book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "library_book_type",
                columns: table => new
                {
                    id_library_book = table.Column<long>(type: "bigint", nullable: false),
                    id_type = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_library_book_type", x => new { x.id_library_book, x.id_type });
                    table.ForeignKey(
                        name: "fk_library_book_library_book_id_library_book",
                        column: x => x.id_library_book,
                        principalTable: "library_book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_library_book_library_book_id_type",
                        column: x => x.id_type,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "profile",
                columns: new[] { "id", "active", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1L, true, new DateTime(2020, 9, 24, 21, 33, 15, 0, DateTimeKind.Unspecified), "PJ", new DateTime(2020, 9, 24, 21, 33, 15, 0, DateTimeKind.Unspecified) },
                    { 2L, true, new DateTime(2020, 9, 24, 21, 33, 15, 0, DateTimeKind.Unspecified), "PF", new DateTime(2020, 9, 24, 21, 33, 15, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "type",
                columns: new[] { "id", "created_at", "description", "updated_at" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 9, 24, 21, 33, 15, 0, DateTimeKind.Unspecified), "Troca", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, new DateTime(2020, 9, 24, 21, 33, 15, 0, DateTimeKind.Unspecified), "Doação", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, new DateTime(2020, 9, 24, 21, 33, 15, 0, DateTimeKind.Unspecified), "Emprestimo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, new DateTime(2020, 9, 24, 21, 33, 15, 0, DateTimeKind.Unspecified), "Interesse", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "access_profile",
                columns: new[] { "id", "access", "id_profile" },
                values: new object[,]
                {
                    { 1L, "access.administrator", 1L },
                    { 2L, "access.default", 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_access_profile_id_profile",
                table: "access_profile",
                column: "id_profile");

            migrationBuilder.CreateIndex(
                name: "IX_address_PersonId",
                table: "address",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_author_book_IdAuthor",
                table: "author_book",
                column: "IdAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_category_book_IdCategory",
                table: "category_book",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_image_id_library_book",
                table: "image",
                column: "id_library_book");

            migrationBuilder.CreateIndex(
                name: "IX_library_id_person",
                table: "library",
                column: "id_person",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_library_book_id_book",
                table: "library_book",
                column: "id_book");

            migrationBuilder.CreateIndex(
                name: "IX_library_book_id_contact",
                table: "library_book",
                column: "id_contact");

            migrationBuilder.CreateIndex(
                name: "IX_library_book_id_library",
                table: "library_book",
                column: "id_library");

            migrationBuilder.CreateIndex(
                name: "IX_library_book_type_id_type",
                table: "library_book_type",
                column: "id_type");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPerson_NotificationId",
                table: "NotificationPerson",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPerson_PersonId",
                table: "NotificationPerson",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_phone_id_person",
                table: "phone",
                column: "id_person");

            migrationBuilder.CreateIndex(
                name: "IX_user_id_person",
                table: "user",
                column: "id_person");

            migrationBuilder.CreateIndex(
                name: "IX_user_id_profile",
                table: "user",
                column: "id_profile");

            migrationBuilder.CreateIndex(
                name: "IX_user_login",
                table: "user",
                column: "login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_profile");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "author_book");

            migrationBuilder.DropTable(
                name: "category_book");

            migrationBuilder.DropTable(
                name: "email_verification");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "library_book_type");

            migrationBuilder.DropTable(
                name: "NotificationPerson");

            migrationBuilder.DropTable(
                name: "phone");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "library_book");

            migrationBuilder.DropTable(
                name: "type");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "profile");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "contact");

            migrationBuilder.DropTable(
                name: "library");

            migrationBuilder.DropTable(
                name: "person");
        }
    }
}
