using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HairSalon_Booking_Engine.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stylist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stylist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
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
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StylistId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Stylist_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylist",
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

            migrationBuilder.CreateTable(
                name: "BookingTreatment",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "int", nullable: false),
                    TreatmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTreatment", x => new { x.BookingsId, x.TreatmentsId });
                    table.ForeignKey(
                        name: "FK_BookingTreatment_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingTreatment_Treatments_TreatmentsId",
                        column: x => x.TreatmentsId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "emma.johansson@example.com", "Emma", "Johansson", "070-123 45 67" },
                    { 2, "lucas.berg@example.com", "Lucas", "Berg", "073-234 56 78" },
                    { 3, null, "Maja", "Nilsson", "076-345 67 89" },
                    { 4, "oliver.svensson@example.com", "Oliver", "Svensson", "070-456 78 90" },
                    { 5, "astrid.eriksson@example.com", "Astrid", "Eriksson", "073-567 89 01" }
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
                table: "Stylist",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Sofia", "Andersson" },
                    { 2, "Marcus", "Lindqvist" },
                    { 3, "Isabelle", "Karlsson" }
                });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Precision cut with a full blowdry finish.", 60, "Women's Cut & Blowdry", 650m },
                    { 2, "Classic scissor or clipper cut.", 30, "Men's Cut", 350m },
                    { 3, "All-over colour with premium tint.", 90, "Full Colour", 950m },
                    { 4, "Foil highlights on the top and crown sections.", 75, "Highlights – Half Head", 800m },
                    { 5, "Foil highlights throughout the entire head.", 105, "Highlights – Full Head", 1100m },
                    { 6, "Hand-painted freehand lightening technique.", 120, "Balayage", 1400m },
                    { 7, "Smoothing treatment for frizz-free, glossy hair.", 150, "Keratin Treatment", 1800m },
                    { 8, "Intensive repair mask with steam application.", 30, "Deep Conditioning Mask", 250m },
                    { 9, "Relaxed cut for children aged 12 and under.", 30, "Children's Cut (≤12)", 250m }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookedDate", "BookingDate", "CustomerId", "StylistId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 14, 13, 30, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 4, new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 },
                    { 5, new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), 5, 2 },
                    { 6, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), 1, 3 }
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
                table: "BookingTreatment",
                columns: new[] { "BookingsId", "TreatmentsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 8 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 6 },
                    { 4, 3 },
                    { 5, 5 },
                    { 5, 8 },
                    { 6, 7 }
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
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StylistId",
                table: "Bookings",
                column: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTreatment_TreatmentsId",
                table: "BookingTreatment",
                column: "TreatmentsId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingTreatment");

            migrationBuilder.DropTable(
                name: "ProductTransaction");

            migrationBuilder.DropTable(
                name: "TransactionTreatment");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Stylist");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
