using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_carts_CartId",
                table: "orderDetails");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShipName",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShipPhoneNumber",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "orderDetails",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_CartId",
                table: "orderDetails",
                newName: "IX_orderDetails_OrderId");

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "orderDetails",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "NameProduct",
                table: "orderDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "orderDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "orderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_orders_OrderId",
                table: "orderDetails",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_orders_OrderId",
                table: "orderDetails");

            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "orderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "orderDetails",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_OrderId",
                table: "orderDetails",
                newName: "IX_orderDetails_CartId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipName",
                table: "orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShipPhoneNumber",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "orderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameProduct",
                table: "orderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "orderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carts_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_carts_UserId",
                table: "carts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_carts_CartId",
                table: "orderDetails",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "Id");
        }
    }
}
