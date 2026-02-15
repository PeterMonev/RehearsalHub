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
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
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
                    { "seed-user-1", 0, "f813eaf8-d086-48a9-9c20-e9c68685f068", new DateTime(2026, 2, 14, 21, 10, 15, 543, DateTimeKind.Utc).AddTicks(3794), null, "rockstar@test.com", true, false, null, null, "ROCKSTAR@TEST.COM", "ROCKSTAR@TEST.COM", "AQAAAAIAAYagAAAAEA4HINl1c7Dt6v7bAgOPBMOmD4JpJM53WmCCv6gRYUx8erZzdjsFfuQcla8kwH/2AQ==", null, false, "/images/defaults/users/user3.png", "59846067-8896-4874-9160-5582f3c306d1", false, "rockstar@test.com" },
                    { "seed-user-2", 0, "ac538090-5e18-4646-9750-202b1566206b", new DateTime(2026, 2, 14, 21, 10, 15, 644, DateTimeKind.Utc).AddTicks(4324), null, "metalhead@test.com", true, false, null, null, "METALHEAD@TEST.COM", "METALHEAD@TEST.COM", "AQAAAAIAAYagAAAAEK09jgp3JFtbJ7egw2/0g4L4sJ9FGhSTjqGwxVUI6ReckaAl5ae4rIno/bCgsKRNCQ==", null, false, "/images/defaults/users/user2.png", "f4c9448a-6f4e-4f0e-9180-2a86d2358899", false, "metalhead@test.com" },
                    { "seed-user-3", 0, "4f249caf-299b-4255-85dd-f0ace0e8dcf7", new DateTime(2026, 2, 14, 21, 10, 15, 723, DateTimeKind.Utc).AddTicks(9837), null, "jazzman@test.com", true, false, null, null, "JAZZMAN@TEST.COM", "JAZZMAN@TEST.COM", "AQAAAAIAAYagAAAAEP7q9QPHA0x110t6SkyVkj+jT18NXgdpd9nB7NVD4VUC5YAGnE4eARGqu23d+pN1xg==", null, false, "/images/defaults/users/user1.png", "788019a3-5c56-4b8c-8f96-339832679f22", false, "jazzman@test.com" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "CreatedOn", "DeletedOn", "Duration", "Genre", "IsPrivate", "ModifiedOn", "MusicalKey", "OwnerBandId", "Tempo", "Title" },
                values: new object[,]
                {
                    { 1, "AC/DC", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9870), null, "04:15", 1, false, null, 0, null, 94, "Back in Black" },
                    { 2, "Guns N' Roses", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9879), null, "05:56", 1, false, null, 8, null, 125, "Sweet Child O' Mine" },
                    { 3, "Led Zeppelin", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9882), null, "08:02", 1, false, null, 19, null, 82, "Stairway to Heaven" },
                    { 4, "Pink Floyd", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9884), null, "06:22", 1, false, null, 22, null, 65, "Comfortably Numb" },
                    { 5, "Eagles", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9886), null, "06:30", 1, false, null, 22, null, 74, "Hotel California" },
                    { 6, "Deep Purple", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9890), null, "05:40", 1, false, null, 35, null, 114, "Smoke on the Water" },
                    { 7, "Aerosmith", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9892), null, "04:28", 1, false, null, 32, null, 80, "Dream On" },
                    { 8, "Bon Jovi", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9894), null, "04:09", 1, false, null, 30, null, 123, "Livin' on a Prayer" },
                    { 9, "Red Hot Chili Peppers", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9897), null, "04:24", 1, false, null, 11, null, 85, "Under the Bridge" },
                    { 10, "Nirvana", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9900), null, "05:01", 1, false, null, 32, null, 117, "Smells Like Teen Spirit" },
                    { 11, "Queen", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9902), null, "03:35", 1, false, null, 30, null, 110, "Another One Bites the Dust" },
                    { 12, "Led Zeppelin", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9904), null, "05:34", 1, false, null, 11, null, 89, "Whole Lotta Love" },
                    { 13, "AC/DC", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9906), null, "03:28", 1, false, null, 0, null, 116, "Highway to Hell" },
                    { 14, "Oasis", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9909), null, "04:18", 1, false, null, 32, null, 87, "Wonderwall" },
                    { 15, "The Rolling Stones", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9912), null, "03:22", 1, false, null, 30, null, 159, "Paint It Black" },
                    { 16, "Led Zeppelin", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9914), null, "04:56", 1, false, null, 0, null, 82, "Black Dog" },
                    { 17, "The Rolling Stones", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9917), null, "03:33", 1, false, null, 13, null, 122, "Start Me Up" },
                    { 18, "Queen", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9920), null, "05:55", 1, false, null, 4, null, 72, "Bohemian Rhapsody" },
                    { 19, "Fleetwood Mac", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9970), null, "03:38", 1, false, null, 13, null, 135, "Go Your Own Way" },
                    { 20, "Van Halen", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9972), null, "04:01", 1, false, null, 5, null, 130, "Jump" },
                    { 21, "The Police", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9974), null, "03:12", 1, false, null, 35, null, 132, "Roxanne" },
                    { 22, "Bryan Adams", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9977), null, "03:35", 1, false, null, 8, null, 139, "Summer of '69" },
                    { 23, "Survivor", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9979), null, "04:04", 1, false, null, 24, null, 109, "Eye of the Tiger" },
                    { 24, "The Clash", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9981), null, "03:06", 1, false, null, 8, null, 113, "Should I Stay or Should I Go" },
                    { 25, "The Clash", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9984), null, "03:20", 1, false, null, 30, null, 134, "London Calling" },
                    { 26, "Pink Floyd", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9987), null, "05:34", 1, false, null, 16, null, 60, "Wish You Were Here" },
                    { 27, "Bruce Springsteen", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9989), null, "04:31", 1, false, null, 11, null, 148, "Born to Run" },
                    { 28, "The Police", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9991), null, "04:13", 1, false, null, 0, null, 117, "Every Breath You Take" },
                    { 29, "U2", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9994), null, "04:56", 1, false, null, 8, null, 110, "With or Without You" },
                    { 30, "R.E.M.", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9996), null, "04:28", 1, false, null, 19, null, 125, "Losing My Religion" },
                    { 31, "Dire Straits", new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(9998), null, "05:48", 1, false, null, 27, null, 148, "Sultans of Swing" },
                    { 32, "Radiohead", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(1), null, "06:23", 1, false, null, 35, null, 82, "Paranoid Android" },
                    { 33, "Radiohead", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(3), null, "03:59", 1, false, null, 16, null, 92, "Creep" },
                    { 34, "The White Stripes", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(6), null, "03:51", 1, false, null, 30, null, 124, "Seven Nation Army" },
                    { 35, "Muse", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(11), null, "03:40", 1, false, null, 22, null, 136, "Plug In Baby" },
                    { 36, "Foo Fighters", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(14), null, "04:15", 1, false, null, 3, null, 130, "Best of You" },
                    { 37, "Foo Fighters", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(17), null, "04:10", 1, false, null, 8, null, 158, "Everlong" },
                    { 38, "Linkin Park", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(19), null, "03:36", 1, false, null, 30, null, 105, "In the End" },
                    { 39, "The Cranberries", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(22), null, "05:06", 1, false, null, 30, null, 84, "Zombie" },
                    { 40, "ZZ Top", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(24), null, "04:13", 1, false, null, 5, null, 125, "Sharp Dressed Man" },
                    { 41, "Metallica", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(26), null, "08:35", 2, false, null, 30, null, 212, "Master of Puppets" },
                    { 42, "Black Sabbath", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(28), null, "02:48", 2, false, null, 30, null, 163, "Paranoid" },
                    { 43, "Iron Maiden", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(31), null, "04:12", 2, false, null, 30, null, 160, "The Trooper" },
                    { 44, "Slayer", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(33), null, "04:14", 2, false, null, 12, null, 210, "Raining Blood" },
                    { 45, "Megadeth", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(35), null, "06:32", 2, false, null, 30, null, 188, "Holy Wars" },
                    { 46, "Judas Priest", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(37), null, "06:06", 2, false, null, 30, null, 209, "Painkiller" },
                    { 47, "Pantera", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(39), null, "05:14", 2, false, null, 8, null, 92, "Walk" },
                    { 48, "Rage Against the Machine", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(42), null, "03:51", 2, false, null, 32, null, 89, "Bulls on Parade" },
                    { 49, "Rammstein", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(44), null, "03:54", 2, false, null, 30, null, 125, "Du Hast" },
                    { 50, "System of a Down", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(49), null, "03:30", 2, false, null, 35, null, 127, "Chop Suey!" },
                    { 51, "Metallica", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(51), null, "05:31", 2, false, null, 30, null, 123, "Enter Sandman" },
                    { 52, "Iron Maiden", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(53), null, "07:11", 2, false, null, 30, null, 171, "Hallowed Be Thy Name" },
                    { 53, "Motörhead", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(56), null, "02:49", 2, false, null, 12, null, 140, "Ace of Spades" },
                    { 54, "Megadeth", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(58), null, "04:02", 2, false, null, 30, null, 140, "Symphony of Destruction" },
                    { 55, "Pantera", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(60), null, "04:03", 2, false, null, 30, null, 114, "Cowboys from Hell" },
                    { 56, "Slipknot", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(62), null, "04:44", 2, false, null, 0, null, 135, "Psychosocial" },
                    { 57, "Tool", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(96), null, "06:47", 2, false, null, 27, null, 107, "Schism" },
                    { 58, "Iron Maiden", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(98), null, "07:18", 2, false, null, 30, null, 110, "Fear of the Dark" },
                    { 59, "Type O Negative", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(100), null, "11:15", 2, false, null, 3, null, 95, "Black No. 1" },
                    { 60, "System of a Down", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(103), null, "03:39", 2, false, null, 24, null, 95, "Toxicity" },
                    { 61, "Sepultura", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(105), null, "03:32", 2, false, null, 3, null, 124, "Roots Bloody Roots" },
                    { 62, "Ozzy Osbourne", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(108), null, "04:56", 2, false, null, 0, null, 138, "Crazy Train" },
                    { 63, "Dio", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(111), null, "04:14", 2, false, null, 19, null, 114, "Rainbow in the Dark" },
                    { 64, "Judas Priest", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(113), null, "02:35", 2, false, null, 19, null, 163, "Breaking the Law" },
                    { 65, "Anthrax", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(115), null, "04:59", 2, false, null, 11, null, 184, "Caught in a Mosh" },
                    { 66, "Korn", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(118), null, "04:19", 2, false, null, 0, null, 97, "Blind" },
                    { 67, "Korn", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(120), null, "04:15", 2, false, null, 22, null, 112, "Freak on a Leash" },
                    { 68, "Rammstein", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(123), null, "04:24", 2, false, null, 27, null, 95, "Engel" },
                    { 69, "Rob Zombie", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(125), null, "03:42", 2, false, null, 30, null, 125, "Dragula" },
                    { 70, "Rage Against the Machine", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(128), null, "05:14", 2, false, null, 8, null, 82, "Killing in the Name" },
                    { 71, "Metallica", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(130), null, "06:28", 2, false, null, 30, null, 46, "Nothing Else Matters" },
                    { 72, "Metallica", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(132), null, "07:27", 2, false, null, 22, null, 103, "One" },
                    { 73, "Iron Maiden", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(134), null, "05:07", 2, false, null, 30, null, 154, "Wasted Years" },
                    { 74, "Avenged Sevenfold", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(138), null, "06:16", 2, false, null, 27, null, 130, "Nightmare" },
                    { 75, "Avenged Sevenfold", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(140), null, "05:13", 2, false, null, 27, null, 125, "Bat Country" },
                    { 76, "Avenged Sevenfold", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(142), null, "08:00", 2, false, null, 27, null, 145, "A Little Piece of Heaven" },
                    { 77, "Papa Roach", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(144), null, "03:19", 2, false, null, 30, null, 91, "Last Resort" },
                    { 78, "Disturbed", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(147), null, "04:05", 2, false, null, 9, null, 174, "Stricken" },
                    { 79, "Disturbed", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(149), null, "04:38", 2, false, null, 30, null, 90, "Down with the Sickness" },
                    { 80, "System of a Down", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(151), null, "04:15", 2, false, null, 24, null, 105, "B.Y.O.B." },
                    { 81, "B.B. King", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(153), null, "05:24", 5, false, null, 22, null, 90, "The Thrill Is Gone" },
                    { 82, "Stevie Ray Vaughan", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(156), null, "03:39", 5, false, null, 11, null, 126, "Pride and Joy" },
                    { 83, "Eric Clapton", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(158), null, "04:14", 5, false, null, 0, null, 110, "Crossroads" },
                    { 84, "Muddy Waters", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(160), null, "02:49", 5, false, null, 0, null, 76, "Hoochie Coochie Man" },
                    { 85, "Jimi Hendrix", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(162), null, "03:50", 5, false, null, 3, null, 66, "Red House" },
                    { 86, "Albert King", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(166), null, "02:47", 5, false, null, 9, null, 88, "Born Under a Bad Sign" },
                    { 87, "Robert Johnson", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(169), null, "02:59", 5, false, null, 11, null, 118, "Sweet Home Chicago" },
                    { 88, "Howlin' Wolf", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(172), null, "03:07", 5, false, null, 11, null, 145, "Smokestack Lightnin'" },
                    { 89, "Stevie Ray Vaughan", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(174), null, "05:21", 5, false, null, 16, null, 60, "Texas Flood" },
                    { 90, "Elmore James", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(177), null, "02:45", 5, false, null, 8, null, 100, "Dust My Broom" },
                    { 91, "Gary Moore", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(180), null, "06:10", 5, false, null, 19, null, 52, "Still Got the Blues" },
                    { 92, "John Lee Hooker", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(183), null, "02:29", 5, false, null, 11, null, 168, "Boom Boom" },
                    { 93, "Gary Moore", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(217), null, "04:58", 5, false, null, 24, null, 64, "Midnight Blues" },
                    { 94, "Buddy Guy", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(219), null, "02:36", 5, false, null, 3, null, 138, "Messin' with the Kid" },
                    { 95, "T-Bone Walker", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(221), null, "03:02", 5, false, null, 16, null, 64, "Stormy Monday" },
                    { 96, "Elmore James", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(224), null, "02:43", 5, false, null, 5, null, 62, "The Sky Is Crying" },
                    { 97, "Muddy Waters", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(226), null, "05:23", 5, false, null, 0, null, 72, "Mannish Boy" },
                    { 98, "Willie Dixon", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(229), null, "02:49", 5, false, null, 0, null, 76, "I'm Your Hoochie Coochie Man" },
                    { 99, "Freddie King", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(231), null, "02:38", 5, false, null, 11, null, 140, "Hide Away" },
                    { 100, "Big Brother & The Holding Company", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(233), null, "09:28", 5, false, null, 35, null, 62, "Ball and Chain" },
                    { 101, "Derek and the Dominos", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(235), null, "05:02", 5, false, null, 5, null, 80, "Bell Bottom Blues" },
                    { 102, "Joe Bonamassa", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(237), null, "07:20", 5, false, null, 5, null, 60, "Blues Deluxe" },
                    { 103, "Joe Bonamassa", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(240), null, "08:13", 5, false, null, 35, null, 58, "Sloe Gin" },
                    { 104, "Stevie Ray Vaughan", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(242), null, "09:11", 5, false, null, 22, null, 58, "Tin Pan Alley" },
                    { 105, "Stevie Ray Vaughan", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(245), null, "02:28", 5, false, null, 0, null, 88, "Life by the Drop" },
                    { 106, "Led Zeppelin", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(248), null, "07:24", 5, false, null, 24, null, 44, "Since I've Been Loving You" },
                    { 107, "Buddy Guy", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(250), null, "04:29", 5, false, null, 19, null, 105, "Damn Right, I've Got the Blues" },
                    { 108, "Buddy Guy", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(252), null, "08:23", 5, false, null, 0, null, 63, "Five Long Years" },
                    { 109, "Howlin' Wolf", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(255), null, "02:22", 5, false, null, 16, null, 72, "Little Red Rooster" },
                    { 110, "Etta James", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(258), null, "03:07", 5, false, null, 16, null, 110, "I Just Want to Make Love to You" },
                    { 111, "Etta James", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(260), null, "02:35", 5, false, null, 0, null, 68, "I'd Rather Go Blind" },
                    { 112, "Otis Rush", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(263), null, "03:06", 5, false, null, 0, null, 60, "I Can't Quit You Baby" },
                    { 113, "Sonny Boy Williamson II", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(265), null, "03:08", 5, false, null, 19, null, 114, "Help Me" },
                    { 114, "Howlin' Wolf", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(268), null, "02:53", 5, false, null, 0, null, 80, "Evil" },
                    { 115, "Koko Taylor", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(270), null, "03:00", 5, false, null, 0, null, 110, "Wang Dang Doodle" },
                    { 116, "Albert King", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(272), null, "04:18", 5, false, null, 16, null, 65, "Call It Stormy Monday" },
                    { 117, "Cream", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(275), null, "04:10", 5, false, null, 8, null, 115, "Sunshine of Your Love" },
                    { 118, "Joe Bonamassa", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(278), null, "05:07", 5, false, null, 16, null, 145, "Bridge to Better Days" },
                    { 119, "John Mayer", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(284), null, "04:05", 5, false, null, 16, null, 62, "Gravity" },
                    { 120, "The Doors", new DateTime(2026, 2, 14, 21, 10, 15, 726, DateTimeKind.Utc).AddTicks(286), null, "04:04", 5, false, null, 11, null, 124, "Roadhouse Blues" }
                });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Genre", "ImageUrl", "ModifiedOn", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7135), null, 1, "/images/defaults/bands/band2.png", null, "RockStars", "seed-user-1" },
                    { 2, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7154), null, 2, "/images/defaults/bands/band3.png", null, "MetalHeads", "seed-user-2" },
                    { 3, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7161), null, 4, "/images/defaults/bands/band3.png", null, "Jazz Collective", "seed-user-3" },
                    { 4, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7164), null, 5, "/images/defaults/bands/band1.png", null, "Blues Brothers", "seed-user-1" },
                    { 5, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7168), null, 6, "/images/defaults/bands/band2.png", null, "Funk Factory", "seed-user-2" },
                    { 6, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7222), null, 7, "/images/defaults/bands/band1.png", null, "Urban Flow", "seed-user-3" },
                    { 7, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7226), null, 8, "/images/defaults/bands/band1.png", null, "ElectroWave", "seed-user-1" },
                    { 8, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7229), null, 3, "/images/defaults/bands/band1.png", null, "Pop Squad", "seed-user-2" },
                    { 9, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7240), null, 2, "/images/defaults/bands/band3.png", null, "Heavy Unit", "seed-user-3" },
                    { 10, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7245), null, 1, "/images/defaults/bands/band2.png", null, "Alternative Vibes", "seed-user-1" },
                    { 11, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7248), null, 6, "/images/defaults/bands/band2.png", null, "Soul Train", "seed-user-2" },
                    { 12, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7251), null, 4, "/images/defaults/bands/band1.png", null, "Night Jam", "seed-user-3" },
                    { 13, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7255), null, 1, "/images/defaults/bands/band2.png", null, "Garage Noise", "seed-user-1" },
                    { 14, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7279), null, 2, "/images/defaults/bands/band1.png", null, "Dark Riffs", "seed-user-2" },
                    { 15, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7282), null, 5, "/images/defaults/bands/band2.png", null, "Smooth Tones", "seed-user-3" },
                    { 16, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7286), null, 1, "/images/defaults/bands/band2.png", null, "Stage Kings", "seed-user-1" },
                    { 17, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7289), null, 6, "/images/defaults/bands/band2.png", null, "Groove Lab", "seed-user-2" },
                    { 18, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7293), null, 7, "/images/defaults/bands/band1.png", null, "Beat Makers", "seed-user-3" },
                    { 19, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7297), null, 8, "/images/defaults/bands/band1.png", null, "Synth Storm", "seed-user-1" },
                    { 20, new DateTime(2026, 2, 14, 21, 10, 15, 724, DateTimeKind.Utc).AddTicks(7300), null, 3, "/images/defaults/bands/band1.png", null, "Pop Nation", "seed-user-2" }
                });

            migrationBuilder.InsertData(
                table: "BandMembers",
                columns: new[] { "Id", "AvatarUrl", "BandId", "CreatedOn", "DeletedOn", "Instrument", "InvitationToken", "IsConfirmed", "IsDeletedInvitation", "ModifiedOn", "Role", "UserId" },
                values: new object[,]
                {
                    { 1, "/images/defaults/members/member1.png", 1, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(432), null, 0, null, true, false, null, 0, "seed-user-1" },
                    { 2, "/images/defaults/members/member1.png", 1, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(441), null, 1, null, true, false, null, 1, "seed-user-2" },
                    { 3, "/images/defaults/members/member2.png", 1, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(444), null, 2, null, true, false, null, 1, "seed-user-3" },
                    { 4, "/images/defaults/members/member1.png", 2, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(448), null, 0, null, true, false, null, 0, "seed-user-2" },
                    { 5, "/images/defaults/members/member1.png", 2, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(451), null, 1, null, true, false, null, 1, "seed-user-1" },
                    { 6, "/images/defaults/members/member2.png", 2, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(455), null, 4, null, true, false, null, 1, "seed-user-3" },
                    { 7, "/images/defaults/members/member1.png", 3, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(459), null, 3, null, true, false, null, 0, "seed-user-3" },
                    { 8, "/images/defaults/members/member2.png", 3, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(462), null, 5, null, true, false, null, 1, "seed-user-1" },
                    { 9, "/images/defaults/members/member3.png", 3, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(465), null, 2, null, true, false, null, 1, "seed-user-2" },
                    { 10, "/images/defaults/members/member2.png", 4, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(469), null, 0, null, true, false, null, 0, "seed-user-1" },
                    { 11, "/images/defaults/members/member1.png", 4, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(472), null, 1, null, true, false, null, 1, "seed-user-2" },
                    { 12, "/images/defaults/members/member1.png", 4, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(475), null, 3, null, true, false, null, 1, "seed-user-3" },
                    { 13, "/images/defaults/members/member1.png", 5, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(479), null, 0, null, true, false, null, 0, "seed-user-2" },
                    { 14, "/images/defaults/members/member2.png", 5, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(482), null, 2, null, true, false, null, 1, "seed-user-3" },
                    { 15, "/images/defaults/members/member2.png", 5, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(485), null, 4, null, true, false, null, 1, "seed-user-1" },
                    { 16, "/images/defaults/members/member2.png", 6, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(488), null, 3, null, true, false, null, 0, "seed-user-3" },
                    { 17, "/images/defaults/members/member1.png", 6, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(491), null, 0, null, true, false, null, 1, "seed-user-1" },
                    { 18, "/images/defaults/members/member2.png", 6, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(495), null, 1, null, true, false, null, 1, "seed-user-2" },
                    { 19, "/images/defaults/members/member3.png", 7, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(498), null, 7, null, true, false, null, 0, "seed-user-1" },
                    { 20, "/images/defaults/members/member3.png", 7, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(501), null, 2, null, true, false, null, 1, "seed-user-2" }
                });

            migrationBuilder.InsertData(
                table: "Setlists",
                columns: new[] { "Id", "BandId", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "RehearsalDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6143), null, null, "Rehearsal Set", null },
                    { 2, 1, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6151), null, null, "Live Show", null },
                    { 3, 2, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6153), null, null, "Practice Night", null },
                    { 4, 3, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6155), null, null, "Festival Set", null },
                    { 5, 4, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6157), null, null, "Warmup", null },
                    { 6, 5, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6160), null, null, "Main Set", null },
                    { 7, 6, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6162), null, null, "Encore Set", null },
                    { 8, 7, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6164), null, null, "Acoustic", null },
                    { 9, 8, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6166), null, null, "Heavy Set", null },
                    { 10, 9, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6173), null, null, "Chill Set", null },
                    { 11, 10, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6175), null, null, "Night Session", null },
                    { 12, 11, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6177), null, null, "Club Gig", null },
                    { 13, 12, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6179), null, null, "Studio Test", null },
                    { 14, 13, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6180), null, null, "Jam Session", null },
                    { 15, 14, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6182), null, null, "Road Trip", null },
                    { 16, 15, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6184), null, null, "Basement", null },
                    { 17, 16, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6186), null, null, "Open Air", null },
                    { 18, 17, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6189), null, null, "Late Show", null },
                    { 19, 18, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6191), null, null, "Soundcheck", null },
                    { 20, 19, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(6193), null, null, "Final Show", null }
                });

            migrationBuilder.InsertData(
                table: "Rehearsals",
                columns: new[] { "Id", "BandId", "CreatedOn", "DeletedOn", "EndRehearsal", "ModifiedOn", "Name", "Notes", "SetlistId", "StartRehearsal" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3073), null, new DateTime(2026, 5, 20, 17, 0, 0, 0, DateTimeKind.Utc), null, "Evening Practice", null, 1, new DateTime(2026, 5, 20, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3100), null, new DateTime(2026, 5, 21, 14, 0, 0, 0, DateTimeKind.Utc), null, "Studio Jam", null, 3, new DateTime(2026, 5, 21, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3137), null, new DateTime(2026, 5, 22, 9, 0, 0, 0, DateTimeKind.Utc), null, "Sound Check", null, 4, new DateTime(2026, 5, 22, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3157), null, new DateTime(2026, 5, 23, 18, 0, 0, 0, DateTimeKind.Utc), null, "Full Band", null, 5, new DateTime(2026, 5, 23, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3178), null, new DateTime(2026, 5, 24, 9, 0, 0, 0, DateTimeKind.Utc), null, "Warmup", null, 6, new DateTime(2026, 5, 24, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3194), null, new DateTime(2026, 5, 25, 21, 0, 0, 0, DateTimeKind.Utc), null, "Late Night", null, 7, new DateTime(2026, 5, 25, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 7, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3207), null, new DateTime(2026, 5, 26, 15, 0, 0, 0, DateTimeKind.Utc), null, "Groove Session", null, 8, new DateTime(2026, 5, 26, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 8, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3258), null, new DateTime(2026, 5, 27, 12, 0, 0, 0, DateTimeKind.Utc), null, "Drum Focus", null, 9, new DateTime(2026, 5, 27, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 9, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3272), null, new DateTime(2026, 5, 28, 16, 0, 0, 0, DateTimeKind.Utc), null, "Vocal Practice", null, 10, new DateTime(2026, 5, 28, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 10, new DateTime(2026, 2, 14, 21, 10, 15, 725, DateTimeKind.Utc).AddTicks(3285), null, new DateTime(2026, 5, 29, 16, 0, 0, 0, DateTimeKind.Utc), null, "Stage Run", null, 11, new DateTime(2026, 5, 29, 13, 0, 0, 0, DateTimeKind.Utc) }
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
