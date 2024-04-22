using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Contact_Manager.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Married", "Name", "Phone", "Salary" },
                values: new object[,]
                {
                    { 1, new DateTime(1881, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Albus Dumbledore", "0101221232", 10000m },
                    { 2, new DateTime(1960, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Severus Snape", "0104324323", 7050m },
                    { 3, new DateTime(1890, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Minerva McGonagall", "0105676567", 7070m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
