using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4_Calculator.Migrations
{
    /// <inheritdoc />
    public partial class Kafka1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataImputVariants");

            migrationBuilder.CreateTable(
                name: "DataInputVariants",
                columns: table => new
                {
                    ID_DataInputVariant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Operand_1 = table.Column<double>(type: "double", nullable: false),
                    Operand_2 = table.Column<double>(type: "double", nullable: false),
                    Type_operation = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "varchar(128)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataInputVariants", x => x.ID_DataInputVariant);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataInputVariants");

            migrationBuilder.CreateTable(
                name: "DataImputVariants",
                columns: table => new
                {
                    ID_DataInputVariant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Operand_1 = table.Column<string>(type: "varchar(128)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Operand_2 = table.Column<string>(type: "varchar(128)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_operation = table.Column<string>(type: "varchar(128)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataImputVariants", x => x.ID_DataInputVariant);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
