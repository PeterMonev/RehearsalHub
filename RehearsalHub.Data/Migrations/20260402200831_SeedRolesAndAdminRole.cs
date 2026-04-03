using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RehearsalHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin-id-001", "1", "Admin", "ADMIN" },
                    { "role-user-id-001", "2", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-1",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl" },
                values: new object[] { "dccbdcf3-3325-4455-9dc7-fee537ef3a82", new DateTime(2026, 4, 2, 20, 8, 26, 480, DateTimeKind.Utc).AddTicks(2730), "AQAAAAIAAYagAAAAEFe/n0jsim3kUVCkbcW8vMm/+2liQrH7IOmB4kN05WZlgah4rBzGxgGS60yV+AhvdQ==", "/images/defaults/users/user3.png" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-10",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "d79467e6-7792-4d6f-8448-83d6bd6a2fd9", new DateTime(2026, 4, 2, 20, 8, 27, 314, DateTimeKind.Utc).AddTicks(4364), "AQAAAAIAAYagAAAAEOpvhTtYabtmVlLgwdTnvY4i9JPo9KYDieVEhl6ZtxjvBkCYcHwXMzYadlK+jlD1TA==", "/images/defaults/users/user1.png", "01868457-c1c5-4aec-a7c7-7800ecd809e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-2",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl" },
                values: new object[] { "22b644f4-6b10-48df-bf5b-7d841a46af53", new DateTime(2026, 4, 2, 20, 8, 26, 573, DateTimeKind.Utc).AddTicks(5191), "AQAAAAIAAYagAAAAEEOHghkQmwtA39wK9jJhZOzdVWckpBo+0EhcMFYwaxqS8uRLxlDE/Zlxulq8GZaP+g==", "/images/defaults/users/user2.png" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash" },
                values: new object[] { "671a81fb-6f7b-4f36-9a71-95f2accd6ce6", new DateTime(2026, 4, 2, 20, 8, 26, 664, DateTimeKind.Utc).AddTicks(4163), "AQAAAAIAAYagAAAAEL2hOBiwjAN6NOu+g//faVXqdNFZInkdkEbgR1BfB8dRDld+g7bg1FyBrUMTc+W39Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-4",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "1760fcf4-d9ba-4402-86c2-92f50e94c12e", new DateTime(2026, 4, 2, 20, 8, 26, 767, DateTimeKind.Utc).AddTicks(325), "AQAAAAIAAYagAAAAEFa6rva64zoye/Hu3XPi1R3dakws7sfZUP2AC38KCV5yFI8cVe4rUSyOnEGTPelAvw==", "/images/defaults/users/user3.png", "b2e83423-f116-49ca-8dbe-7824e47f2b2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-5",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "ec8c3fb7-ca3a-42b9-9139-57e082b12f06", new DateTime(2026, 4, 2, 20, 8, 26, 854, DateTimeKind.Utc).AddTicks(5644), "AQAAAAIAAYagAAAAEDNzyET1QbzDKM7LAa9wb+gBkHfqXX8xbJe0yXfYOw9d7Le0lctjQp/uj/pzVnU2tA==", "/images/defaults/users/user3.png", "90b22261-88cd-45ea-9483-4f48bebd9576" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-6",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f48ce4b-8cd6-4892-9b59-e4da6217063a", new DateTime(2026, 4, 2, 20, 8, 26, 945, DateTimeKind.Utc).AddTicks(1898), "AQAAAAIAAYagAAAAEIljXHa2sgNx1gemILtN2wpR3/oMb2hmX8EZxDqoKEuSjnncWIhW2UziyBdRwiQLiw==", "3a6c62ab-9a7f-4ac6-99a3-3ab813859a51" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-7",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "019242b3-f819-4153-b9f3-ca44071235d7", new DateTime(2026, 4, 2, 20, 8, 27, 40, DateTimeKind.Utc).AddTicks(9762), "AQAAAAIAAYagAAAAEMPpjnFMUUK2Inzh76B9Tn3gB5GR22DYMpfORuh8QpaWem7klb3Taiwh+zAc5yiUAw==", "/images/defaults/users/user2.png", "8f5a2b5c-9e5c-44bd-87e6-60c9a0db7880" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-8",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "933ab836-6248-4773-8f32-fa539fd2e9f8", new DateTime(2026, 4, 2, 20, 8, 27, 130, DateTimeKind.Utc).AddTicks(9549), "AQAAAAIAAYagAAAAEFqa6DmfGOEBkQ7B5cadmTnhsxBHVrY9Y21yBhdktgFt+dERQ9UQvlXi3pmgMDKSDA==", "/images/defaults/users/user2.png", "a9c73dc9-517a-4abf-b7d8-c49969aca01e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-9",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca19d5d7-5829-485f-b6a1-dccaf5590545", new DateTime(2026, 4, 2, 20, 8, 27, 223, DateTimeKind.Utc).AddTicks(6303), "AQAAAAIAAYagAAAAEPPSdWJFOxFYI3edHwoyaesCW1lHA470kI0rqIh7rkIesemeZn8NjgB6mdRuadX5kA==", "f2df24d2-1d63-48ee-9530-42466cbdebc3" });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(7895) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(7904) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(7981) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(7985) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(7992));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(7997));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8000));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8011) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8014) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8017));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8019) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8022));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8026) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8029) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8032) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8035) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8038) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(8041) });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4070), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4092), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4096), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4104), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4109), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4238), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4243), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4253), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4257), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4265), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4269), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4272), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4275), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4279));

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4282), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4286), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4289), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 20, 8, 27, 315, DateTimeKind.Utc).AddTicks(4292), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(836));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(897));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(920));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(943));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(958));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(972));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(1016));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(1083));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(1103));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5597));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5599));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5601));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5603));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5606));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5608));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5610));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5612));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5615));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5617));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5618));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5620));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5622));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5624));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5626));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5628));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5686));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5688));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 316, DateTimeKind.Utc).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(201));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(213));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(216));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(218));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(221));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(227));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(229));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(368));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(372));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(375));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(377));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(379));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(382));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(384));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(387));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(393));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(395));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(397));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(400));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(403));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(405));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(408));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(410));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(413));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(416));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(418));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(421));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(423));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(425));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(428));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(432));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(434));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(437));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(439));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(441));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(444));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(446));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(451));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(456));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(516));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(519));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(522));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(524));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(527));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(529));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(531));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(535));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(537));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(542));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(546));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(548));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(551));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(553));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(558));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(563));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(566));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(568));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(572));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(575));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 70,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(583));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 71,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(587));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 72,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 73,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(593));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 74,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(595));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 75,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(598));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 76,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 77,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(602));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 78,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(652));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 79,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(655));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 80,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(658));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 81,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(661));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 82,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(664));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 83,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 84,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(672));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 85,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(879));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 86,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(883));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 87,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(886));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 88,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(889));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 89,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(892));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 90,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(895));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 91,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(898));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 92,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(901));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 93,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(904));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 94,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(907));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 95,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(909));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 96,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(912));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 97,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(915));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 98,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(919));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 99,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(921));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(925));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(927));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(930));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(932));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(935));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(937));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(940));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(942));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(944));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(947));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(949));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(954));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(957));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(959));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(1024));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(1027));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(1029));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(1032));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 20, 8, 27, 317, DateTimeKind.Utc).AddTicks(1035));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "role-admin-id-001", "de305d54-75b4-4311-81d9-7ed39190224b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-user-id-001");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-admin-id-001", "de305d54-75b4-4311-81d9-7ed39190224b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin-id-001");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-1",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl" },
                values: new object[] { "af41c72a-f4ab-4080-a915-2ad374207bf6", new DateTime(2026, 4, 2, 15, 31, 4, 156, DateTimeKind.Utc).AddTicks(7282), "AQAAAAIAAYagAAAAEDOq0x6uXrHHPTPp6kdnwepIcMLiw2pPwJGsovIcNFvWeKPGr2tnhIr4/KYPBf6lfQ==", "/images/defaults/users/user1.png" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-10",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "3f121588-c0b1-45b8-a429-bdfc2b2c87ca", new DateTime(2026, 4, 2, 15, 31, 4, 966, DateTimeKind.Utc).AddTicks(4036), "AQAAAAIAAYagAAAAEJOq3rPqhCdey0YKRsGgTWDt+mYcl2cqaaqUCz+CO2GNP9f/tgalH0QeatUlBnXchw==", "/images/defaults/users/user2.png", "21c93b46-664c-4a0a-aad1-b402bb600061" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-2",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl" },
                values: new object[] { "26651ce4-8f02-4e82-8581-db66b58fcabe", new DateTime(2026, 4, 2, 15, 31, 4, 269, DateTimeKind.Utc).AddTicks(1608), "AQAAAAIAAYagAAAAEBSNGMgUPo+gFAJvaTkrfsoSud0ALm4aSoAt1+tGAeGoTDdXGQlMmqcySFbMn3BlHQ==", "/images/defaults/users/user1.png" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash" },
                values: new object[] { "f2c624f9-c04e-4c4f-bde6-1ac9e3bb9c09", new DateTime(2026, 4, 2, 15, 31, 4, 359, DateTimeKind.Utc).AddTicks(6928), "AQAAAAIAAYagAAAAEHYFxnyWzhAaRYiJn14qZuHAY/DJDCd0qSGK3bDgenZihK8TTDmX6b6VZYbz5XLlBw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-4",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "ebbc8086-1f81-4e45-ba5c-de76c065f5ad", new DateTime(2026, 4, 2, 15, 31, 4, 468, DateTimeKind.Utc).AddTicks(4883), "AQAAAAIAAYagAAAAEFQDhv4zW7ouvMUNOUosYrNCP/09fE03tP/p58akfJWXa8AS4suoYM/ZjidVXy2XJg==", "/images/defaults/users/user2.png", "0c582a62-50a9-420b-9d01-7849c46b12c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-5",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "6db3d2d8-5942-46ee-ad40-bb41a7f31387", new DateTime(2026, 4, 2, 15, 31, 4, 555, DateTimeKind.Utc).AddTicks(9695), "AQAAAAIAAYagAAAAEGyt5AL/pt3IbpwcWSVwgfcg/nS6ruCuTXZJRO2z2IkCcwmweZnw9Ll+VJyd0vI4dQ==", "/images/defaults/users/user2.png", "382ba4fa-62c5-4e6e-af7a-911d2bb1829e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-6",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0dd3dafe-03a4-4f70-8c57-67c6c8e17df5", new DateTime(2026, 4, 2, 15, 31, 4, 636, DateTimeKind.Utc).AddTicks(8497), "AQAAAAIAAYagAAAAEK5LMhlHpsZbx6Q2jZwmaJGPhFV4LRL6wN5UJ+vtj0kjJ58ASHg2l7Zo0Xq6PQAQNg==", "fb4a5d2c-c88a-4886-90d3-869f7197ce74" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-7",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "cc3de2d8-449d-4608-9cbb-b93b8b829aeb", new DateTime(2026, 4, 2, 15, 31, 4, 717, DateTimeKind.Utc).AddTicks(2073), "AQAAAAIAAYagAAAAENtQJC/88vbjkn4iXAGAeF5tMkgylvs++khA+/K/AqVp3imSX51Mj2/7seHafzdY+g==", "/images/defaults/users/user1.png", "b40e89ca-a185-4868-9a36-5b6082d5a694" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-8",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "913a7719-5be6-4c69-9452-55be0b0edfae", new DateTime(2026, 4, 2, 15, 31, 4, 802, DateTimeKind.Utc).AddTicks(9000), "AQAAAAIAAYagAAAAEOvkv9FiWMVcRvPOtG9QNLCM79nIkA0g+3LJmfke+PJqJAtnQ46ilqd3+vR89I9/zA==", "/images/defaults/users/user3.png", "2a3a0f89-a230-475b-83d7-f7fd35654288" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-9",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98e9e028-aedd-4684-be07-218dceec7ae4", new DateTime(2026, 4, 2, 15, 31, 4, 884, DateTimeKind.Utc).AddTicks(2308), "AQAAAAIAAYagAAAAEPlRpSWpi/ehm9W8amDvHTsRkbe7LBHTEUQCtzIEC94DGWtDIunUw08mFmg4XAeprQ==", "eb0c595f-38c7-4cb8-acb3-77caa7be0477" });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7603) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member2.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7612) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7616) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7619) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7622));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7626));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7695));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7699));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7703) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7706) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7708));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7711) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7714));

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7717) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7722) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7726) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member1.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7729) });

            migrationBuilder.UpdateData(
                table: "BandMembers",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AvatarUrl", "CreatedOn" },
                values: new object[] { "/images/defaults/members/member3.png", new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(7731) });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3257), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3284), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3289), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3292));

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3295), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3300), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3320), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3323), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3329));

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3333), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3370), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3381), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3384), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3387), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3391), "/images/defaults/bands/band3.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3394));

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3397), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3401), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3404), "/images/defaults/bands/band1.png" });

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2026, 4, 2, 15, 31, 4, 967, DateTimeKind.Utc).AddTicks(3408), "/images/defaults/bands/band2.png" });

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(588));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(619));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(655));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(674));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(705));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(720));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(734));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(775));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(789));

            migrationBuilder.UpdateData(
                table: "Rehearsals",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(807));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3281));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3339));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3342));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3344));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3346));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3348));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3350));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3353));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3354));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3356));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3358));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3360));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3362));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3364));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3366));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3369));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3371));

            migrationBuilder.UpdateData(
                table: "Setlists",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(3373));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7529));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7540));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7542));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7545));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7551));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7554));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7556));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7562));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7564));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7567));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7569));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7573));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7575));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7578));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7580));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7584));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7586));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7589));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7591));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7594));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7596));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7599));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7602));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7605));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7608));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7616));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7619));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7656));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7660));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7663));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7666));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7669));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7672));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7674));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7677));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7679));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7682));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7684));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7689));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7692));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7695));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7697));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7701));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7703));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7706));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7709));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7711));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7713));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7716));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7720));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7723));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7725));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7731));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7733));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7736));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7738));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7741));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7743));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7833));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7837));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7842));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7845));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 70,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7849));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 71,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7851));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 72,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7854));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 73,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7856));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 74,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7859));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 75,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 76,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7864));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 77,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7866));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 78,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7869));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 79,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7871));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 80,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7874));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 81,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 82,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 83,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7882));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 84,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 85,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 86,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 87,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 88,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 89,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 90,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7900));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 91,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7903));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 92,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 93,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 94,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 95,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 96,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 97,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 98,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 99,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7975));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7978));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7989));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7991));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7996));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(7999));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8001));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8004));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8006));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8008));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8016));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8018));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8021));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedOn",
                value: new DateTime(2026, 4, 2, 15, 31, 4, 968, DateTimeKind.Utc).AddTicks(8024));
        }
    }
}
