using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Assignment.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturer", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "dealership",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    manufacturerid = table.Column<int>(type: "int(11)", nullable: false),
                    address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phonenumber = table.Column<string>(type: "char(10)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dealership", x => x.id);
                    table.ForeignKey(
                        name: "FK_Dealership_Manufacturer",
                        column: x => x.manufacturerid,
                        principalTable: "manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "model",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    manufacturerid = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model", x => x.id);
                    table.ForeignKey(
                        name: "FK_Model_Manufacturer",
                        column: x => x.manufacturerid,
                        principalTable: "manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    vin = table.Column<string>(type: "char(17)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modelid = table.Column<int>(type: "int(11)", nullable: false),
                    DealershipID = table.Column<int>(type: "int(11)", nullable: false),
                    trimlevel = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.vin);
                    table.ForeignKey(
                        name: "FK_Vehicle_Dealership",
                        column: x => x.DealershipID,
                        principalTable: "dealership",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_Model",
                        column: x => x.modelid,
                        principalTable: "model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.InsertData(
                table: "manufacturer",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Ford" },
                    { 2, "Chevrolet" },
                    { 3, "Dodge" },
                    { 4, "Honda" },
                    { 5, "Toyota" }
                });

            migrationBuilder.InsertData(
                table: "dealership",
                columns: new[] { "id", "address", "manufacturerid", "name", "phonenumber" },
                values: new object[] { 1, "123 Cool St", 1, "Joe's Discount Fords", "8005551234" });

            migrationBuilder.InsertData(
                table: "model",
                columns: new[] { "id", "manufacturerid", "name" },
                values: new object[,]
                {
                    { 1, 1, "Mustang" },
                    { 2, 1, "F-150" },
                    { 3, 2, "Corvette" },
                    { 4, 2, "Camaro" },
                    { 5, 3, "Challenger" },
                    { 6, 3, "Charger" },
                    { 7, 4, "Civic" },
                    { 8, 4, "Accord" },
                    { 9, 5, "Corolla" },
                    { 10, 5, "Camry" }
                });

            migrationBuilder.InsertData(
                table: "vehicle",
                columns: new[] { "vin", "DealershipID", "modelid", "trimlevel" },
                values: new object[] { "1FA6P8TH4J5102322", 1, 1, "Ecoboost" });

            migrationBuilder.InsertData(
                table: "vehicle",
                columns: new[] { "vin", "DealershipID", "modelid", "trimlevel" },
                values: new object[] { "2C3CDXCT7JH260378", 1, 6, "R/T" });

            migrationBuilder.InsertData(
                table: "vehicle",
                columns: new[] { "vin", "DealershipID", "modelid", "trimlevel" },
                values: new object[] { "2HGFC3A51LH220441", 1, 7, "SI" });

            migrationBuilder.CreateIndex(
                name: "FK_Dealership_Manufacturer",
                table: "dealership",
                column: "manufacturerid");

            migrationBuilder.CreateIndex(
                name: "FK_Model_Manufacturer",
                table: "model",
                column: "manufacturerid");

            migrationBuilder.CreateIndex(
                name: "FK_Vehicle_Dealership",
                table: "vehicle",
                column: "DealershipID");

            migrationBuilder.CreateIndex(
                name: "FK_Vehicle_Model",
                table: "vehicle",
                column: "modelid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.DropTable(
                name: "dealership");

            migrationBuilder.DropTable(
                name: "model");

            migrationBuilder.DropTable(
                name: "manufacturer");
        }
    }
}
