using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HairSalon_Booking_Engine.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTransactionAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTransaction");

            migrationBuilder.DropTable(
                name: "TransactionTreatment");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransaction",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    TransactionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransaction", x => new { x.ProductsId, x.TransactionsId });
                    table.ForeignKey(
                        name: "FK_ProductTransaction_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTransaction_Transactions_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTreatment",
                columns: table => new
                {
                    TransactionsId = table.Column<int>(type: "int", nullable: false),
                    TreatmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTreatment", x => new { x.TransactionsId, x.TreatmentsId });
                    table.ForeignKey(
                        name: "FK_TransactionTreatment_Transactions_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionTreatment_Treatments_TreatmentsId",
                        column: x => x.TreatmentsId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Kérastase", "Sulphate-free shampoo for dry or colour-treated hair.", "Hydrating Shampoo 250 ml", 299m, 24 },
                    { 2, "Kérastase", "Rich conditioner that rebuilds damaged hair fibre.", "Repair Conditioner 200 ml", 319m, 18 },
                    { 3, "Moroccanoil", "Lightweight serum for shine and frizz control.", "Argan Oil Serum 100 ml", 389m, 30 },
                    { 4, "Redken", "Heat-activated mousse for lasting lift and body.", "Volumising Mousse 200 ml", 229m, 15 },
                    { 5, "Redken", "Protects up to 230 °C from heat styling damage.", "Heat Protectant Spray 150 ml", 249m, 22 },
                    { 6, "Fanola", "Neutralises brassy tones in blonde and grey hair.", "Purple Toning Shampoo 250 ml", 179m, 20 },
                    { 7, "Davines", "Exfoliating scrub that removes build-up and balances scalp.", "Scalp Scrub 150 ml", 269m, 12 },
                    { 8, "Ouidad", "Nourishing cream that enhances and defines natural curls.", "Defining Curl Cream 200 ml", 299m, 10 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "CustomerId", "Date", "Total" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 900m },
                    { 2, 2, new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 578m },
                    { 3, 3, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2050m },
                    { 4, 4, new DateTime(2025, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1249m },
                    { 5, 5, new DateTime(2025, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1400m }
                });

            migrationBuilder.InsertData(
                table: "ProductTransaction",
                columns: new[] { "ProductsId", "TransactionsId" },
                values: new object[,]
                {
                    { 1, 5 },
                    { 2, 5 },
                    { 3, 2 },
                    { 5, 4 },
                    { 6, 3 }
                });

            migrationBuilder.InsertData(
                table: "TransactionTreatment",
                columns: new[] { "TransactionsId", "TreatmentsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 8 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 6 },
                    { 4, 3 },
                    { 5, 5 },
                    { 5, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransaction_TransactionsId",
                table: "ProductTransaction",
                column: "TransactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTreatment_TreatmentsId",
                table: "TransactionTreatment",
                column: "TreatmentsId");
        }
    }
}
