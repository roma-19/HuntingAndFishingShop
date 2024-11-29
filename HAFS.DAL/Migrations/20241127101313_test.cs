using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductDbId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserDbId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductDbId",
                table: "Orders",
                column: "ProductDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserDbId",
                table: "Orders",
                column: "UserDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductDbId",
                table: "Orders",
                column: "ProductDbId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserDbId",
                table: "Orders",
                column: "UserDbId",
                principalTable: "Users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductDbId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserDbId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductDbId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserDbId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductDbId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserDbId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");
        }
    }
}
