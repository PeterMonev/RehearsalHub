using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RehearsalHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Genre = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bands_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    LinkUrl = table.Column<string>(type: "text", nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BandMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AvatarUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    BandId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    InvitationToken = table.Column<Guid>(type: "uuid", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Instrument = table.Column<int>(type: "integer", nullable: false),
                    IsDeletedInvitation = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BandMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BandMembers_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    BandId = table.Column<int>(type: "integer", nullable: false),
                    RehearsalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setlists_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Artist = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Duration = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Genre = table.Column<int>(type: "integer", nullable: false),
                    MusicalKey = table.Column<int>(type: "integer", nullable: false),
                    Tempo = table.Column<int>(type: "integer", nullable: true),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false),
                    OwnerBandId = table.Column<int>(type: "integer", nullable: true),
                    CreatorId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Bands_OwnerBandId",
                        column: x => x.OwnerBandId,
                        principalTable: "Bands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rehearsals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    StartRehearsal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndRehearsal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    BandId = table.Column<int>(type: "integer", nullable: false),
                    SetlistId = table.Column<int>(type: "integer", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rehearsals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rehearsals_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rehearsals_Setlists_SetlistId",
                        column: x => x.SetlistId,
                        principalTable: "Setlists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SetlistSongs",
                columns: table => new
                {
                    SetlistId = table.Column<int>(type: "integer", nullable: false),
                    SongId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetlistSongs", x => new { x.SetlistId, x.SongId });
                    table.ForeignKey(
                        name: "FK_SetlistSongs_Setlists_SetlistId",
                        column: x => x.SetlistId,
                        principalTable: "Setlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetlistSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin-id-001", "1", "Admin", "ADMIN" },
                    { "role-user-id-001", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "de305d54-75b4-4311-81d9-7ed39190224b", 0, "d71e99f6-21c8-4f38-b9da-9b623fdb906d", new DateTime(2026, 6, 24, 19, 10, 40, 314, DateTimeKind.Utc).AddTicks(4889), null, "admin@rehearsalhub.com", true, false, null, null, "ADMIN@REHEARSALHUB.COM", "ADMIN@REHEARSALHUB.COM", "AQAAAAIAAYagAAAAEB/GSaepWMVwdJEnJ1MYonHnGfRUwiygcSWg5a8UmyDwS9aozkWX91xxUDLtG9anGg==", null, false, "/images/defaults/users/user1.png", "939c0540-025c-43f1-9b63-938804008272", false, "admin@rehearsalhub.com" },
                    { "seed-user-1", 0, "d72ceeb9-792c-4339-bbce-0e6463ec50f3", new DateTime(2026, 6, 24, 19, 10, 40, 397, DateTimeKind.Utc).AddTicks(1973), null, "rockstar@test.com", true, false, null, null, "ROCKSTAR@TEST.COM", "ROCKSTAR@TEST.COM", "AQAAAAIAAYagAAAAECgzZZphuRbdzPdmVaRRY4Tc0RuM0BxV6UGqkDSulXUJLXsdKskkMHNFkyWmnYkpFQ==", null, false, "/images/defaults/users/user1.png", "59846067-8896-4874-9160-5582f3c306d1", false, "rockstar@test.com" },
                    { "seed-user-10", 0, "da4bcafc-d2d2-44c9-a314-530a139fb69a", new DateTime(2026, 6, 24, 19, 10, 41, 124, DateTimeKind.Utc).AddTicks(7485), null, "garage@test.com", true, false, null, null, "GARAGE@TEST.COM", "GARAGE@TEST.COM", "AQAAAAIAAYagAAAAEB6odyh6l1FW9YlRZzmHlhOVbXs5Zee5pc+NU8hmv5+4+rKxwRHLUOdvT8216N+b/w==", null, false, "/images/defaults/users/user2.png", "82281fd2-09f0-4fc8-8517-53db70becd4b", false, "garage@test.com" },
                    { "seed-user-2", 0, "f43df128-d342-44f3-99b2-7f91002bf04a", new DateTime(2026, 6, 24, 19, 10, 40, 474, DateTimeKind.Utc).AddTicks(8532), null, "metalhead@test.com", true, false, null, null, "METALHEAD@TEST.COM", "METALHEAD@TEST.COM", "AQAAAAIAAYagAAAAEAY8+zX4uGpsFKWE1CA1yxhNn2K2Zl0v+LpWHVxwmlsGk2Px/jdPvC7QKGOjDQJIDg==", null, false, "/images/defaults/users/user1.png", "f4c9448a-6f4e-4f0e-9180-2a86d2358899", false, "metalhead@test.com" },
                    { "seed-user-3", 0, "9c258e03-f05e-4c62-825a-2e99d2ebd1ac", new DateTime(2026, 6, 24, 19, 10, 40, 556, DateTimeKind.Utc).AddTicks(5987), null, "jazzman@test.com", true, false, null, null, "JAZZMAN@TEST.COM", "JAZZMAN@TEST.COM", "AQAAAAIAAYagAAAAEAqfLD92U4cc/RGGNIDC7fCSVoiaJU2u4ghiQ0I6t6qHyECqNUhe5f3GyP9HrjD7cA==", null, false, "/images/defaults/users/user2.png", "788019a3-5c56-4b8c-8f96-339832679f22", false, "jazzman@test.com" },
                    { "seed-user-4", 0, "fc9ca866-6a6a-4476-a993-4b4a2613911d", new DateTime(2026, 6, 24, 19, 10, 40, 636, DateTimeKind.Utc).AddTicks(715), null, "bluesman@test.com", true, false, null, null, "BLUESMAN@TEST.COM", "BLUESMAN@TEST.COM", "AQAAAAIAAYagAAAAEH5sNAbRnMeWta/5iP7pQFBkwv3EmSb0GtPhoVJoCgMM9CMG+qABo6rqeKI5EK68nA==", null, false, "/images/defaults/users/user2.png", "0124bc4f-de70-453c-bee3-afea7029b945", false, "bluesman@test.com" },
                    { "seed-user-5", 0, "739daf55-329f-4828-924c-50776d07898d", new DateTime(2026, 6, 24, 19, 10, 40, 714, DateTimeKind.Utc).AddTicks(2157), null, "funky@test.com", true, false, null, null, "FUNKY@TEST.COM", "FUNKY@TEST.COM", "AQAAAAIAAYagAAAAEHqEUglTA7ytRhh/ekWYcS/2OKBN7fDwrBK/CblxneUdYFKdcl0E78kGwhF5QINSQQ==", null, false, "/images/defaults/users/user3.png", "e93af285-52f1-48d1-bf6e-dfcc02de5458", false, "funky@test.com" },
                    { "seed-user-6", 0, "2a4cf138-f928-459b-b659-7d39812b0bee", new DateTime(2026, 6, 24, 19, 10, 40, 792, DateTimeKind.Utc).AddTicks(6524), null, "hiphop@test.com", true, false, null, null, "HIPHOP@TEST.COM", "HIPHOP@TEST.COM", "AQAAAAIAAYagAAAAEL6rIg8r+NILVo2rkCDMwWi5pEX/wV38dVeWVC6LdI6JA21vBAjyip968YusY7vOTA==", null, false, "/images/defaults/users/user3.png", "8ffae08e-7618-4854-81ea-ddaa74d432eb", false, "hiphop@test.com" },
                    { "seed-user-7", 0, "4c067be9-a47d-4813-914a-28acb27c6b42", new DateTime(2026, 6, 24, 19, 10, 40, 877, DateTimeKind.Utc).AddTicks(6857), null, "electro@test.com", true, false, null, null, "ELECTRO@TEST.COM", "ELECTRO@TEST.COM", "AQAAAAIAAYagAAAAEC7ciaHAPgK4ncqtQYlhAyut8Y2auKJgQHaaEH4ammGwAtbbG71HqZs1EQxMpXbjow==", null, false, "/images/defaults/users/user3.png", "9331887e-c988-4778-b22a-a58f62453200", false, "electro@test.com" },
                    { "seed-user-8", 0, "b6560689-32a7-467c-9339-aadf3ca1c53c", new DateTime(2026, 6, 24, 19, 10, 40, 970, DateTimeKind.Utc).AddTicks(3308), null, "popstar@test.com", true, false, null, null, "POPSTAR@TEST.COM", "POPSTAR@TEST.COM", "AQAAAAIAAYagAAAAECkQTJyp7ZCfjV/WiabPXvzEQuFbYlngRDMV9eVEFVqIpyQHzBPlx1VWUATt1BQYag==", null, false, "/images/defaults/users/user3.png", "e62bf054-9890-43e0-a3c0-0e218686958f", false, "popstar@test.com" },
                    { "seed-user-9", 0, "a644b17b-85e0-4435-82bf-5fb306e11dc3", new DateTime(2026, 6, 24, 19, 10, 41, 49, DateTimeKind.Utc).AddTicks(5233), null, "soul@test.com", true, false, null, null, "SOUL@TEST.COM", "SOUL@TEST.COM", "AQAAAAIAAYagAAAAEA+L142fWe9n3eqvzfqnBf/Vid/z9+JmtL7iSNaDyhICiCzolUBqfvnuJw3MUV4ejg==", null, false, "/images/defaults/users/user2.png", "22a74771-9ec7-4081-a797-d021525dd726", false, "soul@test.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "role-admin-id-001", "de305d54-75b4-4311-81d9-7ed39190224b" });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Genre", "ImageUrl", "ModifiedOn", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5176), null, 1, "/images/defaults/bands/band1.png", null, "RockStars", "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 2, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5181), null, 2, "/images/defaults/bands/band1.png", null, "MetalHeads", "seed-user-2" },
                    { 3, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5185), null, 4, "/images/defaults/bands/band3.png", null, "Jazz Collective", "seed-user-3" },
                    { 4, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5188), null, 5, "/images/defaults/bands/band3.png", null, "Blues Brothers", "seed-user-4" },
                    { 5, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5191), null, 6, "/images/defaults/bands/band1.png", null, "Funk Factory", "seed-user-5" },
                    { 6, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5211), null, 7, "/images/defaults/bands/band1.png", null, "Urban Flow", "seed-user-6" },
                    { 7, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5214), null, 8, "/images/defaults/bands/band1.png", null, "ElectroWave", "seed-user-7" },
                    { 8, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5221), null, 3, "/images/defaults/bands/band1.png", null, "Pop Squad", "seed-user-8" },
                    { 9, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5224), null, 2, "/images/defaults/bands/band1.png", null, "Heavy Unit", "seed-user-9" },
                    { 10, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5228), null, 1, "/images/defaults/bands/band2.png", null, "Alternative Vibes", "seed-user-10" },
                    { 11, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5261), null, 6, "/images/defaults/bands/band3.png", null, "Soul Train", "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 12, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5264), null, 4, "/images/defaults/bands/band3.png", null, "Night Jam", "seed-user-2" },
                    { 13, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5267), null, 1, "/images/defaults/bands/band3.png", null, "Garage Noise", "seed-user-3" },
                    { 14, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5270), null, 2, "/images/defaults/bands/band1.png", null, "Dark Riffs", "seed-user-4" },
                    { 15, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5273), null, 5, "/images/defaults/bands/band3.png", null, "Smooth Tones", "seed-user-5" },
                    { 16, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5276), null, 1, "/images/defaults/bands/band2.png", null, "Stage Kings", "seed-user-6" },
                    { 17, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5279), null, 6, "/images/defaults/bands/band1.png", null, "Groove Lab", "seed-user-7" },
                    { 18, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5283), null, 7, "/images/defaults/bands/band3.png", null, "Beat Makers", "seed-user-8" },
                    { 19, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5286), null, 8, "/images/defaults/bands/band2.png", null, "Synth Storm", "seed-user-9" },
                    { 20, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(5290), null, 3, "/images/defaults/bands/band3.png", null, "Pop Nation", "seed-user-10" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "CreatedOn", "CreatorId", "DeletedOn", "Duration", "Genre", "IsPrivate", "ModifiedOn", "MusicalKey", "OwnerBandId", "Tempo", "Title" },
                values: new object[,]
                {
                    { 1, "AC/DC", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8037), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 1, false, null, 0, null, 94, "Back in Black" },
                    { 2, "Guns N' Roses", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8042), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:56", 1, false, null, 8, null, 125, "Sweet Child O' Mine" },
                    { 3, "Led Zeppelin", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8044), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:02", 1, false, null, 19, null, 82, "Stairway to Heaven" },
                    { 4, "Pink Floyd", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8047), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:22", 1, false, null, 22, null, 65, "Comfortably Numb" },
                    { 5, "Eagles", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8049), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:30", 1, false, null, 22, null, 74, "Hotel California" },
                    { 6, "Deep Purple", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8052), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:40", 1, false, null, 35, null, 114, "Smoke on the Water" },
                    { 7, "Aerosmith", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8055), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:28", 1, false, null, 32, null, 80, "Dream On" },
                    { 8, "Bon Jovi", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8057), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:09", 1, false, null, 30, null, 123, "Livin' on a Prayer" },
                    { 9, "Red Hot Chili Peppers", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8062), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:24", 1, false, null, 11, null, 85, "Under the Bridge" },
                    { 10, "Nirvana", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8065), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:01", 1, false, null, 32, null, 117, "Smells Like Teen Spirit" },
                    { 11, "Queen", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8067), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:35", 1, false, null, 30, null, 110, "Another One Bites the Dust" },
                    { 12, "Led Zeppelin", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8069), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:34", 1, false, null, 11, null, 89, "Whole Lotta Love" },
                    { 13, "AC/DC", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8073), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:28", 1, false, null, 0, null, 116, "Highway to Hell" },
                    { 14, "Oasis", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8075), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:18", 1, false, null, 32, null, 87, "Wonderwall" },
                    { 15, "The Rolling Stones", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8078), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:22", 1, false, null, 30, null, 159, "Paint It Black" },
                    { 16, "Led Zeppelin", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8081), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 1, false, null, 0, null, 82, "Black Dog" },
                    { 17, "The Rolling Stones", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8084), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:33", 1, false, null, 13, null, 122, "Start Me Up" },
                    { 18, "Queen", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8087), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:55", 1, false, null, 4, null, 72, "Bohemian Rhapsody" },
                    { 19, "Fleetwood Mac", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8089), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:38", 1, false, null, 13, null, 135, "Go Your Own Way" },
                    { 20, "Van Halen", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8130), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:01", 1, false, null, 5, null, 130, "Jump" },
                    { 21, "The Police", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8133), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:12", 1, false, null, 35, null, 132, "Roxanne" },
                    { 22, "Bryan Adams", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8135), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:35", 1, false, null, 8, null, 139, "Summer of '69" },
                    { 23, "Survivor", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8138), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:04", 1, false, null, 24, null, 109, "Eye of the Tiger" },
                    { 24, "The Clash", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8140), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:06", 1, false, null, 8, null, 113, "Should I Stay or Should I Go" },
                    { 25, "The Clash", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8143), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:20", 1, false, null, 30, null, 134, "London Calling" },
                    { 26, "Pink Floyd", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8146), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:34", 1, false, null, 16, null, 60, "Wish You Were Here" },
                    { 27, "Bruce Springsteen", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8148), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:31", 1, false, null, 11, null, 148, "Born to Run" },
                    { 28, "The Police", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8150), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:13", 1, false, null, 0, null, 117, "Every Breath You Take" },
                    { 29, "U2", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8153), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 1, false, null, 8, null, 110, "With or Without You" },
                    { 30, "R.E.M.", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8156), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:28", 1, false, null, 19, null, 125, "Losing My Religion" },
                    { 31, "Dire Straits", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8158), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:48", 1, false, null, 27, null, 148, "Sultans of Swing" },
                    { 32, "Radiohead", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8160), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:23", 1, false, null, 35, null, 82, "Paranoid Android" },
                    { 33, "Radiohead", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8163), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:59", 1, false, null, 16, null, 92, "Creep" },
                    { 34, "The White Stripes", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8167), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:51", 1, false, null, 30, null, 124, "Seven Nation Army" },
                    { 35, "Muse", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8171), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:40", 1, false, null, 22, null, 136, "Plug In Baby" },
                    { 36, "Foo Fighters", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8173), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 1, false, null, 3, null, 130, "Best of You" },
                    { 37, "Foo Fighters", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8176), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:10", 1, false, null, 8, null, 158, "Everlong" },
                    { 38, "Linkin Park", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8178), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:36", 1, false, null, 30, null, 105, "In the End" },
                    { 39, "The Cranberries", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8181), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:06", 1, false, null, 30, null, 84, "Zombie" },
                    { 40, "ZZ Top", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8183), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:13", 1, false, null, 5, null, 125, "Sharp Dressed Man" },
                    { 41, "Metallica", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8185), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:35", 2, false, null, 30, null, 212, "Master of Puppets" },
                    { 42, "Black Sabbath", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8190), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:48", 2, false, null, 30, null, 163, "Paranoid" },
                    { 43, "Iron Maiden", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8192), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:12", 2, false, null, 30, null, 160, "The Trooper" },
                    { 44, "Slayer", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8195), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 2, false, null, 12, null, 210, "Raining Blood" },
                    { 45, "Megadeth", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8197), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:32", 2, false, null, 30, null, 188, "Holy Wars" },
                    { 46, "Judas Priest", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8199), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:06", 2, false, null, 30, null, 209, "Painkiller" },
                    { 47, "Pantera", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8201), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:14", 2, false, null, 8, null, 92, "Walk" },
                    { 48, "Rage Against the Machine", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8204), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:51", 2, false, null, 32, null, 89, "Bulls on Parade" },
                    { 49, "Rammstein", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8206), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:54", 2, false, null, 30, null, 125, "Du Hast" },
                    { 50, "System of a Down", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8209), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:30", 2, false, null, 35, null, 127, "Chop Suey!" },
                    { 51, "Metallica", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8211), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:31", 2, false, null, 30, null, 123, "Enter Sandman" },
                    { 52, "Iron Maiden", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8214), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:11", 2, false, null, 30, null, 171, "Hallowed Be Thy Name" },
                    { 53, "Motörhead", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8217), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 2, false, null, 12, null, 140, "Ace of Spades" },
                    { 54, "Megadeth", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8307), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:02", 2, false, null, 30, null, 140, "Symphony of Destruction" },
                    { 55, "Pantera", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8310), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:03", 2, false, null, 30, null, 114, "Cowboys from Hell" },
                    { 56, "Slipknot", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8312), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:44", 2, false, null, 0, null, 135, "Psychosocial" },
                    { 57, "Tool", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8315), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:47", 2, false, null, 27, null, 107, "Schism" },
                    { 58, "Iron Maiden", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8317), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:18", 2, false, null, 30, null, 110, "Fear of the Dark" },
                    { 59, "Type O Negative", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8320), "de305d54-75b4-4311-81d9-7ed39190224b", null, "11:15", 2, false, null, 3, null, 95, "Black No. 1" },
                    { 60, "System of a Down", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8322), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:39", 2, false, null, 24, null, 95, "Toxicity" },
                    { 61, "Sepultura", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8326), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:32", 2, false, null, 3, null, 124, "Roots Bloody Roots" },
                    { 62, "Ozzy Osbourne", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8328), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 2, false, null, 0, null, 138, "Crazy Train" },
                    { 63, "Dio", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8330), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 2, false, null, 19, null, 114, "Rainbow in the Dark" },
                    { 64, "Judas Priest", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8333), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:35", 2, false, null, 19, null, 163, "Breaking the Law" },
                    { 65, "Anthrax", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8335), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:59", 2, false, null, 11, null, 184, "Caught in a Mosh" },
                    { 66, "Korn", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8338), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:19", 2, false, null, 0, null, 97, "Blind" },
                    { 67, "Korn", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8341), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 2, false, null, 22, null, 112, "Freak on a Leash" },
                    { 68, "Rammstein", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8343), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:24", 2, false, null, 27, null, 95, "Engel" },
                    { 69, "Rob Zombie", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8345), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:42", 2, false, null, 30, null, 125, "Dragula" },
                    { 70, "Rage Against the Machine", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8348), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:14", 2, false, null, 8, null, 82, "Killing in the Name" },
                    { 71, "Metallica", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8350), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:28", 2, false, null, 30, null, 46, "Nothing Else Matters" },
                    { 72, "Metallica", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8352), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:27", 2, false, null, 22, null, 103, "One" },
                    { 73, "Iron Maiden", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8355), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:07", 2, false, null, 30, null, 154, "Wasted Years" },
                    { 74, "Avenged Sevenfold", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8357), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:16", 2, false, null, 27, null, 130, "Nightmare" },
                    { 75, "Avenged Sevenfold", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8360), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:13", 2, false, null, 27, null, 125, "Bat Country" },
                    { 76, "Avenged Sevenfold", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8362), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:00", 2, false, null, 27, null, 145, "A Little Piece of Heaven" },
                    { 77, "Papa Roach", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8365), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:19", 2, false, null, 30, null, 91, "Last Resort" },
                    { 78, "Disturbed", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8367), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:05", 2, false, null, 9, null, 174, "Stricken" },
                    { 79, "Disturbed", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8370), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:38", 2, false, null, 30, null, 90, "Down with the Sickness" },
                    { 80, "System of a Down", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8372), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 2, false, null, 24, null, 105, "B.Y.O.B." },
                    { 81, "B.B. King", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8374), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:24", 5, false, null, 22, null, 90, "The Thrill Is Gone" },
                    { 82, "Stevie Ray Vaughan", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8376), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:39", 5, false, null, 11, null, 126, "Pride and Joy" },
                    { 83, "Eric Clapton", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8379), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 5, false, null, 0, null, 110, "Crossroads" },
                    { 84, "Muddy Waters", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8381), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 5, false, null, 0, null, 76, "Hoochie Coochie Man" },
                    { 85, "Jimi Hendrix", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8383), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:50", 5, false, null, 3, null, 66, "Red House" },
                    { 86, "Albert King", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8386), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:47", 5, false, null, 9, null, 88, "Born Under a Bad Sign" },
                    { 87, "Robert Johnson", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8442), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:59", 5, false, null, 11, null, 118, "Sweet Home Chicago" },
                    { 88, "Howlin' Wolf", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8444), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:07", 5, false, null, 11, null, 145, "Smokestack Lightnin'" },
                    { 89, "Stevie Ray Vaughan", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8447), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:21", 5, false, null, 16, null, 60, "Texas Flood" },
                    { 90, "Elmore James", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8449), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:45", 5, false, null, 8, null, 100, "Dust My Broom" },
                    { 91, "Gary Moore", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8451), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:10", 5, false, null, 19, null, 52, "Still Got the Blues" },
                    { 92, "John Lee Hooker", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8454), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:29", 5, false, null, 11, null, 168, "Boom Boom" },
                    { 93, "Gary Moore", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8456), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:58", 5, false, null, 24, null, 64, "Midnight Blues" },
                    { 94, "Buddy Guy", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8458), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:36", 5, false, null, 3, null, 138, "Messin' with the Kid" },
                    { 95, "T-Bone Walker", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8461), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:02", 5, false, null, 16, null, 64, "Stormy Monday" },
                    { 96, "Elmore James", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8464), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:43", 5, false, null, 5, null, 62, "The Sky Is Crying" },
                    { 97, "Muddy Waters", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8466), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:23", 5, false, null, 0, null, 72, "Mannish Boy" },
                    { 98, "Willie Dixon", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8468), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 5, false, null, 0, null, 76, "I'm Your Hoochie Coochie Man" },
                    { 99, "Freddie King", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8471), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:38", 5, false, null, 11, null, 140, "Hide Away" },
                    { 100, "Big Brother & The Holding Company", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8473), "de305d54-75b4-4311-81d9-7ed39190224b", null, "09:28", 5, false, null, 35, null, 62, "Ball and Chain" },
                    { 101, "Derek and the Dominos", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8476), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:02", 5, false, null, 5, null, 80, "Bell Bottom Blues" },
                    { 102, "Joe Bonamassa", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8478), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:20", 5, false, null, 5, null, 60, "Blues Deluxe" },
                    { 103, "Joe Bonamassa", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8480), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:13", 5, false, null, 35, null, 58, "Sloe Gin" },
                    { 104, "Stevie Ray Vaughan", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8482), "de305d54-75b4-4311-81d9-7ed39190224b", null, "09:11", 5, false, null, 22, null, 58, "Tin Pan Alley" },
                    { 105, "Stevie Ray Vaughan", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8485), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:28", 5, false, null, 0, null, 88, "Life by the Drop" },
                    { 106, "Led Zeppelin", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8487), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:24", 5, false, null, 24, null, 44, "Since I've Been Loving You" },
                    { 107, "Buddy Guy", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8489), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:29", 5, false, null, 19, null, 105, "Damn Right, I've Got the Blues" },
                    { 108, "Buddy Guy", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8492), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:23", 5, false, null, 0, null, 63, "Five Long Years" },
                    { 109, "Howlin' Wolf", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8495), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:22", 5, false, null, 16, null, 72, "Little Red Rooster" },
                    { 110, "Etta James", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8497), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:07", 5, false, null, 16, null, 110, "I Just Want to Make Love to You" },
                    { 111, "Etta James", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8499), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:35", 5, false, null, 0, null, 68, "I'd Rather Go Blind" },
                    { 112, "Otis Rush", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8503), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:06", 5, false, null, 0, null, 60, "I Can't Quit You Baby" },
                    { 113, "Sonny Boy Williamson II", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8505), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:08", 5, false, null, 19, null, 114, "Help Me" },
                    { 114, "Howlin' Wolf", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8507), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:53", 5, false, null, 0, null, 80, "Evil" },
                    { 115, "Koko Taylor", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8510), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:00", 5, false, null, 0, null, 110, "Wang Dang Doodle" },
                    { 116, "Albert King", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8512), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:18", 5, false, null, 16, null, 65, "Call It Stormy Monday" },
                    { 117, "Cream", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8514), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:10", 5, false, null, 8, null, 115, "Sunshine of Your Love" },
                    { 118, "Joe Bonamassa", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8517), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:07", 5, false, null, 16, null, 145, "Bridge to Better Days" },
                    { 119, "John Mayer", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8519), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:05", 5, false, null, 16, null, 62, "Gravity" },
                    { 120, "The Doors", new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(8521), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:04", 5, false, null, 11, null, 124, "Roadhouse Blues" }
                });

            migrationBuilder.InsertData(
                table: "BandMembers",
                columns: new[] { "Id", "AvatarUrl", "BandId", "CreatedOn", "DeletedOn", "Instrument", "InvitationToken", "IsConfirmed", "IsDeletedInvitation", "ModifiedOn", "Role", "UserId" },
                values: new object[,]
                {
                    { 1, "/images/defaults/members/member3.png", 1, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8206), null, 0, null, true, false, null, 0, "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 2, "/images/defaults/members/member1.png", 1, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8210), null, 1, null, true, false, null, 1, "seed-user-2" },
                    { 3, "/images/defaults/members/member2.png", 2, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8213), null, 0, null, true, false, null, 0, "seed-user-2" },
                    { 4, "/images/defaults/members/member2.png", 2, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8215), null, 2, null, true, false, null, 1, "seed-user-3" },
                    { 5, "/images/defaults/members/member2.png", 13, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8218), null, 3, null, true, false, null, 0, "seed-user-3" },
                    { 6, "/images/defaults/members/member2.png", 3, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8221), null, 5, null, true, false, null, 1, "seed-user-4" },
                    { 7, "/images/defaults/members/member3.png", 4, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8224), null, 0, null, true, false, null, 0, "seed-user-4" },
                    { 8, "/images/defaults/members/member1.png", 4, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8292), null, 1, null, true, false, null, 1, "seed-user-5" },
                    { 9, "/images/defaults/members/member1.png", 5, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8296), null, 4, null, true, false, null, 0, "seed-user-5" },
                    { 10, "/images/defaults/members/member3.png", 5, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8299), null, 2, null, true, false, null, 1, "seed-user-6" },
                    { 11, "/images/defaults/members/member1.png", 6, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8302), null, 3, null, true, false, null, 0, "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 12, "/images/defaults/members/member1.png", 6, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8304), null, 0, null, true, false, null, 1, "seed-user-7" },
                    { 13, "/images/defaults/members/member1.png", 7, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8307), null, 7, null, true, false, null, 0, "seed-user-7" },
                    { 14, "/images/defaults/members/member2.png", 7, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8309), null, 4, null, true, false, null, 1, "seed-user-8" },
                    { 15, "/images/defaults/members/member3.png", 8, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8312), null, 5, null, true, false, null, 0, "seed-user-8" },
                    { 16, "/images/defaults/members/member3.png", 8, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8315), null, 2, null, true, false, null, 1, "seed-user-9" },
                    { 17, "/images/defaults/members/member3.png", 9, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8317), null, 0, null, true, false, null, 0, "seed-user-9" },
                    { 18, "/images/defaults/members/member2.png", 9, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8320), null, 1, null, true, false, null, 1, "seed-user-10" },
                    { 19, "/images/defaults/members/member2.png", 10, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8323), null, 3, null, true, false, null, 0, "seed-user-10" },
                    { 20, "/images/defaults/members/member1.png", 10, new DateTime(2026, 6, 24, 19, 10, 41, 125, DateTimeKind.Utc).AddTicks(8325), null, 2, null, true, false, null, 1, "seed-user-1" }
                });

            migrationBuilder.InsertData(
                table: "Setlists",
                columns: new[] { "Id", "BandId", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "RehearsalDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4975), null, null, "Rehearsal Set", null },
                    { 2, 1, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4978), null, null, "Live Show", null },
                    { 3, 2, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4980), null, null, "Practice Night", null },
                    { 4, 3, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4982), null, null, "Festival Set", null },
                    { 5, 4, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4984), null, null, "Warmup", null },
                    { 6, 5, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4987), null, null, "Main Set", null },
                    { 7, 6, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4989), null, null, "Encore Set", null },
                    { 8, 7, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4991), null, null, "Acoustic", null },
                    { 9, 8, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4993), null, null, "Heavy Set", null },
                    { 10, 9, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4995), null, null, "Chill Set", null },
                    { 11, 10, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4997), null, null, "Night Session", null },
                    { 12, 11, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(4999), null, null, "Club Gig", null },
                    { 13, 12, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(5000), null, null, "Studio Test", null },
                    { 14, 13, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(5002), null, null, "Jam Session", null },
                    { 15, 14, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(5004), null, null, "Road Trip", null },
                    { 16, 15, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(5006), null, null, "Basement", null },
                    { 17, 16, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(5008), null, null, "Open Air", null },
                    { 18, 17, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(5011), null, null, "Late Show", null },
                    { 19, 18, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(5013), null, null, "Soundcheck", null },
                    { 20, 19, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(5014), null, null, "Final Show", null }
                });

            migrationBuilder.InsertData(
                table: "Rehearsals",
                columns: new[] { "Id", "BandId", "CreatedOn", "DeletedOn", "EndRehearsal", "ModifiedOn", "Name", "Notes", "SetlistId", "StartRehearsal" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1093), null, new DateTime(2026, 5, 20, 17, 0, 0, 0, DateTimeKind.Utc), null, "Evening Practice", null, 1, new DateTime(2026, 5, 20, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1108), null, new DateTime(2026, 5, 21, 14, 0, 0, 0, DateTimeKind.Utc), null, "Studio Jam", null, 3, new DateTime(2026, 5, 21, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1138), null, new DateTime(2026, 5, 22, 9, 0, 0, 0, DateTimeKind.Utc), null, "Sound Check", null, 4, new DateTime(2026, 5, 22, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1156), null, new DateTime(2026, 5, 23, 18, 0, 0, 0, DateTimeKind.Utc), null, "Full Band", null, 5, new DateTime(2026, 5, 23, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1181), null, new DateTime(2026, 5, 24, 9, 0, 0, 0, DateTimeKind.Utc), null, "Warmup", null, 6, new DateTime(2026, 5, 24, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1194), null, new DateTime(2026, 5, 25, 21, 0, 0, 0, DateTimeKind.Utc), null, "Late Night", null, 7, new DateTime(2026, 5, 25, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 7, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1206), null, new DateTime(2026, 5, 26, 15, 0, 0, 0, DateTimeKind.Utc), null, "Groove Session", null, 8, new DateTime(2026, 5, 26, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 8, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1260), null, new DateTime(2026, 5, 27, 12, 0, 0, 0, DateTimeKind.Utc), null, "Drum Focus", null, 9, new DateTime(2026, 5, 27, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 9, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1272), null, new DateTime(2026, 5, 28, 16, 0, 0, 0, DateTimeKind.Utc), null, "Vocal Practice", null, 10, new DateTime(2026, 5, 28, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 10, new DateTime(2026, 6, 24, 19, 10, 41, 126, DateTimeKind.Utc).AddTicks(1285), null, new DateTime(2026, 5, 29, 16, 0, 0, 0, DateTimeKind.Utc), null, "Stage Run", null, 11, new DateTime(2026, 5, 29, 13, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "SetlistSongs",
                columns: new[] { "SetlistId", "SongId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 8 },
                    { 4, 9 },
                    { 5, 10 },
                    { 6, 11 },
                    { 7, 12 },
                    { 8, 13 },
                    { 9, 14 },
                    { 10, 15 },
                    { 11, 16 },
                    { 12, 17 },
                    { 13, 18 },
                    { 14, 19 },
                    { 15, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BandMembers_BandId",
                table: "BandMembers",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_BandMembers_UserId_BandId",
                table: "BandMembers",
                columns: new[] { "UserId", "BandId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bands_OwnerId",
                table: "Bands",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientId",
                table: "Notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rehearsals_BandId",
                table: "Rehearsals",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Rehearsals_SetlistId",
                table: "Rehearsals",
                column: "SetlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Setlists_BandId",
                table: "Setlists",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_SetlistSongs_SongId",
                table: "SetlistSongs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_CreatorId",
                table: "Songs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_OwnerBandId",
                table: "Songs",
                column: "OwnerBandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BandMembers");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Rehearsals");

            migrationBuilder.DropTable(
                name: "SetlistSongs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Setlists");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
