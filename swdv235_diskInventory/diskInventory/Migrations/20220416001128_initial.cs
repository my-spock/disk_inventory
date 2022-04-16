using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace diskInventory.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "borrower",
                columns: table => new
                {
                    borrower_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    borrower_name = table.Column<string>(nullable: true),
                    borrower_phone_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrower", x => x.borrower_id);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    genre_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genre_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "item_type",
                columns: table => new
                {
                    item_type_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_type_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_type", x => x.item_type_id);
                });

            migrationBuilder.CreateTable(
                name: "status_type",
                columns: table => new
                {
                    status_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_type", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "item",
                columns: table => new
                {
                    item_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_name = table.Column<string>(nullable: true),
                    release_date = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    GenreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item", x => x.item_id);
                    table.ForeignKey(
                        name: "FK_item_genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "genre",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_item_status_type_StatusId",
                        column: x => x.StatusId,
                        principalTable: "status_type",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_item_item_type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "item_type",
                        principalColumn: "item_type_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "borrowed_item",
                columns: table => new
                {
                    borrowed_item_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    borrowed_date = table.Column<DateTime>(nullable: false),
                    returned_date = table.Column<DateTime>(nullable: true),
                    BorrowerId = table.Column<int>(nullable: true),
                    ItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrowed_item", x => x.borrowed_item_id);
                    table.ForeignKey(
                        name: "FK_borrowed_item_borrower_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "borrower",
                        principalColumn: "borrower_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_borrowed_item_item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "item",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_borrowed_item_BorrowerId",
                table: "borrowed_item",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_borrowed_item_ItemId",
                table: "borrowed_item",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_item_GenreId",
                table: "item",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_item_StatusId",
                table: "item",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_item_TypeId",
                table: "item",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "borrowed_item");

            migrationBuilder.DropTable(
                name: "borrower");

            migrationBuilder.DropTable(
                name: "item");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "status_type");

            migrationBuilder.DropTable(
                name: "item_type");
        }
    }
}
