using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehearsalHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteBands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Songs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Songs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Setlists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Setlists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Rehearsals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Rehearsals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Bands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bands",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "BandMembers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BandMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-1",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "DeletedOn", "PasswordHash", "ProfilePictureUrl" },
                values: new object[] { "d562791c-67eb-4d54-9ea6-16b410f1bc9f", new DateTime(2026, 2, 12, 19, 53, 20, 246, DateTimeKind.Utc).AddTicks(1287), null, "AQAAAAIAAYagAAAAELZJTvzVfxxd9YfKyATrigPbijfTlL/UdbocWHTvDekjdeNtPYH8A4O41CJHH9sfjA==", "/images/defaults/users/user1.png" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-2",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "DeletedOn", "PasswordHash" },
                values: new object[] { "8093516a-943d-4fce-8c10-22acd1af467d", new DateTime(2026, 2, 12, 19, 53, 20, 331, DateTimeKind.Utc).AddTicks(7699), null, "AQAAAAIAAYagAAAAEGSC6Mad+fKkXL06e7vJJrBTa0hCsuxZPIVu6ta+PxqxzGbu1b2qAAdE+1bmkUztRQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "DeletedOn", "PasswordHash", "ProfilePictureUrl" },
                values: new object[] { "20a67e6a-c6a0-48d2-9149-05cc4efd798c", new DateTime(2026, 2, 12, 19, 53, 20, 412, DateTimeKind.Utc).AddTicks(4939), null, "AQAAAAIAAYagAAAAEIrJEMEwH9qTOluYBgi02/AHJg2ksF76jxAa24HUrqnO3TFeKvo3vRa9mM3/Zsm+/Q==", "/images/defaults/users/user2.png" });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7519), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7525), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7529), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7532), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7535), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7539), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7542), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7545), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7548), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7552), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7555), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7558), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7561), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7564), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7567), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7570), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7572), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7635), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7638), null });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AvatarUrl", "CreatedOn", "DeletedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(7641), null });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2691), null, "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2761), null, "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2775), null, "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2779), null, "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2783), null, "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2789), null, "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2792), null, "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2796), null, "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2810), null, "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2815), null, "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2818), null, "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2822), null, "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2825), null, "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2850), null, "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2854), null, "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2857), null, "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2860), null, "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2907), null, "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2910), null, "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedOn", "DeletedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 413, DateTimeKind.Utc).AddTicks(2914), null, "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(782), null, new DateTime(2026, 5, 20, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 15, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(811), null, new DateTime(2026, 5, 21, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(868), null, new DateTime(2026, 5, 22, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 7, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(951), null, new DateTime(2026, 5, 23, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 23, 16, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(997), null, new DateTime(2026, 5, 24, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 24, 8, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(1017), null, new DateTime(2026, 5, 25, 21, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 25, 19, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(1032), null, new DateTime(2026, 5, 26, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 26, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(1089), null, new DateTime(2026, 5, 27, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 27, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(1104), null, new DateTime(2026, 5, 28, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 28, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedOn", "DeletedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(1120), null, new DateTime(2026, 5, 29, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 29, 13, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4466), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4471), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4473), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4475), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4478), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4481), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4483), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4485), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4488), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4491), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4493), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4495), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4497), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4500), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4502), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4504), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4571), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4575), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4577), null });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 414, DateTimeKind.Utc).AddTicks(4579), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6929), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6936), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6939), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6941), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6944), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6949), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6951), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6954), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6956), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(6960), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7056), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7059), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7062), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7065), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7068), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7070), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7072), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7079), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7082), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7085), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7088), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7090), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7092), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7095), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7098), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7100), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7103), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7106), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7108), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7111), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7115), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7118), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7121), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7124), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7127), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7130), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7132), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7135), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7137), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7140), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7142), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7145), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7147), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7150), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7152), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7156), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7158), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7197), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7200), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7203), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7205), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7208), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7210), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7213), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7215), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7218), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7220), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7224), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7226), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7229), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7231), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7233), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7236), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7238), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7241), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7245), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7248), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7250), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7253), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7256), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7258), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7261), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7263), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7266), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7268), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7271), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7273), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7276), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7278), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7281), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7283), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7287), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7290), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7315), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7318), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7320), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7322), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7325), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7327), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7330), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7332), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7335), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7337), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7341), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7343), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7346), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7348), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7351), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7353), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7356), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7358), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7360), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7363), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7365), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7368), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7371), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7374), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7376), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7379), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7381), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7384), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7387), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7390), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7393), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7395), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7397), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7400), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7402), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7405), null });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "CreatedOn", "DeletedOn" },
                values: new object[] { new DateTime(2026, 2, 12, 19, 53, 20, 417, DateTimeKind.Utc).AddTicks(7408), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Setlists");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Setlists");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Rehearsals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Rehearsals");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "BandMembers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BandMembers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-1",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl" },
                values: new object[] { "51a44be4-b63a-42f3-b2b2-83307d5c3927", new DateTime(2026, 2, 5, 19, 7, 45, 613, DateTimeKind.Utc).AddTicks(118), "AQAAAAIAAYagAAAAEHINuYI9grzyHVeME0z0Ex6C/32hovfmMTn+YVxYPyph35eyxT4RFl4s0TuzhQAYdA==", "/images/defaults/users/user3.png" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-2",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash" },
                values: new object[] { "ded343ab-daf8-41a0-87bb-82e1642ae7bc", new DateTime(2026, 2, 5, 19, 7, 45, 688, DateTimeKind.Utc).AddTicks(8933), "AQAAAAIAAYagAAAAEPe9pGJ5MdlQgN8IcWGl0CKvGxWJm14N5d8XQEO5LLKG1IqaKAenkvzmt+wIibtd1w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl" },
                values: new object[] { "1e22b7bf-12f7-42bc-b5bd-0824ecc38ba6", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(1575), "AQAAAAIAAYagAAAAEP6oshFCmLKup/0eiKVjBIc64rni90ca8h2IdVFrhPJlj1CCL6LvFHtCzirAakyHFw==", "/images/defaults/users/user3.png" });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7831));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7837) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7840) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7843));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7846));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7850));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7853) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7856) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7858));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7862) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7867) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7870));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7873) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7878) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7887) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(7890) });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(5980), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(5987), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(5991), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(5995), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6005), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6012), "/images/defaults/bands/band1.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6015), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6018), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6123), "/images/defaults/bands/band2.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6149), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6152), "/images/defaults/bands/band2.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6155), "/images/defaults/bands/band1.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6159), "/images/defaults/bands/band1.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6162), "/images/defaults/bands/band1.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6183), "/images/defaults/bands/band2.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6186), "/images/defaults/bands/band2.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6190), "/images/defaults/bands/band2.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6194), "/images/defaults/bands/band1.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6198), "/images/defaults/bands/band3.jpg" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(6201), "/images/defaults/bands/band2.jpg" });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(9820), new DateTime(2024, 5, 20, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 20, 15, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 771, DateTimeKind.Utc).AddTicks(9841), new DateTime(2024, 5, 21, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 21, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(42), new DateTime(2024, 5, 22, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 22, 7, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(65), new DateTime(2024, 5, 23, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 23, 16, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(90), new DateTime(2024, 5, 24, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 24, 8, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(106), new DateTime(2024, 5, 25, 21, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 25, 19, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(120), new DateTime(2024, 5, 26, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 26, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(177), new DateTime(2024, 5, 27, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 27, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(190), new DateTime(2024, 5, 28, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 28, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedOn", "EndRehearsal", "StartRehearsal" },
                values: new object[] { new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(205), new DateTime(2024, 5, 29, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 29, 13, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1658));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1663));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1665));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1667));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1669));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1672));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1676));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1677));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1732));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1734));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1736));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1740));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1742));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1745));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1749));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1751));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(1752));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4404));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4409));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4412));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4415));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4417));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4421));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4424));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4433));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4435));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4438));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4440));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4442));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4445));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4447));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4449));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4451));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4455));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4458));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4465));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4467));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4469));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4471));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4474));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4476));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4478));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4480));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4483));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4488));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4594));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4597));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4601));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4603));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4606));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4608));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4611));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4613));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4615));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4617));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4621));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4623));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4626));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4629));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4634));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4639));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4642));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4644));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4646));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4648));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4650));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4654));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4656));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4659));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4661));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4664));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4666));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4671));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4674));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4676));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4679));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4682));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4685));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4688));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4690));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 70,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4747));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 71,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4749));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 72,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4752));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 73,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 74,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4758));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 75,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4762));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 76,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4764));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 77,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4769));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 78,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4772));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 79,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4774));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 80,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4776));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 81,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4778));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 82,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4781));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 83,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4783));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 84,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4786));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 85,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4789));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 86,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4792));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 87,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 88,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4798));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 89,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4801));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 90,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4803));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 91,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4805));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 92,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4808));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 93,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4810));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 94,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4812));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 95,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 96,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4818));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 97,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4820));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 98,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4823));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 99,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4826));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4829));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4832));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4835));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4837));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4840));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4842));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4847));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4849));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4851));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4853));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4856));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4858));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4860));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4862));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4864));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4867));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4906));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4908));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4911));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4913));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 5, 19, 7, 45, 772, DateTimeKind.Utc).AddTicks(4915));
        }
    }
}
