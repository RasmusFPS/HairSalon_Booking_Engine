using HairSalon_Booking_Engine.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSalon_Booking_Engine
{
    internal static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // ----------------------------------------------------------------
            // Stylists
            // ----------------------------------------------------------------
            modelBuilder.Entity<Stylist>().HasData(
                new Stylist { Id = 1, FirstName = "Sofia",   LastName = "Andersson" },
                new Stylist { Id = 2, FirstName = "Marcus",  LastName = "Lindqvist" },
                new Stylist { Id = 3, FirstName = "Isabelle", LastName = "Karlsson" }
            );

            // ----------------------------------------------------------------
            // Customers
            // ----------------------------------------------------------------
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Emma",   LastName = "Johansson", Phone = "070-123 45 67", Email = "emma.johansson@example.com" },
                new Customer { Id = 2, FirstName = "Lucas",  LastName = "Berg",      Phone = "073-234 56 78", Email = "lucas.berg@example.com" },
                new Customer { Id = 3, FirstName = "Maja",   LastName = "Nilsson",   Phone = "076-345 67 89", Email = null },
                new Customer { Id = 4, FirstName = "Oliver", LastName = "Svensson",  Phone = "070-456 78 90", Email = "oliver.svensson@example.com" },
                new Customer { Id = 5, FirstName = "Astrid", LastName = "Eriksson",  Phone = "073-567 89 01", Email = "astrid.eriksson@example.com" }
            );

            // ----------------------------------------------------------------
            // Treatments
            // ----------------------------------------------------------------
            modelBuilder.Entity<Treatment>().HasData(
                new Treatment { Id = 1, Name = "Women's Cut & Blowdry",  Description = "Precision cut with a full blowdry finish.",          Price = 650m,  DurationMin = 60  },
                new Treatment { Id = 2, Name = "Men's Cut",              Description = "Classic scissor or clipper cut.",                    Price = 350m,  DurationMin = 30  },
                new Treatment { Id = 3, Name = "Full Colour",            Description = "All-over colour with premium tint.",                 Price = 950m,  DurationMin = 90  },
                new Treatment { Id = 4, Name = "Highlights – Half Head", Description = "Foil highlights on the top and crown sections.",     Price = 800m,  DurationMin = 75  },
                new Treatment { Id = 5, Name = "Highlights – Full Head", Description = "Foil highlights throughout the entire head.",        Price = 1100m, DurationMin = 105 },
                new Treatment { Id = 6, Name = "Balayage",               Description = "Hand-painted freehand lightening technique.",        Price = 1400m, DurationMin = 120 },
                new Treatment { Id = 7, Name = "Keratin Treatment",      Description = "Smoothing treatment for frizz-free, glossy hair.",   Price = 1800m, DurationMin = 150 },
                new Treatment { Id = 8, Name = "Deep Conditioning Mask", Description = "Intensive repair mask with steam application.",      Price = 250m,  DurationMin = 30  },
                new Treatment { Id = 9, Name = "Children's Cut (≤12)",  Description = "Relaxed cut for children aged 12 and under.",        Price = 250m,   DurationMin = 30  }
            );

            // ----------------------------------------------------------------
            // Products
            // ----------------------------------------------------------------
            //modelBuilder.Entity<Product>().HasData(
            //    new Product { Id = 1, Name = "Hydrating Shampoo 250 ml",      Description = "Sulphate-free shampoo for dry or colour-treated hair.",   Brand = "Kérastase",  Price = 299m, Stock = 24 },
            //    new Product { Id = 2, Name = "Repair Conditioner 200 ml",     Description = "Rich conditioner that rebuilds damaged hair fibre.",       Brand = "Kérastase",  Price = 319m, Stock = 18 },
            //    new Product { Id = 3, Name = "Argan Oil Serum 100 ml",        Description = "Lightweight serum for shine and frizz control.",           Brand = "Moroccanoil", Price = 389m, Stock = 30 },
            //    new Product { Id = 4, Name = "Volumising Mousse 200 ml",      Description = "Heat-activated mousse for lasting lift and body.",          Brand = "Redken",     Price = 229m, Stock = 15 },
            //    new Product { Id = 5, Name = "Heat Protectant Spray 150 ml",  Description = "Protects up to 230 °C from heat styling damage.",          Brand = "Redken",     Price = 249m, Stock = 22 },
            //    new Product { Id = 6, Name = "Purple Toning Shampoo 250 ml",  Description = "Neutralises brassy tones in blonde and grey hair.",        Brand = "Fanola",     Price = 179m, Stock = 20 },
            //    new Product { Id = 7, Name = "Scalp Scrub 150 ml",            Description = "Exfoliating scrub that removes build-up and balances scalp.", Brand = "Davines", Price = 269m, Stock = 12 },
            //    new Product { Id = 8, Name = "Defining Curl Cream 200 ml",    Description = "Nourishing cream that enhances and defines natural curls.", Brand = "Ouidad",    Price = 299m, Stock = 10 }
            //);

            // ----------------------------------------------------------------
            // Bookings
            // ----------------------------------------------------------------
            modelBuilder.Entity<Booking>().HasData(
                // BookedDate = when the appointment was made; BookingDate = when they come in
                new Booking { Id = 1, CreatedAt = new DateTime(2025, 5, 1),  StartTime = new DateTime(2025, 5, 12, 10, 0, 0), StylistId = 1, CustomerId = 1 },
                new Booking { Id = 2, CreatedAt = new DateTime(2025, 5, 3),  StartTime = new DateTime(2025, 5, 14, 13, 30, 0), StylistId = 2, CustomerId = 2 },
                new Booking { Id = 3, CreatedAt = new DateTime(2025, 5, 5),  StartTime = new DateTime(2025, 5, 15, 11, 0, 0), StylistId = 1, CustomerId = 3 },
                new Booking { Id = 4, CreatedAt = new DateTime(2025, 5, 6),  StartTime = new DateTime(2025, 5, 19, 9, 0, 0),  StylistId = 3, CustomerId = 4 },
                new Booking { Id = 5, CreatedAt = new DateTime(2025, 5, 8),  StartTime = new DateTime(2025, 5, 21, 14, 0, 0), StylistId = 2, CustomerId = 5 },
                new Booking { Id = 6, CreatedAt = new DateTime(2025, 5, 10), StartTime = new DateTime(2025, 5, 22, 10, 30, 0), StylistId = 3, CustomerId = 1 }
            );

            // ----------------------------------------------------------------
            // Booking ↔ Treatment (join table)
            // EF Core convention names this BookingTreatment; adjust if yours differs.
            // ----------------------------------------------------------------
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Treatments)
                .WithMany(t => t.Bookings)
                .UsingEntity(j => j.HasData(
                    new { BookingsId = 1, TreatmentsId = 1 },  // Emma  – Women's Cut & Blowdry
                    new { BookingsId = 1, TreatmentsId = 8 },  // Emma  – Deep Conditioning Mask
                    new { BookingsId = 2, TreatmentsId = 2 },  // Lucas – Men's Cut
                    new { BookingsId = 3, TreatmentsId = 6 },  // Maja  – Balayage
                    new { BookingsId = 3, TreatmentsId = 1 },  // Maja  – Women's Cut & Blowdry
                    new { BookingsId = 4, TreatmentsId = 3 },  // Oliver – Full Colour
                    new { BookingsId = 5, TreatmentsId = 5 },  // Astrid – Full Head Highlights
                    new { BookingsId = 5, TreatmentsId = 8 },  // Astrid – Deep Conditioning Mask
                    new { BookingsId = 6, TreatmentsId = 7 }   // Emma  – Keratin Treatment
                ));

            // ----------------------------------------------------------------
            // Transactions
            // ----------------------------------------------------------------
            //modelBuilder.Entity<Transaction>().HasData(
            //    new Transaction { Id = 1, Date = new DateTime(2025, 5, 12), CustomerId = 1, Total = 900m  },  // Cut + Mask
            //    new Transaction { Id = 2, Date = new DateTime(2025, 5, 14), CustomerId = 2, Total = 578m  },  // Men's Cut + Argan Oil
            //    new Transaction { Id = 3, Date = new DateTime(2025, 5, 15), CustomerId = 3, Total = 2050m },  // Balayage + Cut
            //    new Transaction { Id = 4, Date = new DateTime(2025, 5, 19), CustomerId = 4, Total = 1249m },  // Full Colour + Heat Spray
            //    new Transaction { Id = 5, Date = new DateTime(2025, 5, 21), CustomerId = 5, Total = 1400m }   // Full Highlights + Mask
            //);

            // ----------------------------------------------------------------
            // Transaction ↔ Treatment (join table)
            // ----------------------------------------------------------------
            //modelBuilder.Entity<Transaction>()
            //    .HasMany(t => t.Treatments)
            //    .WithMany(tr => tr.Transactions)
            //    .UsingEntity(j => j.HasData(
            //        new { TransactionsId = 1, TreatmentsId = 1 },
            //        new { TransactionsId = 1, TreatmentsId = 8 },
            //        new { TransactionsId = 2, TreatmentsId = 2 },
            //        new { TransactionsId = 3, TreatmentsId = 6 },
            //        new { TransactionsId = 3, TreatmentsId = 1 },
            //        new { TransactionsId = 4, TreatmentsId = 3 },
            //        new { TransactionsId = 5, TreatmentsId = 5 },
            //        new { TransactionsId = 5, TreatmentsId = 8 }
            //    ));

            // ----------------------------------------------------------------
            // Transaction ↔ Product (join table)
            // ----------------------------------------------------------------
            //modelBuilder.Entity<Transaction>()
            //    .HasMany(t => t.Products)
            //    .WithMany(p => p.Transactions)
            //    .UsingEntity(j => j.HasData(
            //        new { TransactionsId = 2, ProductsId = 3 },  // Lucas bought Argan Oil
            //        new { TransactionsId = 4, ProductsId = 5 },  // Oliver bought Heat Protectant
            //        new { TransactionsId = 3, ProductsId = 6 },  // Maja bought Purple Toning Shampoo
            //        new { TransactionsId = 5, ProductsId = 1 },  // Astrid bought Hydrating Shampoo
            //        new { TransactionsId = 5, ProductsId = 2 }   // Astrid bought Repair Conditioner
            //    ));
        }
    }
}
