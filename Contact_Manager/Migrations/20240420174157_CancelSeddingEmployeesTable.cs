using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Contact_Manager.Migrations
{
    /// <inheritdoc />
    public partial class CancelSeddingEmployeesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Married", "Name", "Phone", "Salary" },
                values: new object[,]
                {
                    { 1, new DateOnly(1881, 6, 30), false, "Albus Dumbledore", "0101221232", 10000m },
                    { 2, new DateOnly(1960, 9, 1), false, "Severus Snape", "0104324323", 7050m },
                    { 3, new DateOnly(1890, 4, 10), true, "Minerva McGonagall", "0105676567", 7070m }
                });
        }
    }
}
