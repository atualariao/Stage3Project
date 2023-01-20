using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceWebAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    PrimaryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserPrimaryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderTotalPrice = table.Column<double>(type: "float", nullable: false),
                    OrderCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.PrimaryID);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserPrimaryID",
                        column: x => x.UserPrimaryID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    ItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemPrice = table.Column<double>(type: "float", nullable: false),
                    OrderPrimaryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_CartItems_Orders_OrderPrimaryID",
                        column: x => x.OrderPrimaryID,
                        principalTable: "Orders",
                        principalColumn: "PrimaryID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_OrderPrimaryID",
                table: "CartItems",
                column: "OrderPrimaryID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserPrimaryID",
                table: "Orders",
                column: "UserPrimaryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
