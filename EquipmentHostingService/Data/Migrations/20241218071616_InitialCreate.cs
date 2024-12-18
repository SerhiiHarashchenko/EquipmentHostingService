using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentHostingService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StandardArea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPlacementContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityCode = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EquipmentTypeCode = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EquipmentQuantity = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPlacementContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentPlacementContracts_EquipmentTypes_EquipmentTypeCode",
                        column: x => x.EquipmentTypeCode,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentPlacementContracts_Facilities_FacilityCode",
                        column: x => x.FacilityCode,
                        principalTable: "Facilities",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPlacementContract_FacilityCode",
                table: "EquipmentPlacementContracts",
                column: "FacilityCode");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPlacementContracts_EquipmentTypeCode",
                table: "EquipmentPlacementContracts",
                column: "EquipmentTypeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentPlacementContracts");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "Facilities");
        }
    }
}
