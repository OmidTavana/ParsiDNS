using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParsiDNS.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Init_DataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DNS",
                columns: table => new
                {
                    DnsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Dns1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Dns2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DNS", x => x.DnsId);
                });

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.SoftwareId);
                });

            migrationBuilder.CreateTable(
                name: "DnsSoftware",
                columns: table => new
                {
                    DS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    DnsId = table.Column<int>(type: "int", nullable: false),
                    TotalLikeCount = table.Column<int>(type: "int", nullable: false),
                    LastMonthLikeCount = table.Column<int>(type: "int", nullable: false),
                    LastWeekLikeCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DnsSoftware", x => x.DS_Id);
                    table.ForeignKey(
                        name: "FK_DnsSoftware_DNS_DnsId",
                        column: x => x.DnsId,
                        principalTable: "DNS",
                        principalColumn: "DnsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DnsSoftware_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DnsSoftware_DnsId",
                table: "DnsSoftware",
                column: "DnsId");

            migrationBuilder.CreateIndex(
                name: "IX_DnsSoftware_SoftwareId",
                table: "DnsSoftware",
                column: "SoftwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DnsSoftware");

            migrationBuilder.DropTable(
                name: "DNS");

            migrationBuilder.DropTable(
                name: "Software");
        }
    }
}
