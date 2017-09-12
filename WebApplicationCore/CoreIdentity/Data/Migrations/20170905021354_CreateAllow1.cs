using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreIdentity.Data.Migrations
{
    public partial class CreateAllow1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allowances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowanceType = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    OTE = table.Column<bool>(type: "bit", nullable: false),
                    PaidBy = table.Column<int>(type: "int", nullable: false),
                    PayrollTax = table.Column<bool>(type: "bit", nullable: false),
                    SGRule = table.Column<bool>(type: "bit", nullable: false),
                    SpecialSuperRate = table.Column<float>(type: "real", nullable: false),
                    SpecialSuperRule = table.Column<bool>(type: "bit", nullable: false),
                    Taxable = table.Column<bool>(type: "bit", nullable: false),
                    UsedAllowanceID = table.Column<int>(type: "int", nullable: false),
                    W1 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allowances", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allowances");
        }
    }
}
