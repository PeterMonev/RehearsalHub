using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RehearsalHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    BandId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    InvitationToken = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Instrument = table.Column<int>(type: "int", nullable: false),
                    IsDeletedInvitation = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BandId = table.Column<int>(type: "int", nullable: false),
                    RehearsalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artist = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    MusicalKey = table.Column<int>(type: "int", nullable: false),
                    Tempo = table.Column<int>(type: "int", nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    OwnerBandId = table.Column<int>(type: "int", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartRehearsal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndRehearsal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BandId = table.Column<int>(type: "int", nullable: false),
                    SetlistId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    SetlistId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
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
                    { "de305d54-75b4-4311-81d9-7ed39190224b", 0, "828a7652-d2d3-449b-92cb-30a427f66160", new DateTime(2026, 4, 7, 9, 59, 22, 102, DateTimeKind.Utc).AddTicks(1570), null, "admin@rehearsalhub.com", true, false, null, null, "ADMIN@REHEARSALHUB.COM", "ADMIN@REHEARSALHUB.COM", "AQAAAAIAAYagAAAAEEJ9Vav0uDc1vYL4R6LC/V4fTxSjlxEbFnZj1/yeSkz6f6zcKroDB6Koi0/JbBki6Q==", null, false, "/images/defaults/users/user3.png", "939c0540-025c-43f1-9b63-938804008272", false, "admin@rehearsalhub.com" },
                    { "seed-user-1", 0, "8314ccff-4783-4374-873b-17826613f298", new DateTime(2026, 4, 7, 9, 59, 22, 181, DateTimeKind.Utc).AddTicks(9072), null, "rockstar@test.com", true, false, null, null, "ROCKSTAR@TEST.COM", "ROCKSTAR@TEST.COM", "AQAAAAIAAYagAAAAELrfHXFjs6sEUaaCUOwU0gNb5endPvajnbno7KLAIIX8NEdc5bvdWAM16ajA178z5Q==", null, false, "/images/defaults/users/user1.png", "59846067-8896-4874-9160-5582f3c306d1", false, "rockstar@test.com" },
                    { "seed-user-10", 0, "2006d9fd-7d0f-4ad5-98da-17ddad3c82fd", new DateTime(2026, 4, 7, 9, 59, 22, 940, DateTimeKind.Utc).AddTicks(2042), null, "garage@test.com", true, false, null, null, "GARAGE@TEST.COM", "GARAGE@TEST.COM", "AQAAAAIAAYagAAAAEF7f9fPONwLoZds4azrjQs7Oci3QUCflvNcyGSPPReaGCGdsB5nguBmSYkbPiQa0Ew==", null, false, "/images/defaults/users/user3.png", "8fb715c7-093e-4a6f-ac75-351a72ccaf30", false, "garage@test.com" },
                    { "seed-user-2", 0, "8278cacd-c69f-4902-84d3-e983ef9581b3", new DateTime(2026, 4, 7, 9, 59, 22, 297, DateTimeKind.Utc).AddTicks(6487), null, "metalhead@test.com", true, false, null, null, "METALHEAD@TEST.COM", "METALHEAD@TEST.COM", "AQAAAAIAAYagAAAAED8bME4YPVEUHzyBO+j7DTrWtTPoFAzevLL+y3JNDcyqrBEZa+2vMddgMeNPPxm1+A==", null, false, "/images/defaults/users/user3.png", "f4c9448a-6f4e-4f0e-9180-2a86d2358899", false, "metalhead@test.com" },
                    { "seed-user-3", 0, "3de13b20-5e43-46f9-94d0-1fd361f73e10", new DateTime(2026, 4, 7, 9, 59, 22, 393, DateTimeKind.Utc).AddTicks(3179), null, "jazzman@test.com", true, false, null, null, "JAZZMAN@TEST.COM", "JAZZMAN@TEST.COM", "AQAAAAIAAYagAAAAENz0MgEwt64OwWAkcau8SAgly7AmkAbaxSglfIPIDKGVMdGXXNFEJHEQIzwkboFrqA==", null, false, "/images/defaults/users/user3.png", "788019a3-5c56-4b8c-8f96-339832679f22", false, "jazzman@test.com" },
                    { "seed-user-4", 0, "93699cc9-96cb-4bcb-bec1-aece523d9a44", new DateTime(2026, 4, 7, 9, 59, 22, 475, DateTimeKind.Utc).AddTicks(2233), null, "bluesman@test.com", true, false, null, null, "BLUESMAN@TEST.COM", "BLUESMAN@TEST.COM", "AQAAAAIAAYagAAAAEOerNQy2jrfUKP59EWrCxicHvx4Qn8WNoclp40x3nE5L0BRToZPqY3J1/Aqu9yOpjA==", null, false, "/images/defaults/users/user2.png", "b49d4d09-82cd-4068-9f3d-0af2b1424acf", false, "bluesman@test.com" },
                    { "seed-user-5", 0, "b842134d-a3d6-4fb6-91cf-595f80ac1847", new DateTime(2026, 4, 7, 9, 59, 22, 553, DateTimeKind.Utc).AddTicks(3112), null, "funky@test.com", true, false, null, null, "FUNKY@TEST.COM", "FUNKY@TEST.COM", "AQAAAAIAAYagAAAAEEMcq/GlOttcREezTRGOkDjSpNK9yKAg3LX12S9uqpWqXJ9U3vdClPlte3Tg9CjIkg==", null, false, "/images/defaults/users/user1.png", "5e1e6243-d24f-4457-9199-ff416ccee69e", false, "funky@test.com" },
                    { "seed-user-6", 0, "0418b600-73f1-4c25-955c-518138d39bc4", new DateTime(2026, 4, 7, 9, 59, 22, 630, DateTimeKind.Utc).AddTicks(442), null, "hiphop@test.com", true, false, null, null, "HIPHOP@TEST.COM", "HIPHOP@TEST.COM", "AQAAAAIAAYagAAAAENR6O3xtJr87G3YZewxNiIpKa2DqklkMnmql0bAkidiiSlt/jw66gZbAT5xC9rcWQA==", null, false, "/images/defaults/users/user2.png", "774d0e71-30a4-4244-aea7-3b78ab12ce18", false, "hiphop@test.com" },
                    { "seed-user-7", 0, "340b39f4-53eb-466c-aaa3-e024658397a7", new DateTime(2026, 4, 7, 9, 59, 22, 710, DateTimeKind.Utc).AddTicks(7263), null, "electro@test.com", true, false, null, null, "ELECTRO@TEST.COM", "ELECTRO@TEST.COM", "AQAAAAIAAYagAAAAEIZEAz/J+PZ0gREL9Dbt3sJ4LKOGuiDmwfKdM4gim/r0x0MrSPGOHNcqm3CUskVaBg==", null, false, "/images/defaults/users/user3.png", "2c92b02f-b4f5-4b1d-a9a1-54ff55423730", false, "electro@test.com" },
                    { "seed-user-8", 0, "533704d4-08c7-4e1f-bf9a-56a45e61b384", new DateTime(2026, 4, 7, 9, 59, 22, 788, DateTimeKind.Utc).AddTicks(8964), null, "popstar@test.com", true, false, null, null, "POPSTAR@TEST.COM", "POPSTAR@TEST.COM", "AQAAAAIAAYagAAAAEJhFmNHN7Gcmsr687y7+n3vbY125Gk6Nu9c7ob0jFw/LCXtuHcgr4AUJl6k37pqm5A==", null, false, "/images/defaults/users/user1.png", "58255c43-c53e-4e5f-ac42-e587eccb0c43", false, "popstar@test.com" },
                    { "seed-user-9", 0, "d9903268-8fe2-490f-bfc2-198104155425", new DateTime(2026, 4, 7, 9, 59, 22, 864, DateTimeKind.Utc).AddTicks(6612), null, "soul@test.com", true, false, null, null, "SOUL@TEST.COM", "SOUL@TEST.COM", "AQAAAAIAAYagAAAAEDn6i4zt9iCedSUtCaXqbYKjFm71yQp8K4JEp9AIwN2ROeJEyAP8RSMDRlrUIYhr+A==", null, false, "/images/defaults/users/user1.png", "b7032664-2e54-4027-8e88-ba4ff39f93c7", false, "soul@test.com" }
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
                    { 1, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2041), null, 1, "/images/defaults/bands/band3.png", null, "RockStars", "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 2, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2056), null, 2, "/images/defaults/bands/band3.png", null, "MetalHeads", "seed-user-2" },
                    { 3, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2059), null, 4, "/images/defaults/bands/band1.png", null, "Jazz Collective", "seed-user-3" },
                    { 4, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2063), null, 5, "/images/defaults/bands/band2.png", null, "Blues Brothers", "seed-user-4" },
                    { 5, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2066), null, 6, "/images/defaults/bands/band3.png", null, "Funk Factory", "seed-user-5" },
                    { 6, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2090), null, 7, "/images/defaults/bands/band2.png", null, "Urban Flow", "seed-user-6" },
                    { 7, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2096), null, 8, "/images/defaults/bands/band1.png", null, "ElectroWave", "seed-user-7" },
                    { 8, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2099), null, 3, "/images/defaults/bands/band2.png", null, "Pop Squad", "seed-user-8" },
                    { 9, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2102), null, 2, "/images/defaults/bands/band1.png", null, "Heavy Unit", "seed-user-9" },
                    { 10, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2110), null, 1, "/images/defaults/bands/band1.png", null, "Alternative Vibes", "seed-user-10" },
                    { 11, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2113), null, 6, "/images/defaults/bands/band3.png", null, "Soul Train", "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 12, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2116), null, 4, "/images/defaults/bands/band2.png", null, "Night Jam", "seed-user-2" },
                    { 13, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2119), null, 1, "/images/defaults/bands/band3.png", null, "Garage Noise", "seed-user-3" },
                    { 14, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2164), null, 2, "/images/defaults/bands/band3.png", null, "Dark Riffs", "seed-user-4" },
                    { 15, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2168), null, 5, "/images/defaults/bands/band2.png", null, "Smooth Tones", "seed-user-5" },
                    { 16, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2171), null, 1, "/images/defaults/bands/band3.png", null, "Stage Kings", "seed-user-6" },
                    { 17, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2174), null, 6, "/images/defaults/bands/band3.png", null, "Groove Lab", "seed-user-7" },
                    { 18, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2178), null, 7, "/images/defaults/bands/band2.png", null, "Beat Makers", "seed-user-8" },
                    { 19, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2181), null, 8, "/images/defaults/bands/band1.png", null, "Synth Storm", "seed-user-9" },
                    { 20, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(2184), null, 3, "/images/defaults/bands/band1.png", null, "Pop Nation", "seed-user-10" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "CreatedOn", "CreatorId", "DeletedOn", "Duration", "Genre", "IsPrivate", "ModifiedOn", "MusicalKey", "OwnerBandId", "Tempo", "Title" },
                values: new object[,]
                {
                    { 1, "AC/DC", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6892), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 1, false, null, 0, null, 94, "Back in Black" },
                    { 2, "Guns N' Roses", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6901), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:56", 1, false, null, 8, null, 125, "Sweet Child O' Mine" },
                    { 3, "Led Zeppelin", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6903), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:02", 1, false, null, 19, null, 82, "Stairway to Heaven" },
                    { 4, "Pink Floyd", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6906), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:22", 1, false, null, 22, null, 65, "Comfortably Numb" },
                    { 5, "Eagles", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6909), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:30", 1, false, null, 22, null, 74, "Hotel California" },
                    { 6, "Deep Purple", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6912), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:40", 1, false, null, 35, null, 114, "Smoke on the Water" },
                    { 7, "Aerosmith", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6915), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:28", 1, false, null, 32, null, 80, "Dream On" },
                    { 8, "Bon Jovi", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6917), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:09", 1, false, null, 30, null, 123, "Livin' on a Prayer" },
                    { 9, "Red Hot Chili Peppers", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6919), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:24", 1, false, null, 11, null, 85, "Under the Bridge" },
                    { 10, "Nirvana", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6923), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:01", 1, false, null, 32, null, 117, "Smells Like Teen Spirit" },
                    { 11, "Queen", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6925), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:35", 1, false, null, 30, null, 110, "Another One Bites the Dust" },
                    { 12, "Led Zeppelin", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6927), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:34", 1, false, null, 11, null, 89, "Whole Lotta Love" },
                    { 13, "AC/DC", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6930), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:28", 1, false, null, 0, null, 116, "Highway to Hell" },
                    { 14, "Oasis", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6932), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:18", 1, false, null, 32, null, 87, "Wonderwall" },
                    { 15, "The Rolling Stones", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6934), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:22", 1, false, null, 30, null, 159, "Paint It Black" },
                    { 16, "Led Zeppelin", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6938), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 1, false, null, 0, null, 82, "Black Dog" },
                    { 17, "The Rolling Stones", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6940), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:33", 1, false, null, 13, null, 122, "Start Me Up" },
                    { 18, "Queen", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6943), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:55", 1, false, null, 4, null, 72, "Bohemian Rhapsody" },
                    { 19, "Fleetwood Mac", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6946), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:38", 1, false, null, 13, null, 135, "Go Your Own Way" },
                    { 20, "Van Halen", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6948), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:01", 1, false, null, 5, null, 130, "Jump" },
                    { 21, "The Police", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6950), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:12", 1, false, null, 35, null, 132, "Roxanne" },
                    { 22, "Bryan Adams", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6953), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:35", 1, false, null, 8, null, 139, "Summer of '69" },
                    { 23, "Survivor", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6955), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:04", 1, false, null, 24, null, 109, "Eye of the Tiger" },
                    { 24, "The Clash", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(6957), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:06", 1, false, null, 8, null, 113, "Should I Stay or Should I Go" },
                    { 25, "The Clash", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7011), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:20", 1, false, null, 30, null, 134, "London Calling" },
                    { 26, "Pink Floyd", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7014), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:34", 1, false, null, 16, null, 60, "Wish You Were Here" },
                    { 27, "Bruce Springsteen", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7021), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:31", 1, false, null, 11, null, 148, "Born to Run" },
                    { 28, "The Police", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7023), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:13", 1, false, null, 0, null, 117, "Every Breath You Take" },
                    { 29, "U2", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7026), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 1, false, null, 8, null, 110, "With or Without You" },
                    { 30, "R.E.M.", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7028), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:28", 1, false, null, 19, null, 125, "Losing My Religion" },
                    { 31, "Dire Straits", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7030), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:48", 1, false, null, 27, null, 148, "Sultans of Swing" },
                    { 32, "Radiohead", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7033), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:23", 1, false, null, 35, null, 82, "Paranoid Android" },
                    { 33, "Radiohead", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7035), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:59", 1, false, null, 16, null, 92, "Creep" },
                    { 34, "The White Stripes", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7040), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:51", 1, false, null, 30, null, 124, "Seven Nation Army" },
                    { 35, "Muse", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7043), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:40", 1, false, null, 22, null, 136, "Plug In Baby" },
                    { 36, "Foo Fighters", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7045), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 1, false, null, 3, null, 130, "Best of You" },
                    { 37, "Foo Fighters", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7049), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:10", 1, false, null, 8, null, 158, "Everlong" },
                    { 38, "Linkin Park", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7052), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:36", 1, false, null, 30, null, 105, "In the End" },
                    { 39, "The Cranberries", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7056), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:06", 1, false, null, 30, null, 84, "Zombie" },
                    { 40, "ZZ Top", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7059), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:13", 1, false, null, 5, null, 125, "Sharp Dressed Man" },
                    { 41, "Metallica", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7061), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:35", 2, false, null, 30, null, 212, "Master of Puppets" },
                    { 42, "Black Sabbath", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7066), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:48", 2, false, null, 30, null, 163, "Paranoid" },
                    { 43, "Iron Maiden", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7068), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:12", 2, false, null, 30, null, 160, "The Trooper" },
                    { 44, "Slayer", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7073), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 2, false, null, 12, null, 210, "Raining Blood" },
                    { 45, "Megadeth", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7075), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:32", 2, false, null, 30, null, 188, "Holy Wars" },
                    { 46, "Judas Priest", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7080), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:06", 2, false, null, 30, null, 209, "Painkiller" },
                    { 47, "Pantera", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7082), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:14", 2, false, null, 8, null, 92, "Walk" },
                    { 48, "Rage Against the Machine", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7084), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:51", 2, false, null, 32, null, 89, "Bulls on Parade" },
                    { 49, "Rammstein", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7088), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:54", 2, false, null, 30, null, 125, "Du Hast" },
                    { 50, "System of a Down", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7090), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:30", 2, false, null, 35, null, 127, "Chop Suey!" },
                    { 51, "Metallica", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7093), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:31", 2, false, null, 30, null, 123, "Enter Sandman" },
                    { 52, "Iron Maiden", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7095), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:11", 2, false, null, 30, null, 171, "Hallowed Be Thy Name" },
                    { 53, "Motörhead", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7098), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 2, false, null, 12, null, 140, "Ace of Spades" },
                    { 54, "Megadeth", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7104), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:02", 2, false, null, 30, null, 140, "Symphony of Destruction" },
                    { 55, "Pantera", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7106), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:03", 2, false, null, 30, null, 114, "Cowboys from Hell" },
                    { 56, "Slipknot", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7109), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:44", 2, false, null, 0, null, 135, "Psychosocial" },
                    { 57, "Tool", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7111), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:47", 2, false, null, 27, null, 107, "Schism" },
                    { 58, "Iron Maiden", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7120), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:18", 2, false, null, 30, null, 110, "Fear of the Dark" },
                    { 59, "Type O Negative", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7122), "de305d54-75b4-4311-81d9-7ed39190224b", null, "11:15", 2, false, null, 3, null, 95, "Black No. 1" },
                    { 60, "System of a Down", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7125), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:39", 2, false, null, 24, null, 95, "Toxicity" },
                    { 61, "Sepultura", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7167), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:32", 2, false, null, 3, null, 124, "Roots Bloody Roots" },
                    { 62, "Ozzy Osbourne", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7170), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 2, false, null, 0, null, 138, "Crazy Train" },
                    { 63, "Dio", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7172), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 2, false, null, 19, null, 114, "Rainbow in the Dark" },
                    { 64, "Judas Priest", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7174), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:35", 2, false, null, 19, null, 163, "Breaking the Law" },
                    { 65, "Anthrax", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7178), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:59", 2, false, null, 11, null, 184, "Caught in a Mosh" },
                    { 66, "Korn", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7181), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:19", 2, false, null, 0, null, 97, "Blind" },
                    { 67, "Korn", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7183), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 2, false, null, 22, null, 112, "Freak on a Leash" },
                    { 68, "Rammstein", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7185), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:24", 2, false, null, 27, null, 95, "Engel" },
                    { 69, "Rob Zombie", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7188), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:42", 2, false, null, 30, null, 125, "Dragula" },
                    { 70, "Rage Against the Machine", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7190), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:14", 2, false, null, 8, null, 82, "Killing in the Name" },
                    { 71, "Metallica", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7193), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:28", 2, false, null, 30, null, 46, "Nothing Else Matters" },
                    { 72, "Metallica", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7196), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:27", 2, false, null, 22, null, 103, "One" },
                    { 73, "Iron Maiden", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7198), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:07", 2, false, null, 30, null, 154, "Wasted Years" },
                    { 74, "Avenged Sevenfold", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7201), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:16", 2, false, null, 27, null, 130, "Nightmare" },
                    { 75, "Avenged Sevenfold", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7203), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:13", 2, false, null, 27, null, 125, "Bat Country" },
                    { 76, "Avenged Sevenfold", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7205), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:00", 2, false, null, 27, null, 145, "A Little Piece of Heaven" },
                    { 77, "Papa Roach", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7208), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:19", 2, false, null, 30, null, 91, "Last Resort" },
                    { 78, "Disturbed", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7210), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:05", 2, false, null, 9, null, 174, "Stricken" },
                    { 79, "Disturbed", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7212), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:38", 2, false, null, 30, null, 90, "Down with the Sickness" },
                    { 80, "System of a Down", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7215), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 2, false, null, 24, null, 105, "B.Y.O.B." },
                    { 81, "B.B. King", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7218), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:24", 5, false, null, 22, null, 90, "The Thrill Is Gone" },
                    { 82, "Stevie Ray Vaughan", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7220), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:39", 5, false, null, 11, null, 126, "Pride and Joy" },
                    { 83, "Eric Clapton", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7223), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 5, false, null, 0, null, 110, "Crossroads" },
                    { 84, "Muddy Waters", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7225), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 5, false, null, 0, null, 76, "Hoochie Coochie Man" },
                    { 85, "Jimi Hendrix", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7227), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:50", 5, false, null, 3, null, 66, "Red House" },
                    { 86, "Albert King", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7230), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:47", 5, false, null, 9, null, 88, "Born Under a Bad Sign" },
                    { 87, "Robert Johnson", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7232), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:59", 5, false, null, 11, null, 118, "Sweet Home Chicago" },
                    { 88, "Howlin' Wolf", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7234), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:07", 5, false, null, 11, null, 145, "Smokestack Lightnin'" },
                    { 89, "Stevie Ray Vaughan", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7236), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:21", 5, false, null, 16, null, 60, "Texas Flood" },
                    { 90, "Elmore James", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7239), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:45", 5, false, null, 8, null, 100, "Dust My Broom" },
                    { 91, "Gary Moore", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7241), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:10", 5, false, null, 19, null, 52, "Still Got the Blues" },
                    { 92, "John Lee Hooker", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7243), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:29", 5, false, null, 11, null, 168, "Boom Boom" },
                    { 93, "Gary Moore", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7246), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:58", 5, false, null, 24, null, 64, "Midnight Blues" },
                    { 94, "Buddy Guy", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7285), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:36", 5, false, null, 3, null, 138, "Messin' with the Kid" },
                    { 95, "T-Bone Walker", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7288), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:02", 5, false, null, 16, null, 64, "Stormy Monday" },
                    { 96, "Elmore James", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7290), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:43", 5, false, null, 5, null, 62, "The Sky Is Crying" },
                    { 97, "Muddy Waters", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7293), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:23", 5, false, null, 0, null, 72, "Mannish Boy" },
                    { 98, "Willie Dixon", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7295), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 5, false, null, 0, null, 76, "I'm Your Hoochie Coochie Man" },
                    { 99, "Freddie King", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7298), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:38", 5, false, null, 11, null, 140, "Hide Away" },
                    { 100, "Big Brother & The Holding Company", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7300), "de305d54-75b4-4311-81d9-7ed39190224b", null, "09:28", 5, false, null, 35, null, 62, "Ball and Chain" },
                    { 101, "Derek and the Dominos", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7302), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:02", 5, false, null, 5, null, 80, "Bell Bottom Blues" },
                    { 102, "Joe Bonamassa", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7305), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:20", 5, false, null, 5, null, 60, "Blues Deluxe" },
                    { 103, "Joe Bonamassa", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7307), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:13", 5, false, null, 35, null, 58, "Sloe Gin" },
                    { 104, "Stevie Ray Vaughan", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7309), "de305d54-75b4-4311-81d9-7ed39190224b", null, "09:11", 5, false, null, 22, null, 58, "Tin Pan Alley" },
                    { 105, "Stevie Ray Vaughan", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7312), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:28", 5, false, null, 0, null, 88, "Life by the Drop" },
                    { 106, "Led Zeppelin", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7315), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:24", 5, false, null, 24, null, 44, "Since I've Been Loving You" },
                    { 107, "Buddy Guy", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7317), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:29", 5, false, null, 19, null, 105, "Damn Right, I've Got the Blues" },
                    { 108, "Buddy Guy", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7319), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:23", 5, false, null, 0, null, 63, "Five Long Years" },
                    { 109, "Howlin' Wolf", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7322), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:22", 5, false, null, 16, null, 72, "Little Red Rooster" },
                    { 110, "Etta James", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7324), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:07", 5, false, null, 16, null, 110, "I Just Want to Make Love to You" },
                    { 111, "Etta James", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7326), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:35", 5, false, null, 0, null, 68, "I'd Rather Go Blind" },
                    { 112, "Otis Rush", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7329), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:06", 5, false, null, 0, null, 60, "I Can't Quit You Baby" },
                    { 113, "Sonny Boy Williamson II", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7331), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:08", 5, false, null, 19, null, 114, "Help Me" },
                    { 114, "Howlin' Wolf", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7334), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:53", 5, false, null, 0, null, 80, "Evil" },
                    { 115, "Koko Taylor", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7337), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:00", 5, false, null, 0, null, 110, "Wang Dang Doodle" },
                    { 116, "Albert King", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7339), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:18", 5, false, null, 16, null, 65, "Call It Stormy Monday" },
                    { 117, "Cream", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7341), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:10", 5, false, null, 8, null, 115, "Sunshine of Your Love" },
                    { 118, "Joe Bonamassa", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7343), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:07", 5, false, null, 16, null, 145, "Bridge to Better Days" },
                    { 119, "John Mayer", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7346), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:05", 5, false, null, 16, null, 62, "Gravity" },
                    { 120, "The Doors", new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(7348), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:04", 5, false, null, 11, null, 124, "Roadhouse Blues" }
                });

            migrationBuilder.InsertData(
                table: "BandMembers",
                columns: new[] { "Id", "AvatarUrl", "BandId", "CreatedOn", "DeletedOn", "Instrument", "InvitationToken", "IsConfirmed", "IsDeletedInvitation", "ModifiedOn", "Role", "UserId" },
                values: new object[,]
                {
                    { 1, "/images/defaults/members/member2.png", 1, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6105), null, 0, null, true, false, null, 0, "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 2, "/images/defaults/members/member1.png", 1, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6115), null, 1, null, true, false, null, 1, "seed-user-2" },
                    { 3, "/images/defaults/members/member1.png", 2, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6118), null, 0, null, true, false, null, 0, "seed-user-2" },
                    { 4, "/images/defaults/members/member1.png", 2, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6121), null, 2, null, true, false, null, 1, "seed-user-3" },
                    { 5, "/images/defaults/members/member2.png", 13, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6124), null, 3, null, true, false, null, 0, "seed-user-3" },
                    { 6, "/images/defaults/members/member3.png", 3, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6128), null, 5, null, true, false, null, 1, "seed-user-4" },
                    { 7, "/images/defaults/members/member2.png", 4, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6131), null, 0, null, true, false, null, 0, "seed-user-4" },
                    { 8, "/images/defaults/members/member3.png", 4, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6134), null, 1, null, true, false, null, 1, "seed-user-5" },
                    { 9, "/images/defaults/members/member1.png", 5, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6137), null, 4, null, true, false, null, 0, "seed-user-5" },
                    { 10, "/images/defaults/members/member2.png", 5, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6140), null, 2, null, true, false, null, 1, "seed-user-6" },
                    { 11, "/images/defaults/members/member2.png", 6, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6143), null, 3, null, true, false, null, 0, "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 12, "/images/defaults/members/member2.png", 6, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6145), null, 0, null, true, false, null, 1, "seed-user-7" },
                    { 13, "/images/defaults/members/member2.png", 7, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6191), null, 7, null, true, false, null, 0, "seed-user-7" },
                    { 14, "/images/defaults/members/member2.png", 7, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6194), null, 4, null, true, false, null, 1, "seed-user-8" },
                    { 15, "/images/defaults/members/member3.png", 8, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6196), null, 5, null, true, false, null, 0, "seed-user-8" },
                    { 16, "/images/defaults/members/member2.png", 8, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6199), null, 2, null, true, false, null, 1, "seed-user-9" },
                    { 17, "/images/defaults/members/member2.png", 9, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6202), null, 0, null, true, false, null, 0, "seed-user-9" },
                    { 18, "/images/defaults/members/member2.png", 9, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6205), null, 1, null, true, false, null, 1, "seed-user-10" },
                    { 19, "/images/defaults/members/member1.png", 10, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6208), null, 3, null, true, false, null, 0, "seed-user-10" },
                    { 20, "/images/defaults/members/member3.png", 10, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(6211), null, 2, null, true, false, null, 1, "seed-user-1" }
                });

            migrationBuilder.InsertData(
                table: "Setlists",
                columns: new[] { "Id", "BandId", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "RehearsalDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2957), null, null, "Rehearsal Set", null },
                    { 2, 1, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2964), null, null, "Live Show", null },
                    { 3, 2, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2966), null, null, "Practice Night", null },
                    { 4, 3, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2968), null, null, "Festival Set", null },
                    { 5, 4, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2970), null, null, "Warmup", null },
                    { 6, 5, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2973), null, null, "Main Set", null },
                    { 7, 6, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2975), null, null, "Encore Set", null },
                    { 8, 7, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2977), null, null, "Acoustic", null },
                    { 9, 8, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2979), null, null, "Heavy Set", null },
                    { 10, 9, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2982), null, null, "Chill Set", null },
                    { 11, 10, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2983), null, null, "Night Session", null },
                    { 12, 11, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2985), null, null, "Club Gig", null },
                    { 13, 12, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2987), null, null, "Studio Test", null },
                    { 14, 13, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2989), null, null, "Jam Session", null },
                    { 15, 14, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2991), null, null, "Road Trip", null },
                    { 16, 15, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2993), null, null, "Basement", null },
                    { 17, 16, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2994), null, null, "Open Air", null },
                    { 18, 17, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2997), null, null, "Late Show", null },
                    { 19, 18, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(2999), null, null, "Soundcheck", null },
                    { 20, 19, new DateTime(2026, 4, 7, 9, 59, 22, 942, DateTimeKind.Utc).AddTicks(3001), null, null, "Final Show", null }
                });

            migrationBuilder.InsertData(
                table: "Rehearsals",
                columns: new[] { "Id", "BandId", "CreatedOn", "DeletedOn", "EndRehearsal", "ModifiedOn", "Name", "Notes", "SetlistId", "StartRehearsal" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(8845), null, new DateTime(2026, 5, 20, 17, 0, 0, 0, DateTimeKind.Utc), null, "Evening Practice", null, 1, new DateTime(2026, 5, 20, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(8872), null, new DateTime(2026, 5, 21, 14, 0, 0, 0, DateTimeKind.Utc), null, "Studio Jam", null, 3, new DateTime(2026, 5, 21, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(8904), null, new DateTime(2026, 5, 22, 9, 0, 0, 0, DateTimeKind.Utc), null, "Sound Check", null, 4, new DateTime(2026, 5, 22, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(8920), null, new DateTime(2026, 5, 23, 18, 0, 0, 0, DateTimeKind.Utc), null, "Full Band", null, 5, new DateTime(2026, 5, 23, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(8947), null, new DateTime(2026, 5, 24, 9, 0, 0, 0, DateTimeKind.Utc), null, "Warmup", null, 6, new DateTime(2026, 5, 24, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(8961), null, new DateTime(2026, 5, 25, 21, 0, 0, 0, DateTimeKind.Utc), null, "Late Night", null, 7, new DateTime(2026, 5, 25, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 7, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(8974), null, new DateTime(2026, 5, 26, 15, 0, 0, 0, DateTimeKind.Utc), null, "Groove Session", null, 8, new DateTime(2026, 5, 26, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 8, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(9010), null, new DateTime(2026, 5, 27, 12, 0, 0, 0, DateTimeKind.Utc), null, "Drum Focus", null, 9, new DateTime(2026, 5, 27, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 9, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(9023), null, new DateTime(2026, 5, 28, 16, 0, 0, 0, DateTimeKind.Utc), null, "Vocal Practice", null, 10, new DateTime(2026, 5, 28, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 10, new DateTime(2026, 4, 7, 9, 59, 22, 941, DateTimeKind.Utc).AddTicks(9039), null, new DateTime(2026, 5, 29, 16, 0, 0, 0, DateTimeKind.Utc), null, "Stage Run", null, 11, new DateTime(2026, 5, 29, 13, 0, 0, 0, DateTimeKind.Utc) }
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
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
