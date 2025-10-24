using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmolyeePortal.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                        column: x => x.EmployeeTypeId,
                        principalTable: "EmployeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, false, "IT" },
                    { 2, false, "HR" },
                    { 3, false, "Sales" },
                    { 4, false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, false, "Permanent" },
                    { 2, false, "Temporary" },
                    { 3, false, "Contract" },
                    { 4, false, "Intern" }
                });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "Id", "DepartmentId", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, "Software Developer" },
                    { 2, 1, false, "System Administrator" },
                    { 3, 1, false, "NetWork  Engineer" },
                    { 4, 2, false, "HR Specialist" },
                    { 5, 2, false, "HR Manager" },
                    { 6, 2, false, "Talent Acquisition Coordinator" },
                    { 7, 3, false, "Sales Executive " },
                    { 8, 3, false, "Sales Manager " },
                    { 9, 3, false, "Account Executive " },
                    { 10, 4, false, "Office Manager " },
                    { 11, 4, false, "Executive Assistence " },
                    { 12, 4, false, "Receptionist " }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "DepartmentId", "DesignationId", "Email", "EmployeeTypeId", "FullName", "Gender", "HireDate", "Salary" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "John@example.com", 1, "John Snow", "Male", new DateTime(2020, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 60000m },
                    { 2, new DateTime(1985, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, "Patrik@example.com", 1, "Patrik BedDavid", "Female", new DateTime(2018, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 80000m },
                    { 3, new DateTime(1991, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, "Shamil@example.com", 3, "Shamil", "Male", new DateTime(2019, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 61000m },
                    { 4, new DateTime(1993, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8, "Chris@example.com", 3, "Chris Willx", "Male", new DateTime(2020, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 30000m },
                    { 5, new DateTime(1982, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9, "Hamza@example.com", 2, "Hamza Ahmed", "Female", new DateTime(2017, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 67000m },
                    { 6, new DateTime(1984, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 10, "Iman@example.com", 1, "Iman Gadzi", "Male", new DateTime(2020, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 63000m },
                    { 7, new DateTime(1993, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 12, "David@example.com", 2, "David Goggins", "Female", new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 69000m },
                    { 8, new DateTime(1994, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 9, "Andrew@example.com", 1, "Adrew T", "Male", new DateTime(2018, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 68000m },
                    { 9, new DateTime(1996, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7, "Tristan@example.com", 3, "Tristan T", "Female", new DateTime(2019, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 65000m },
                    { 10, new DateTime(1988, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, "Luke@example.com", 1, "Luke Balmer", "Male", new DateTime(2022, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 72000m },
                    { 11, new DateTime(1987, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 6, "khabib@example.com", 1, "Khabib NM", "Male", new DateTime(2021, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 76000m },
                    { 12, new DateTime(1995, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, "Jones@example.com", 2, "John Jones", "Female", new DateTime(2019, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 74000m },
                    { 13, new DateTime(1997, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "Mike@example.com", 1, "Mike Tyson", "Male", new DateTime(2018, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 79000m },
                    { 14, new DateTime(1989, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9, "Bruce@example.com", 1, "Bruce Wayne", "Female", new DateTime(2017, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 75000m },
                    { 15, new DateTime(1992, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, "Batman@example.com", 3, "Batman", "Male", new DateTime(2021, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 62000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designations_DepartmentId",
                table: "Designations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
