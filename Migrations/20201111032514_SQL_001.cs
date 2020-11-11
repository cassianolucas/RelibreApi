using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RelibreApi.Migrations
{
    public partial class SQL_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    login = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_verification = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_verification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "varchar", maxLength: 255, nullable: true),
                    document = table.Column<string>(type: "varchar", maxLength: 18, nullable: true),
                    type_person = table.Column<string>(maxLength: 2, nullable: false),
                    web_site = table.Column<string>(type: "varchar(255)", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", nullable: true),
                    url_image = table.Column<string>(type: "varchar(255)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "timestamp", nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    name = table.Column<string>(type: "varchar(144)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", nullable: false),
                    value = table.Column<decimal>(type: "numeric(12,4)", nullable: false),
                    period = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "author_book",
                columns: table => new
                {
                    id_book = table.Column<long>(type: "bigint", nullable: false),
                    id_author = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_book", x => new { x.id_book, x.id_author });
                    table.ForeignKey(
                        name: "fk_author_book_author_id_author",
                        column: x => x.id_author,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_author_book_book_id_book",
                        column: x => x.id_book,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "category_book",
                columns: table => new
                {
                    id_book = table.Column<long>(type: "bigint", nullable: false),
                    id_category = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_book", x => new { x.id_book, x.id_category });
                    table.ForeignKey(
                        name: "fk_category_book_book_id_book",
                        column: x => x.id_book,
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_category_book_category_id_category",
                        column: x => x.id_category,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_person = table.Column<long>(type: "bigint", nullable: false),
                    nick_name = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    latitude = table.Column<string>(type: "varchar(25)", nullable: false),
                    longitude = table.Column<string>(type: "varchar(25)", nullable: false),
                    full_address = table.Column<string>(type: "varchar", nullable: false),
                    master = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_adress_person_id_person",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "library",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
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
                name: "notification_person",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_notification = table.Column<long>(type: "bigint", nullable: false),
                    id_person = table.Column<long>(type: "bigint", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_person", x => x.id);
                    table.ForeignKey(
                        name: "fk_notification_person_notification_id_notification",
                        column: x => x.id_notification,
                        principalTable: "notification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_notification_person_person_id_person",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "phone",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    login_verified = table.Column<bool>(type: "boolean", nullable: false),
                    total_count = table.Column<int>(type: "integer", nullable: false),
                    TotalValue = table.Column<int>(type: "integer", nullable: false)
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
                name: "person_subscription",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_person = table.Column<long>(type: "bigint", nullable: false),
                    IdSubscription = table.Column<long>(type: "bigint", nullable: false),
                    paid_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    validate = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_subscription", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_subscription_id_person",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_subscription_subscription_id_subscription",
                        column: x => x.IdSubscription,
                        principalTable: "subscription",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "library_book",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_library = table.Column<long>(type: "bigint", nullable: false),
                    id_book = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "numeric(12,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_library_book", x => x.Id);
                    table.ForeignKey(
                        name: "fk_library_book_book_id_book",
                        column: x => x.id_book,
                        principalTable: "book",
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
                name: "contact_book",
                columns: table => new
                {
                    id_contact_owner = table.Column<long>(type: "bigint", nullable: false),
                    id_contact_request = table.Column<long>(type: "bigint", nullable: false),
                    id_library_book = table.Column<long>(type: "bigint", nullable: false),
                    approved = table.Column<bool>(type: "boolean", nullable: false),
                    denied = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_book", x => new { x.id_contact_owner, x.id_contact_request, x.id_library_book });
                    table.ForeignKey(
                        name: "fk_contact_book_owner_contact_id_contact",
                        column: x => x.id_contact_owner,
                        principalTable: "contact",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contact_book_request_contact_id_contact",
                        column: x => x.id_contact_request,
                        principalTable: "contact",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contact_book_library_book_id_library_book",
                        column: x => x.id_library_book,
                        principalTable: "library_book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image_link = table.Column<string>(type: "varchar(255)", nullable: false),
                    id_library_book = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_library_book_image_id_library_book",
                        column: x => x.id_library_book,
                        principalTable: "library_book",
                        principalColumn: "Id",
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
                        principalColumn: "Id",
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
                    { 1L, true, new DateTime(2020, 11, 11, 0, 25, 14, 699, DateTimeKind.Local).AddTicks(9842), "PJ", new DateTime(2020, 11, 11, 0, 25, 14, 700, DateTimeKind.Local).AddTicks(5842) },
                    { 2L, true, new DateTime(2020, 11, 11, 0, 25, 14, 700, DateTimeKind.Local).AddTicks(6175), "PF", new DateTime(2020, 11, 11, 0, 25, 14, 700, DateTimeKind.Local).AddTicks(6185) }
                });

            migrationBuilder.InsertData(
                table: "type",
                columns: new[] { "id", "Active", "created_at", "description", "updated_at" },
                values: new object[,]
                {
                    { 1L, false, new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4785), "Trocar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, false, new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4822), "Doar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, false, new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4824), "Emprestar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, false, new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4825), "Interesse", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, false, new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4826), "Venda", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                name: "IX_address_id_person",
                table: "address",
                column: "id_person");

            migrationBuilder.CreateIndex(
                name: "IX_author_book_id_author",
                table: "author_book",
                column: "id_author");

            migrationBuilder.CreateIndex(
                name: "IX_category_book_id_category",
                table: "category_book",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_contact_email",
                table: "contact",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contact_book_id_contact_request",
                table: "contact_book",
                column: "id_contact_request");

            migrationBuilder.CreateIndex(
                name: "IX_contact_book_id_library_book",
                table: "contact_book",
                column: "id_library_book");

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
                name: "IX_library_book_id_library",
                table: "library_book",
                column: "id_library");

            migrationBuilder.CreateIndex(
                name: "IX_library_book_type_id_type",
                table: "library_book_type",
                column: "id_type");

            migrationBuilder.CreateIndex(
                name: "IX_notification_person_id_notification",
                table: "notification_person",
                column: "id_notification");

            migrationBuilder.CreateIndex(
                name: "IX_notification_person_id_person",
                table: "notification_person",
                column: "id_person");

            migrationBuilder.CreateIndex(
                name: "IX_person_subscription_id_person",
                table: "person_subscription",
                column: "id_person");

            migrationBuilder.CreateIndex(
                name: "IX_person_subscription_IdSubscription",
                table: "person_subscription",
                column: "IdSubscription");

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
                name: "contact_book");

            migrationBuilder.DropTable(
                name: "email_verification");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "library_book_type");

            migrationBuilder.DropTable(
                name: "notification_person");

            migrationBuilder.DropTable(
                name: "person_subscription");

            migrationBuilder.DropTable(
                name: "phone");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "contact");

            migrationBuilder.DropTable(
                name: "library_book");

            migrationBuilder.DropTable(
                name: "type");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "profile");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "library");

            migrationBuilder.DropTable(
                name: "person");
        }
    }
}
