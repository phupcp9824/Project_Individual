using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Updatesupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Supplier_SupplierId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "Supplier");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "suppliers");

            migrationBuilder.RenameColumn(
                name: "TenNhaCungCap",
                table: "suppliers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SoDienThoai",
                table: "suppliers",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_suppliers",
                table: "suppliers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_suppliers_SupplierId",
                table: "products",
                column: "SupplierId",
                principalTable: "suppliers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_suppliers_SupplierId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_suppliers",
                table: "suppliers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "suppliers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "suppliers");

            migrationBuilder.RenameTable(
                name: "suppliers",
                newName: "Supplier");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Supplier",
                newName: "SoDienThoai");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Supplier",
                newName: "TenNhaCungCap");

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Supplier_SupplierId",
                table: "products",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id");
        }
    }
}
