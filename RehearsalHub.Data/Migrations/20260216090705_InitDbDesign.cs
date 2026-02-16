using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RehearsalHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDbDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "de305d54-75b4-4311-81d9-7ed39190224b", 0, "45c9cace-138e-46d1-9195-28e6f7593f3a", new DateTime(2026, 2, 16, 9, 7, 3, 994, DateTimeKind.Utc).AddTicks(8447), null, "admin@rehearsalhub.com", true, false, null, null, "ADMIN@REHEARSALHUB.COM", "ADMIN@REHEARSALHUB.COM", "AQAAAAIAAYagAAAAEDyfIfPwTSWbl9qKVvSz5+sT0hFZaVAMl6bayawA7Tk/XaI9rz9DAL+x+Moanw6i8w==", null, false, "/images/defaults/users/user1.png", "939c0540-025c-43f1-9b63-938804008272", false, "admin@rehearsalhub.com" },
                    { "seed-user-1", 0, "8c55eb00-81cf-4548-9284-99af889754a9", new DateTime(2026, 2, 16, 9, 7, 4, 79, DateTimeKind.Utc).AddTicks(2728), null, "rockstar@test.com", true, false, null, null, "ROCKSTAR@TEST.COM", "ROCKSTAR@TEST.COM", "AQAAAAIAAYagAAAAEJUpkYsSgFaX6yCpz1VxA4U/zLzEHQAdWJ0FFFcT17LkzNcWVcWwvScW+TB6+A1zMQ==", null, false, "/images/defaults/users/user2.png", "59846067-8896-4874-9160-5582f3c306d1", false, "rockstar@test.com" },
                    { "seed-user-10", 0, "b59cd028-e930-434e-8fdb-50b8b4691064", new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(233), null, "garage@test.com", true, false, null, null, "GARAGE@TEST.COM", "GARAGE@TEST.COM", "AQAAAAIAAYagAAAAEK/5y46IX35lO9+XMSE4eJP5XZnx+UEwmGgpustt89ebIhaWNJxugDKfHcxUKPt0Uw==", null, false, "/images/defaults/users/user2.png", "b05826c1-3a95-474c-aaff-421462acf61e", false, "garage@test.com" },
                    { "seed-user-2", 0, "c873e88c-d596-4ac9-a815-b63840618dc1", new DateTime(2026, 2, 16, 9, 7, 4, 160, DateTimeKind.Utc).AddTicks(1292), null, "metalhead@test.com", true, false, null, null, "METALHEAD@TEST.COM", "METALHEAD@TEST.COM", "AQAAAAIAAYagAAAAEGhUjHGjCUDUp8bEexru0dBBQL+D74IW3UkLHcBWasF3a2AzD6FdqIIzPbmLHlCurA==", null, false, "/images/defaults/users/user1.png", "f4c9448a-6f4e-4f0e-9180-2a86d2358899", false, "metalhead@test.com" },
                    { "seed-user-3", 0, "7ebae486-31ea-4adf-be82-cad68f2cc2af", new DateTime(2026, 2, 16, 9, 7, 4, 238, DateTimeKind.Utc).AddTicks(8367), null, "jazzman@test.com", true, false, null, null, "JAZZMAN@TEST.COM", "JAZZMAN@TEST.COM", "AQAAAAIAAYagAAAAEP5AhvrkPISguGfbLTy/Jofv8qhFqeWKDXk/b7Rfc8IBK0zw/+6d6yE6xbgwgEfQkQ==", null, false, "/images/defaults/users/user1.png", "788019a3-5c56-4b8c-8f96-339832679f22", false, "jazzman@test.com" },
                    { "seed-user-4", 0, "dc1e8bf9-3f25-406d-853f-16e38dcec535", new DateTime(2026, 2, 16, 9, 7, 4, 322, DateTimeKind.Utc).AddTicks(4076), null, "bluesman@test.com", true, false, null, null, "BLUESMAN@TEST.COM", "BLUESMAN@TEST.COM", "AQAAAAIAAYagAAAAEC9EN+2ghVo0kT0ii+ncXkBZRQZYppokutdnUBXUcI0XznKZ3wLS5wsnMgQ2XVJjPw==", null, false, "/images/defaults/users/user2.png", "6a264f32-d15e-4a88-95ad-7c02c14bdd57", false, "bluesman@test.com" },
                    { "seed-user-5", 0, "89ba3aad-9217-4c1f-8f02-069e468ecd10", new DateTime(2026, 2, 16, 9, 7, 4, 404, DateTimeKind.Utc).AddTicks(3483), null, "funky@test.com", true, false, null, null, "FUNKY@TEST.COM", "FUNKY@TEST.COM", "AQAAAAIAAYagAAAAEAsPRzutkY5jNyPNvvbMNmMPo39DQygp8bb5CUqGGD0H3BHu0Z1v1ws8A4bKz0a3bg==", null, false, "/images/defaults/users/user1.png", "337685f6-ebe2-4675-9154-dfaf5041d1db", false, "funky@test.com" },
                    { "seed-user-6", 0, "4908ae1b-5b5c-4429-a964-71f2eb167b26", new DateTime(2026, 2, 16, 9, 7, 4, 491, DateTimeKind.Utc).AddTicks(205), null, "hiphop@test.com", true, false, null, null, "HIPHOP@TEST.COM", "HIPHOP@TEST.COM", "AQAAAAIAAYagAAAAEDMCJQXE3734dSho8RGkOKAit8nzf37iGlCySaitnZfraqZ9iFAuU2Z4mpsiRN3GUw==", null, false, "/images/defaults/users/user2.png", "45d1e391-12f0-4b6d-a7dd-bb133359ebfb", false, "hiphop@test.com" },
                    { "seed-user-7", 0, "2c626eb3-8711-470d-bca8-155443cdbb26", new DateTime(2026, 2, 16, 9, 7, 4, 574, DateTimeKind.Utc).AddTicks(8494), null, "electro@test.com", true, false, null, null, "ELECTRO@TEST.COM", "ELECTRO@TEST.COM", "AQAAAAIAAYagAAAAEPL3e/jdWkOSJ3AYStn39Y5zCfX8tAgjeAg0QTq4BbKUIE7QwZCzUOTGektqdw9n+g==", null, false, "/images/defaults/users/user2.png", "ff81f31c-fafb-4d93-9afd-103045c3b709", false, "electro@test.com" },
                    { "seed-user-8", 0, "acb396a4-8eaa-4944-89a4-4cde44be73b3", new DateTime(2026, 2, 16, 9, 7, 4, 654, DateTimeKind.Utc).AddTicks(5270), null, "popstar@test.com", true, false, null, null, "POPSTAR@TEST.COM", "POPSTAR@TEST.COM", "AQAAAAIAAYagAAAAENm0VngeRGLSgx+qBOvTdWkkKRZH1i2GoIRWDdVrI2UyNXnWOesh8TwfZFnhagQaWQ==", null, false, "/images/defaults/users/user2.png", "2d5cf7e4-90ac-403f-ae3a-b81b0ed8a0a3", false, "popstar@test.com" },
                    { "seed-user-9", 0, "2b3632a2-8b42-42d7-a6c1-167485417302", new DateTime(2026, 2, 16, 9, 7, 4, 738, DateTimeKind.Utc).AddTicks(2605), null, "soul@test.com", true, false, null, null, "SOUL@TEST.COM", "SOUL@TEST.COM", "AQAAAAIAAYagAAAAEK08i2H4QrRh4JHcQaF2bDpZT2mpS8mDgOqf3A+auynIQvCY6NrTjqLzwbqeAqT6fw==", null, false, "/images/defaults/users/user3.png", "05d51a4a-95c5-4ded-8b85-2a08f660e9ba", false, "soul@test.com" }
                });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Genre", "ImageUrl", "ModifiedOn", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9106), null, 1, "/images/defaults/bands/band3.png", null, "RockStars", "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 2, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9169), null, 2, "/images/defaults/bands/band2.png", null, "MetalHeads", "seed-user-2" },
                    { 3, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9173), null, 4, "/images/defaults/bands/band3.png", null, "Jazz Collective", "seed-user-3" },
                    { 4, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9177), null, 5, "/images/defaults/bands/band1.png", null, "Blues Brothers", "seed-user-4" },
                    { 5, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9180), null, 6, "/images/defaults/bands/band3.png", null, "Funk Factory", "seed-user-5" },
                    { 6, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9201), null, 7, "/images/defaults/bands/band1.png", null, "Urban Flow", "seed-user-6" },
                    { 7, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9204), null, 8, "/images/defaults/bands/band3.png", null, "ElectroWave", "seed-user-7" },
                    { 8, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9214), null, 3, "/images/defaults/bands/band1.png", null, "Pop Squad", "seed-user-8" },
                    { 9, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9219), null, 2, "/images/defaults/bands/band2.png", null, "Heavy Unit", "seed-user-9" },
                    { 10, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9225), null, 1, "/images/defaults/bands/band3.png", null, "Alternative Vibes", "seed-user-10" },
                    { 11, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9229), null, 6, "/images/defaults/bands/band3.png", null, "Soul Train", "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 12, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9232), null, 4, "/images/defaults/bands/band3.png", null, "Night Jam", "seed-user-2" },
                    { 13, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9236), null, 1, "/images/defaults/bands/band1.png", null, "Garage Noise", "seed-user-3" },
                    { 14, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9240), null, 2, "/images/defaults/bands/band2.png", null, "Dark Riffs", "seed-user-4" },
                    { 15, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9243), null, 5, "/images/defaults/bands/band3.png", null, "Smooth Tones", "seed-user-5" },
                    { 16, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9247), null, 1, "/images/defaults/bands/band2.png", null, "Stage Kings", "seed-user-6" },
                    { 17, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9250), null, 6, "/images/defaults/bands/band1.png", null, "Groove Lab", "seed-user-7" },
                    { 18, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9342), null, 7, "/images/defaults/bands/band2.png", null, "Beat Makers", "seed-user-8" },
                    { 19, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9346), null, 8, "/images/defaults/bands/band2.png", null, "Synth Storm", "seed-user-9" },
                    { 20, new DateTime(2026, 2, 16, 9, 7, 4, 817, DateTimeKind.Utc).AddTicks(9349), null, 3, "/images/defaults/bands/band1.png", null, "Pop Nation", "seed-user-10" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "CreatedOn", "CreatorId", "DeletedOn", "Duration", "Genre", "IsPrivate", "ModifiedOn", "MusicalKey", "OwnerBandId", "Tempo", "Title" },
                values: new object[,]
                {
                    { 1, "AC/DC", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2413), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 1, false, null, 0, null, 94, "Back in Black" },
                    { 2, "Guns N' Roses", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2421), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:56", 1, false, null, 8, null, 125, "Sweet Child O' Mine" },
                    { 3, "Led Zeppelin", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2424), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:02", 1, false, null, 19, null, 82, "Stairway to Heaven" },
                    { 4, "Pink Floyd", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2427), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:22", 1, false, null, 22, null, 65, "Comfortably Numb" },
                    { 5, "Eagles", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2429), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:30", 1, false, null, 22, null, 74, "Hotel California" },
                    { 6, "Deep Purple", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2433), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:40", 1, false, null, 35, null, 114, "Smoke on the Water" },
                    { 7, "Aerosmith", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2436), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:28", 1, false, null, 32, null, 80, "Dream On" },
                    { 8, "Bon Jovi", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2438), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:09", 1, false, null, 30, null, 123, "Livin' on a Prayer" },
                    { 9, "Red Hot Chili Peppers", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2441), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:24", 1, false, null, 11, null, 85, "Under the Bridge" },
                    { 10, "Nirvana", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2444), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:01", 1, false, null, 32, null, 117, "Smells Like Teen Spirit" },
                    { 11, "Queen", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2486), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:35", 1, false, null, 30, null, 110, "Another One Bites the Dust" },
                    { 12, "Led Zeppelin", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2489), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:34", 1, false, null, 11, null, 89, "Whole Lotta Love" },
                    { 13, "AC/DC", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2491), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:28", 1, false, null, 0, null, 116, "Highway to Hell" },
                    { 14, "Oasis", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2494), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:18", 1, false, null, 32, null, 87, "Wonderwall" },
                    { 15, "The Rolling Stones", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2496), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:22", 1, false, null, 30, null, 159, "Paint It Black" },
                    { 16, "Led Zeppelin", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2499), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 1, false, null, 0, null, 82, "Black Dog" },
                    { 17, "The Rolling Stones", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2501), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:33", 1, false, null, 13, null, 122, "Start Me Up" },
                    { 18, "Queen", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2505), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:55", 1, false, null, 4, null, 72, "Bohemian Rhapsody" },
                    { 19, "Fleetwood Mac", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2508), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:38", 1, false, null, 13, null, 135, "Go Your Own Way" },
                    { 20, "Van Halen", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2510), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:01", 1, false, null, 5, null, 130, "Jump" },
                    { 21, "The Police", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2513), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:12", 1, false, null, 35, null, 132, "Roxanne" },
                    { 22, "Bryan Adams", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2515), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:35", 1, false, null, 8, null, 139, "Summer of '69" },
                    { 23, "Survivor", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2519), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:04", 1, false, null, 24, null, 109, "Eye of the Tiger" },
                    { 24, "The Clash", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2521), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:06", 1, false, null, 8, null, 113, "Should I Stay or Should I Go" },
                    { 25, "The Clash", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2524), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:20", 1, false, null, 30, null, 134, "London Calling" },
                    { 26, "Pink Floyd", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2527), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:34", 1, false, null, 16, null, 60, "Wish You Were Here" },
                    { 27, "Bruce Springsteen", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2529), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:31", 1, false, null, 11, null, 148, "Born to Run" },
                    { 28, "The Police", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2532), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:13", 1, false, null, 0, null, 117, "Every Breath You Take" },
                    { 29, "U2", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2535), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 1, false, null, 8, null, 110, "With or Without You" },
                    { 30, "R.E.M.", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2538), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:28", 1, false, null, 19, null, 125, "Losing My Religion" },
                    { 31, "Dire Straits", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2545), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:48", 1, false, null, 27, null, 148, "Sultans of Swing" },
                    { 32, "Radiohead", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2547), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:23", 1, false, null, 35, null, 82, "Paranoid Android" },
                    { 33, "Radiohead", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2550), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:59", 1, false, null, 16, null, 92, "Creep" },
                    { 34, "The White Stripes", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2553), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:51", 1, false, null, 30, null, 124, "Seven Nation Army" },
                    { 35, "Muse", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2556), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:40", 1, false, null, 22, null, 136, "Plug In Baby" },
                    { 36, "Foo Fighters", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2559), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 1, false, null, 3, null, 130, "Best of You" },
                    { 37, "Foo Fighters", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2561), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:10", 1, false, null, 8, null, 158, "Everlong" },
                    { 38, "Linkin Park", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2564), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:36", 1, false, null, 30, null, 105, "In the End" },
                    { 39, "The Cranberries", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2566), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:06", 1, false, null, 30, null, 84, "Zombie" },
                    { 40, "ZZ Top", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2568), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:13", 1, false, null, 5, null, 125, "Sharp Dressed Man" },
                    { 41, "Metallica", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2571), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:35", 2, false, null, 30, null, 212, "Master of Puppets" },
                    { 42, "Black Sabbath", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2574), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:48", 2, false, null, 30, null, 163, "Paranoid" },
                    { 43, "Iron Maiden", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2576), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:12", 2, false, null, 30, null, 160, "The Trooper" },
                    { 44, "Slayer", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2665), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 2, false, null, 12, null, 210, "Raining Blood" },
                    { 45, "Megadeth", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2669), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:32", 2, false, null, 30, null, 188, "Holy Wars" },
                    { 46, "Judas Priest", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2672), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:06", 2, false, null, 30, null, 209, "Painkiller" },
                    { 47, "Pantera", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2674), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:14", 2, false, null, 8, null, 92, "Walk" },
                    { 48, "Rage Against the Machine", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2677), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:51", 2, false, null, 32, null, 89, "Bulls on Parade" },
                    { 49, "Rammstein", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2679), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:54", 2, false, null, 30, null, 125, "Du Hast" },
                    { 50, "System of a Down", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2682), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:30", 2, false, null, 35, null, 127, "Chop Suey!" },
                    { 51, "Metallica", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2684), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:31", 2, false, null, 30, null, 123, "Enter Sandman" },
                    { 52, "Iron Maiden", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2687), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:11", 2, false, null, 30, null, 171, "Hallowed Be Thy Name" },
                    { 53, "Motörhead", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2690), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 2, false, null, 12, null, 140, "Ace of Spades" },
                    { 54, "Megadeth", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2693), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:02", 2, false, null, 30, null, 140, "Symphony of Destruction" },
                    { 55, "Pantera", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2695), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:03", 2, false, null, 30, null, 114, "Cowboys from Hell" },
                    { 56, "Slipknot", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2702), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:44", 2, false, null, 0, null, 135, "Psychosocial" },
                    { 57, "Tool", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2705), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:47", 2, false, null, 27, null, 107, "Schism" },
                    { 58, "Iron Maiden", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2708), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:18", 2, false, null, 30, null, 110, "Fear of the Dark" },
                    { 59, "Type O Negative", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2710), "de305d54-75b4-4311-81d9-7ed39190224b", null, "11:15", 2, false, null, 3, null, 95, "Black No. 1" },
                    { 60, "System of a Down", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2713), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:39", 2, false, null, 24, null, 95, "Toxicity" },
                    { 61, "Sepultura", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2715), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:32", 2, false, null, 3, null, 124, "Roots Bloody Roots" },
                    { 62, "Ozzy Osbourne", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2718), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:56", 2, false, null, 0, null, 138, "Crazy Train" },
                    { 63, "Dio", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2720), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 2, false, null, 19, null, 114, "Rainbow in the Dark" },
                    { 64, "Judas Priest", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2723), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:35", 2, false, null, 19, null, 163, "Breaking the Law" },
                    { 65, "Anthrax", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2725), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:59", 2, false, null, 11, null, 184, "Caught in a Mosh" },
                    { 66, "Korn", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2729), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:19", 2, false, null, 0, null, 97, "Blind" },
                    { 67, "Korn", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2732), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 2, false, null, 22, null, 112, "Freak on a Leash" },
                    { 68, "Rammstein", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2734), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:24", 2, false, null, 27, null, 95, "Engel" },
                    { 69, "Rob Zombie", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2737), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:42", 2, false, null, 30, null, 125, "Dragula" },
                    { 70, "Rage Against the Machine", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2741), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:14", 2, false, null, 8, null, 82, "Killing in the Name" },
                    { 71, "Metallica", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2743), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:28", 2, false, null, 30, null, 46, "Nothing Else Matters" },
                    { 72, "Metallica", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2746), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:27", 2, false, null, 22, null, 103, "One" },
                    { 73, "Iron Maiden", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2748), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:07", 2, false, null, 30, null, 154, "Wasted Years" },
                    { 74, "Avenged Sevenfold", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2751), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:16", 2, false, null, 27, null, 130, "Nightmare" },
                    { 75, "Avenged Sevenfold", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2753), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:13", 2, false, null, 27, null, 125, "Bat Country" },
                    { 76, "Avenged Sevenfold", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2756), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:00", 2, false, null, 27, null, 145, "A Little Piece of Heaven" },
                    { 77, "Papa Roach", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2814), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:19", 2, false, null, 30, null, 91, "Last Resort" },
                    { 78, "Disturbed", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2819), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:05", 2, false, null, 9, null, 174, "Stricken" },
                    { 79, "Disturbed", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2822), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:38", 2, false, null, 30, null, 90, "Down with the Sickness" },
                    { 80, "System of a Down", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2824), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:15", 2, false, null, 24, null, 105, "B.Y.O.B." },
                    { 81, "B.B. King", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2827), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:24", 5, false, null, 22, null, 90, "The Thrill Is Gone" },
                    { 82, "Stevie Ray Vaughan", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2829), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:39", 5, false, null, 11, null, 126, "Pride and Joy" },
                    { 83, "Eric Clapton", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2832), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:14", 5, false, null, 0, null, 110, "Crossroads" },
                    { 84, "Muddy Waters", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2834), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 5, false, null, 0, null, 76, "Hoochie Coochie Man" },
                    { 85, "Jimi Hendrix", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2837), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:50", 5, false, null, 3, null, 66, "Red House" },
                    { 86, "Albert King", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2839), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:47", 5, false, null, 9, null, 88, "Born Under a Bad Sign" },
                    { 87, "Robert Johnson", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2842), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:59", 5, false, null, 11, null, 118, "Sweet Home Chicago" },
                    { 88, "Howlin' Wolf", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2845), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:07", 5, false, null, 11, null, 145, "Smokestack Lightnin'" },
                    { 89, "Stevie Ray Vaughan", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2848), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:21", 5, false, null, 16, null, 60, "Texas Flood" },
                    { 90, "Elmore James", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2850), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:45", 5, false, null, 8, null, 100, "Dust My Broom" },
                    { 91, "Gary Moore", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2853), "de305d54-75b4-4311-81d9-7ed39190224b", null, "06:10", 5, false, null, 19, null, 52, "Still Got the Blues" },
                    { 92, "John Lee Hooker", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2855), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:29", 5, false, null, 11, null, 168, "Boom Boom" },
                    { 93, "Gary Moore", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2858), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:58", 5, false, null, 24, null, 64, "Midnight Blues" },
                    { 94, "Buddy Guy", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2860), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:36", 5, false, null, 3, null, 138, "Messin' with the Kid" },
                    { 95, "T-Bone Walker", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2863), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:02", 5, false, null, 16, null, 64, "Stormy Monday" },
                    { 96, "Elmore James", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2865), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:43", 5, false, null, 5, null, 62, "The Sky Is Crying" },
                    { 97, "Muddy Waters", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2868), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:23", 5, false, null, 0, null, 72, "Mannish Boy" },
                    { 98, "Willie Dixon", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2871), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:49", 5, false, null, 0, null, 76, "I'm Your Hoochie Coochie Man" },
                    { 99, "Freddie King", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2874), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:38", 5, false, null, 11, null, 140, "Hide Away" },
                    { 100, "Big Brother & The Holding Company", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2877), "de305d54-75b4-4311-81d9-7ed39190224b", null, "09:28", 5, false, null, 35, null, 62, "Ball and Chain" },
                    { 101, "Derek and the Dominos", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2879), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:02", 5, false, null, 5, null, 80, "Bell Bottom Blues" },
                    { 102, "Joe Bonamassa", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2883), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:20", 5, false, null, 5, null, 60, "Blues Deluxe" },
                    { 103, "Joe Bonamassa", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2885), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:13", 5, false, null, 35, null, 58, "Sloe Gin" },
                    { 104, "Stevie Ray Vaughan", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2888), "de305d54-75b4-4311-81d9-7ed39190224b", null, "09:11", 5, false, null, 22, null, 58, "Tin Pan Alley" },
                    { 105, "Stevie Ray Vaughan", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2890), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:28", 5, false, null, 0, null, 88, "Life by the Drop" },
                    { 106, "Led Zeppelin", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2892), "de305d54-75b4-4311-81d9-7ed39190224b", null, "07:24", 5, false, null, 24, null, 44, "Since I've Been Loving You" },
                    { 107, "Buddy Guy", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2895), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:29", 5, false, null, 19, null, 105, "Damn Right, I've Got the Blues" },
                    { 108, "Buddy Guy", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2898), "de305d54-75b4-4311-81d9-7ed39190224b", null, "08:23", 5, false, null, 0, null, 63, "Five Long Years" },
                    { 109, "Howlin' Wolf", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2901), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:22", 5, false, null, 16, null, 72, "Little Red Rooster" },
                    { 110, "Etta James", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2903), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:07", 5, false, null, 16, null, 110, "I Just Want to Make Love to You" },
                    { 111, "Etta James", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2906), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:35", 5, false, null, 0, null, 68, "I'd Rather Go Blind" },
                    { 112, "Otis Rush", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2908), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:06", 5, false, null, 0, null, 60, "I Can't Quit You Baby" },
                    { 113, "Sonny Boy Williamson II", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2910), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:08", 5, false, null, 19, null, 114, "Help Me" },
                    { 114, "Howlin' Wolf", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2913), "de305d54-75b4-4311-81d9-7ed39190224b", null, "02:53", 5, false, null, 0, null, 80, "Evil" },
                    { 115, "Koko Taylor", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2953), "de305d54-75b4-4311-81d9-7ed39190224b", null, "03:00", 5, false, null, 0, null, 110, "Wang Dang Doodle" },
                    { 116, "Albert King", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2956), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:18", 5, false, null, 16, null, 65, "Call It Stormy Monday" },
                    { 117, "Cream", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2958), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:10", 5, false, null, 8, null, 115, "Sunshine of Your Love" },
                    { 118, "Joe Bonamassa", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2961), "de305d54-75b4-4311-81d9-7ed39190224b", null, "05:07", 5, false, null, 16, null, 145, "Bridge to Better Days" },
                    { 119, "John Mayer", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2963), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:05", 5, false, null, 16, null, 62, "Gravity" },
                    { 120, "The Doors", new DateTime(2026, 2, 16, 9, 7, 4, 819, DateTimeKind.Utc).AddTicks(2966), "de305d54-75b4-4311-81d9-7ed39190224b", null, "04:04", 5, false, null, 11, null, 124, "Roadhouse Blues" }
                });

            migrationBuilder.InsertData(
                table: "BandMembers",
                columns: new[] { "Id", "AvatarUrl", "BandId", "CreatedOn", "DeletedOn", "Instrument", "InvitationToken", "IsConfirmed", "IsDeletedInvitation", "ModifiedOn", "Role", "UserId" },
                values: new object[,]
                {
                    { 1, "/images/defaults/members/member3.png", 1, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3063), null, 0, null, true, false, null, 0, "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 2, "/images/defaults/members/member3.png", 1, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3072), null, 1, null, true, false, null, 1, "seed-user-2" },
                    { 3, "/images/defaults/members/member3.png", 2, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3075), null, 0, null, true, false, null, 0, "seed-user-2" },
                    { 4, "/images/defaults/members/member1.png", 2, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3078), null, 2, null, true, false, null, 1, "seed-user-3" },
                    { 5, "/images/defaults/members/member3.png", 13, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3081), null, 3, null, true, false, null, 0, "seed-user-3" },
                    { 6, "/images/defaults/members/member2.png", 3, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3085), null, 5, null, true, false, null, 1, "seed-user-4" },
                    { 7, "/images/defaults/members/member1.png", 4, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3088), null, 0, null, true, false, null, 0, "seed-user-4" },
                    { 8, "/images/defaults/members/member1.png", 4, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3091), null, 1, null, true, false, null, 1, "seed-user-5" },
                    { 9, "/images/defaults/members/member2.png", 5, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3094), null, 4, null, true, false, null, 0, "seed-user-5" },
                    { 10, "/images/defaults/members/member3.png", 5, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3098), null, 2, null, true, false, null, 1, "seed-user-6" },
                    { 11, "/images/defaults/members/member3.png", 6, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3101), null, 3, null, true, false, null, 0, "de305d54-75b4-4311-81d9-7ed39190224b" },
                    { 12, "/images/defaults/members/member1.png", 6, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3104), null, 0, null, true, false, null, 1, "seed-user-7" },
                    { 13, "/images/defaults/members/member3.png", 7, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3106), null, 7, null, true, false, null, 0, "seed-user-7" },
                    { 14, "/images/defaults/members/member2.png", 7, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3109), null, 4, null, true, false, null, 1, "seed-user-8" },
                    { 15, "/images/defaults/members/member2.png", 8, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3112), null, 5, null, true, false, null, 0, "seed-user-8" },
                    { 16, "/images/defaults/members/member2.png", 8, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3115), null, 2, null, true, false, null, 1, "seed-user-9" },
                    { 17, "/images/defaults/members/member2.png", 9, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3118), null, 0, null, true, false, null, 0, "seed-user-9" },
                    { 18, "/images/defaults/members/member1.png", 9, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3172), null, 1, null, true, false, null, 1, "seed-user-10" },
                    { 19, "/images/defaults/members/member3.png", 10, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3175), null, 3, null, true, false, null, 0, "seed-user-10" },
                    { 20, "/images/defaults/members/member2.png", 10, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(3178), null, 2, null, true, false, null, 1, "seed-user-1" }
                });

            migrationBuilder.InsertData(
                table: "Setlists",
                columns: new[] { "Id", "BandId", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "RehearsalDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8679), null, null, "Rehearsal Set", null },
                    { 2, 1, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8686), null, null, "Live Show", null },
                    { 3, 2, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8688), null, null, "Practice Night", null },
                    { 4, 3, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8690), null, null, "Festival Set", null },
                    { 5, 4, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8692), null, null, "Warmup", null },
                    { 6, 5, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8695), null, null, "Main Set", null },
                    { 7, 6, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8697), null, null, "Encore Set", null },
                    { 8, 7, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8699), null, null, "Acoustic", null },
                    { 9, 8, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8701), null, null, "Heavy Set", null },
                    { 10, 9, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8703), null, null, "Chill Set", null },
                    { 11, 10, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8705), null, null, "Night Session", null },
                    { 12, 11, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8707), null, null, "Club Gig", null },
                    { 13, 12, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8709), null, null, "Studio Test", null },
                    { 14, 13, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8711), null, null, "Jam Session", null },
                    { 15, 14, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8713), null, null, "Road Trip", null },
                    { 16, 15, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8715), null, null, "Basement", null },
                    { 17, 16, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8768), null, null, "Open Air", null },
                    { 18, 17, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8771), null, null, "Late Show", null },
                    { 19, 18, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8773), null, null, "Soundcheck", null },
                    { 20, 19, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(8775), null, null, "Final Show", null }
                });

            migrationBuilder.InsertData(
                table: "Rehearsals",
                columns: new[] { "Id", "BandId", "CreatedOn", "DeletedOn", "EndRehearsal", "ModifiedOn", "Name", "Notes", "SetlistId", "StartRehearsal" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(5994), null, new DateTime(2026, 5, 20, 17, 0, 0, 0, DateTimeKind.Utc), null, "Evening Practice", null, 1, new DateTime(2026, 5, 20, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6023), null, new DateTime(2026, 5, 21, 14, 0, 0, 0, DateTimeKind.Utc), null, "Studio Jam", null, 3, new DateTime(2026, 5, 21, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6055), null, new DateTime(2026, 5, 22, 9, 0, 0, 0, DateTimeKind.Utc), null, "Sound Check", null, 4, new DateTime(2026, 5, 22, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6072), null, new DateTime(2026, 5, 23, 18, 0, 0, 0, DateTimeKind.Utc), null, "Full Band", null, 5, new DateTime(2026, 5, 23, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6098), null, new DateTime(2026, 5, 24, 9, 0, 0, 0, DateTimeKind.Utc), null, "Warmup", null, 6, new DateTime(2026, 5, 24, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6114), null, new DateTime(2026, 5, 25, 21, 0, 0, 0, DateTimeKind.Utc), null, "Late Night", null, 7, new DateTime(2026, 5, 25, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 7, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6127), null, new DateTime(2026, 5, 26, 15, 0, 0, 0, DateTimeKind.Utc), null, "Groove Session", null, 8, new DateTime(2026, 5, 26, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 8, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6168), null, new DateTime(2026, 5, 27, 12, 0, 0, 0, DateTimeKind.Utc), null, "Drum Focus", null, 9, new DateTime(2026, 5, 27, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 9, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6182), null, new DateTime(2026, 5, 28, 16, 0, 0, 0, DateTimeKind.Utc), null, "Vocal Practice", null, 10, new DateTime(2026, 5, 28, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 10, new DateTime(2026, 2, 16, 9, 7, 4, 818, DateTimeKind.Utc).AddTicks(6200), null, new DateTime(2026, 5, 29, 16, 0, 0, 0, DateTimeKind.Utc), null, "Stage Run", null, 11, new DateTime(2026, 5, 29, 13, 0, 0, 0, DateTimeKind.Utc) }
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
