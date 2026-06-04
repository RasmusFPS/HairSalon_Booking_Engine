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
                new Stylist { Id = 1, FirstName = "Sofia", LastName = "Andersson" },
                new Stylist { Id = 2, FirstName = "Marcus", LastName = "Lindqvist" },
                new Stylist { Id = 3, FirstName = "Isabelle", LastName = "Karlsson" }
            );

            // ----------------------------------------------------------------
            // Customers
            // ----------------------------------------------------------------
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Emma", LastName = "Johansson", Phone = "070-123 45 67", Email = "emma.johansson@example.com" },
                new Customer { Id = 2, FirstName = "Lucas", LastName = "Berg", Phone = "073-234 56 78", Email = "lucas.berg@example.com" },
                new Customer { Id = 3, FirstName = "Maja", LastName = "Nilsson", Phone = "076-345 67 89", Email = null },
                new Customer { Id = 4, FirstName = "Oliver", LastName = "Svensson", Phone = "070-456 78 90", Email = "oliver.svensson@example.com" },
                new Customer { Id = 5, FirstName = "Astrid", LastName = "Eriksson", Phone = "073-567 89 01", Email = "astrid.eriksson@example.com" }
            );

            // ----------------------------------------------------------------
            // Treatments
            // ----------------------------------------------------------------
            modelBuilder.Entity<Treatment>().HasData(
                new Treatment { Id = 1, Name = "Women's Cut & Blowdry", Description = "Precision cut with a full blowdry finish.", Price = 650m, DurationMin = 60 },
                new Treatment { Id = 2, Name = "Men's Cut", Description = "Classic scissor or clipper cut.", Price = 350m, DurationMin = 30 },
                new Treatment { Id = 3, Name = "Full Colour", Description = "All-over colour with premium tint.", Price = 950m, DurationMin = 90 },
                new Treatment { Id = 4, Name = "Highlights – Half Head", Description = "Foil highlights on the top and crown sections.", Price = 800m, DurationMin = 75 },
                new Treatment { Id = 5, Name = "Highlights – Full Head", Description = "Foil highlights throughout the entire head.", Price = 1100m, DurationMin = 105 },
                new Treatment { Id = 6, Name = "Balayage", Description = "Hand-painted freehand lightening technique.", Price = 1400m, DurationMin = 120 },
                new Treatment { Id = 7, Name = "Keratin Treatment", Description = "Smoothing treatment for frizz-free, glossy hair.", Price = 1800m, DurationMin = 150 },
                new Treatment { Id = 8, Name = "Deep Conditioning Mask", Description = "Intensive repair mask with steam application.", Price = 250m, DurationMin = 30 },
                new Treatment { Id = 9, Name = "Children's Cut (<=12)", Description = "Relaxed cut for children aged 12 and under.", Price = 250m, DurationMin = 30 }
            );

            // ----------------------------------------------------------------
            // Schedules
            //
            // Coverage per weekday (at least one stylist every day):
            //   Monday:    Sofia, Isabelle
            //   Tuesday:   Sofia
            //   Wednesday: Marcus
            //   Thursday:  Sofia, Isabelle
            //   Friday:    Marcus, Isabelle
            // ----------------------------------------------------------------
            modelBuilder.Entity<Schedule>().HasData(
                // Sofia (1): Mon, Tue, Thu
                new Schedule { Id = 1, StylistId = 1, DayOfWeek = DayOfWeek.Monday, WorkStart = new TimeOnly(9, 0), WorkEnd = new TimeOnly(17, 0), LunchTime = new TimeOnly(12, 0) },
                new Schedule { Id = 2, StylistId = 1, DayOfWeek = DayOfWeek.Tuesday, WorkStart = new TimeOnly(9, 0), WorkEnd = new TimeOnly(17, 0), LunchTime = new TimeOnly(12, 0) },
                new Schedule { Id = 3, StylistId = 1, DayOfWeek = DayOfWeek.Thursday, WorkStart = new TimeOnly(9, 0), WorkEnd = new TimeOnly(17, 0), LunchTime = new TimeOnly(12, 0) },
                // Marcus (2): Wed, Fri
                new Schedule { Id = 4, StylistId = 2, DayOfWeek = DayOfWeek.Wednesday, WorkStart = new TimeOnly(9, 0), WorkEnd = new TimeOnly(17, 0), LunchTime = new TimeOnly(12, 0) },
                new Schedule { Id = 5, StylistId = 2, DayOfWeek = DayOfWeek.Friday, WorkStart = new TimeOnly(9, 0), WorkEnd = new TimeOnly(17, 0), LunchTime = new TimeOnly(12, 0) },
                // Isabelle (3): Mon, Thu, Fri
                new Schedule { Id = 6, StylistId = 3, DayOfWeek = DayOfWeek.Monday, WorkStart = new TimeOnly(9, 0), WorkEnd = new TimeOnly(17, 0), LunchTime = new TimeOnly(12, 0) },
                new Schedule { Id = 7, StylistId = 3, DayOfWeek = DayOfWeek.Thursday, WorkStart = new TimeOnly(9, 0), WorkEnd = new TimeOnly(17, 0), LunchTime = new TimeOnly(12, 0) },
                new Schedule { Id = 8, StylistId = 3, DayOfWeek = DayOfWeek.Friday, WorkStart = new TimeOnly(9, 0), WorkEnd = new TimeOnly(17, 0), LunchTime = new TimeOnly(12, 0) }
            );

            // ----------------------------------------------------------------
            // Bookings — Jun 1–15 2026
            //
            // Weekdays and working stylists:
            //   Mon Jun 1:  Sofia, Isabelle
            //   Tue Jun 2:  Sofia
            //   Wed Jun 3:  Marcus
            //   Thu Jun 4:  Sofia, Isabelle
            //   Fri Jun 5:  Marcus, Isabelle
            //   Mon Jun 8:  Sofia, Isabelle
            //   Tue Jun 9:  Sofia                   [gap — no bookings]
            //   Wed Jun 10: Marcus                  [gap — no bookings]
            //   Thu Jun 11: Sofia, Isabelle
            //   Fri Jun 12: Marcus, Isabelle
            //   Mon Jun 15: Sofia, Isabelle
            // ----------------------------------------------------------------
            modelBuilder.Entity<Booking>().HasData(

                // --- Jun 1 (Mon) — Sofia & Isabelle ---
                new Booking { Id = 1, CreatedAt = new DateTime(2026, 5, 20), StartTime = new DateTime(2026, 6, 1, 9, 0, 0), EndTime = new DateTime(2026, 6, 1, 10, 0, 0), StylistId = 1, CustomerId = 1, Status = BookingStatus.Confirmed },  // Sofia:    Emma    – Women's Cut & Blowdry (60 min)
                new Booking { Id = 2, CreatedAt = new DateTime(2026, 5, 20), StartTime = new DateTime(2026, 6, 1, 10, 0, 0), EndTime = new DateTime(2026, 6, 1, 10, 30, 0), StylistId = 1, CustomerId = 2, Status = BookingStatus.Confirmed },  // Sofia:    Lucas   – Men's Cut (30 min)
                new Booking { Id = 3, CreatedAt = new DateTime(2026, 5, 21), StartTime = new DateTime(2026, 6, 1, 14, 0, 0), EndTime = new DateTime(2026, 6, 1, 15, 30, 0), StylistId = 1, CustomerId = 5, Status = BookingStatus.Confirmed },  // Sofia:    Astrid  – Full Colour (90 min)
                new Booking { Id = 4, CreatedAt = new DateTime(2026, 5, 22), StartTime = new DateTime(2026, 6, 1, 9, 0, 0), EndTime = new DateTime(2026, 6, 1, 11, 0, 0), StylistId = 3, CustomerId = 3, Status = BookingStatus.Confirmed },  // Isabelle: Maja    – Balayage (120 min)
                new Booking { Id = 5, CreatedAt = new DateTime(2026, 5, 22), StartTime = new DateTime(2026, 6, 1, 13, 0, 0), EndTime = new DateTime(2026, 6, 1, 14, 0, 0), StylistId = 3, CustomerId = 4, Status = BookingStatus.Confirmed },  // Isabelle: Oliver  – Men's Cut + Deep Conditioning (60 min)

                // --- Jun 2 (Tue) — Sofia ---
                new Booking { Id = 6, CreatedAt = new DateTime(2026, 5, 23), StartTime = new DateTime(2026, 6, 2, 9, 0, 0), EndTime = new DateTime(2026, 6, 2, 10, 45, 0), StylistId = 1, CustomerId = 4, Status = BookingStatus.Confirmed },  // Sofia:    Oliver  – Highlights Half Head (75 min)
                new Booking { Id = 7, CreatedAt = new DateTime(2026, 5, 23), StartTime = new DateTime(2026, 6, 2, 13, 0, 0), EndTime = new DateTime(2026, 6, 2, 14, 0, 0), StylistId = 1, CustomerId = 1, Status = BookingStatus.Confirmed },  // Sofia:    Emma    – Women's Cut & Blowdry (60 min)

                // --- Jun 3 (Wed) — Marcus ---
                new Booking { Id = 8, CreatedAt = new DateTime(2026, 5, 23), StartTime = new DateTime(2026, 6, 3, 9, 0, 0), EndTime = new DateTime(2026, 6, 3, 10, 45, 0), StylistId = 2, CustomerId = 1, Status = BookingStatus.Confirmed },  // Marcus:   Emma    – Highlights Full Head (105 min)
                new Booking { Id = 9, CreatedAt = new DateTime(2026, 5, 24), StartTime = new DateTime(2026, 6, 3, 13, 0, 0), EndTime = new DateTime(2026, 6, 3, 13, 30, 0), StylistId = 2, CustomerId = 2, Status = BookingStatus.Confirmed },  // Marcus:   Lucas   – Men's Cut (30 min)
                new Booking { Id = 10, CreatedAt = new DateTime(2026, 5, 24), StartTime = new DateTime(2026, 6, 3, 15, 0, 0), EndTime = new DateTime(2026, 6, 3, 15, 30, 0), StylistId = 2, CustomerId = 5, Status = BookingStatus.Confirmed },  // Marcus:   Astrid  – Deep Conditioning Mask (30 min)

                // --- Jun 4 (Thu) — Sofia & Isabelle ---
                new Booking { Id = 11, CreatedAt = new DateTime(2026, 5, 25), StartTime = new DateTime(2026, 6, 4, 9, 0, 0), EndTime = new DateTime(2026, 6, 4, 11, 30, 0), StylistId = 1, CustomerId = 3, Status = BookingStatus.Confirmed },  // Sofia:    Maja    – Keratin Treatment (150 min)
                new Booking { Id = 12, CreatedAt = new DateTime(2026, 5, 25), StartTime = new DateTime(2026, 6, 4, 13, 0, 0), EndTime = new DateTime(2026, 6, 4, 13, 30, 0), StylistId = 1, CustomerId = 4, Status = BookingStatus.Confirmed },  // Sofia:    Oliver  – Children's Cut (30 min)
                new Booking { Id = 13, CreatedAt = new DateTime(2026, 5, 26), StartTime = new DateTime(2026, 6, 4, 9, 0, 0), EndTime = new DateTime(2026, 6, 4, 10, 0, 0), StylistId = 3, CustomerId = 2, Status = BookingStatus.Confirmed },  // Isabelle: Lucas   – Women's Cut & Blowdry (60 min)
                new Booking { Id = 14, CreatedAt = new DateTime(2026, 5, 26), StartTime = new DateTime(2026, 6, 4, 14, 0, 0), EndTime = new DateTime(2026, 6, 4, 15, 45, 0), StylistId = 3, CustomerId = 5, Status = BookingStatus.Confirmed },  // Isabelle: Astrid  – Highlights Full Head (105 min)

                // --- Jun 5 (Fri) — Marcus & Isabelle ---
                new Booking { Id = 15, CreatedAt = new DateTime(2026, 5, 26), StartTime = new DateTime(2026, 6, 5, 9, 0, 0), EndTime = new DateTime(2026, 6, 5, 11, 0, 0), StylistId = 2, CustomerId = 3, Status = BookingStatus.Confirmed },  // Marcus:   Maja    – Balayage (120 min)
                new Booking { Id = 16, CreatedAt = new DateTime(2026, 5, 27), StartTime = new DateTime(2026, 6, 5, 9, 0, 0), EndTime = new DateTime(2026, 6, 5, 10, 30, 0), StylistId = 3, CustomerId = 1, Status = BookingStatus.Confirmed },  // Isabelle: Emma    – Full Colour (90 min)
                new Booking { Id = 17, CreatedAt = new DateTime(2026, 5, 27), StartTime = new DateTime(2026, 6, 5, 13, 0, 0), EndTime = new DateTime(2026, 6, 5, 13, 30, 0), StylistId = 3, CustomerId = 4, Status = BookingStatus.Confirmed },  // Isabelle: Oliver  – Men's Cut (30 min)

                // --- Jun 8 (Mon) — Sofia (morning gap) & Isabelle ---
                new Booking { Id = 18, CreatedAt = new DateTime(2026, 5, 28), StartTime = new DateTime(2026, 6, 8, 14, 0, 0), EndTime = new DateTime(2026, 6, 8, 15, 0, 0), StylistId = 1, CustomerId = 1, Status = BookingStatus.Confirmed },  // Sofia:    Emma    – Women's Cut & Blowdry (60 min) [morning free]
                new Booking { Id = 19, CreatedAt = new DateTime(2026, 5, 28), StartTime = new DateTime(2026, 6, 8, 9, 0, 0), EndTime = new DateTime(2026, 6, 8, 10, 15, 0), StylistId = 3, CustomerId = 2, Status = BookingStatus.Confirmed },  // Isabelle: Lucas   – Highlights Half Head (75 min)
                new Booking { Id = 20, CreatedAt = new DateTime(2026, 5, 29), StartTime = new DateTime(2026, 6, 8, 13, 0, 0), EndTime = new DateTime(2026, 6, 8, 15, 0, 0), StylistId = 3, CustomerId = 5, Status = BookingStatus.Confirmed },  // Isabelle: Astrid  – Balayage (120 min)

                // --- Jun 9 (Tue) — Sofia [gap, no bookings] ---
                // --- Jun 10 (Wed) — Marcus [gap, no bookings] ---

                // --- Jun 11 (Thu) — Sofia & Isabelle ---
                new Booking { Id = 21, CreatedAt = new DateTime(2026, 5, 30), StartTime = new DateTime(2026, 6, 11, 9, 0, 0), EndTime = new DateTime(2026, 6, 11, 10, 30, 0), StylistId = 1, CustomerId = 2, Status = BookingStatus.Confirmed },  // Sofia:    Lucas   – Full Colour (90 min)
                new Booking { Id = 22, CreatedAt = new DateTime(2026, 5, 30), StartTime = new DateTime(2026, 6, 11, 13, 0, 0), EndTime = new DateTime(2026, 6, 11, 14, 0, 0), StylistId = 1, CustomerId = 3, Status = BookingStatus.Confirmed },  // Sofia:    Maja    – Women's Cut & Blowdry (60 min)
                new Booking { Id = 23, CreatedAt = new DateTime(2026, 5, 31), StartTime = new DateTime(2026, 6, 11, 10, 0, 0), EndTime = new DateTime(2026, 6, 11, 11, 30, 0), StylistId = 3, CustomerId = 4, Status = BookingStatus.Confirmed },  // Isabelle: Oliver  – Full Colour (90 min)
                new Booking { Id = 24, CreatedAt = new DateTime(2026, 5, 31), StartTime = new DateTime(2026, 6, 11, 15, 0, 0), EndTime = new DateTime(2026, 6, 11, 15, 30, 0), StylistId = 3, CustomerId = 1, Status = BookingStatus.Confirmed },  // Isabelle: Emma    – Deep Conditioning Mask (30 min)

                // --- Jun 12 (Fri) — Marcus & Isabelle ---
                new Booking { Id = 25, CreatedAt = new DateTime(2026, 6, 1), StartTime = new DateTime(2026, 6, 12, 9, 0, 0), EndTime = new DateTime(2026, 6, 12, 10, 30, 0), StylistId = 2, CustomerId = 5, Status = BookingStatus.Confirmed },  // Marcus:   Astrid  – Full Colour (90 min)
                new Booking { Id = 26, CreatedAt = new DateTime(2026, 6, 1), StartTime = new DateTime(2026, 6, 12, 9, 0, 0), EndTime = new DateTime(2026, 6, 12, 9, 30, 0), StylistId = 3, CustomerId = 4, Status = BookingStatus.Confirmed },  // Isabelle: Oliver  – Children's Cut (30 min)
                new Booking { Id = 27, CreatedAt = new DateTime(2026, 6, 2), StartTime = new DateTime(2026, 6, 12, 13, 0, 0), EndTime = new DateTime(2026, 6, 12, 14, 30, 0), StylistId = 3, CustomerId = 2, Status = BookingStatus.Confirmed },  // Isabelle: Lucas   – Highlights Half Head (75 min)

                // --- Jun 15 (Mon) — Sofia & Isabelle ---
                new Booking { Id = 28, CreatedAt = new DateTime(2026, 6, 1), StartTime = new DateTime(2026, 6, 15, 9, 0, 0), EndTime = new DateTime(2026, 6, 15, 10, 45, 0), StylistId = 1, CustomerId = 5, Status = BookingStatus.Pending },   // Sofia:    Astrid  – Highlights Full Head (105 min)
                new Booking { Id = 29, CreatedAt = new DateTime(2026, 6, 2), StartTime = new DateTime(2026, 6, 15, 14, 0, 0), EndTime = new DateTime(2026, 6, 15, 14, 30, 0), StylistId = 1, CustomerId = 4, Status = BookingStatus.Pending },   // Sofia:    Oliver  – Men's Cut (30 min)
                new Booking { Id = 30, CreatedAt = new DateTime(2026, 6, 1), StartTime = new DateTime(2026, 6, 15, 9, 0, 0), EndTime = new DateTime(2026, 6, 15, 11, 30, 0), StylistId = 3, CustomerId = 1, Status = BookingStatus.Pending }    // Isabelle: Emma    – Keratin Treatment (150 min)
            );

            // ----------------------------------------------------------------
            // Booking ↔ Treatment (join table)
            // ----------------------------------------------------------------
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Treatments)
                .WithMany(t => t.Bookings)
                .UsingEntity(j => j.HasData(
                    // Jun 1 — Sofia
                    new { BookingsId = 1, TreatmentsId = 1 },  // Emma    – Women's Cut & Blowdry
                    new { BookingsId = 2, TreatmentsId = 2 },  // Lucas   – Men's Cut
                    new { BookingsId = 3, TreatmentsId = 3 },  // Astrid  – Full Colour
                                                               // Jun 1 — Isabelle
                    new { BookingsId = 4, TreatmentsId = 6 },  // Maja    – Balayage
                    new { BookingsId = 5, TreatmentsId = 2 },  // Oliver  – Men's Cut
                    new { BookingsId = 5, TreatmentsId = 8 },  // Oliver  – Deep Conditioning Mask
                                                               // Jun 2 — Sofia
                    new { BookingsId = 6, TreatmentsId = 4 },  // Oliver  – Highlights Half Head
                    new { BookingsId = 7, TreatmentsId = 1 },  // Emma    – Women's Cut & Blowdry
                                                               // Jun 3 — Marcus
                    new { BookingsId = 8, TreatmentsId = 5 },  // Emma    – Highlights Full Head
                    new { BookingsId = 9, TreatmentsId = 2 },  // Lucas   – Men's Cut
                    new { BookingsId = 10, TreatmentsId = 8 },  // Astrid  – Deep Conditioning Mask
                                                                // Jun 4 — Sofia
                    new { BookingsId = 11, TreatmentsId = 7 },  // Maja    – Keratin Treatment
                    new { BookingsId = 12, TreatmentsId = 9 },  // Oliver  – Children's Cut
                                                                // Jun 4 — Isabelle
                    new { BookingsId = 13, TreatmentsId = 1 },  // Lucas   – Women's Cut & Blowdry
                    new { BookingsId = 14, TreatmentsId = 5 },  // Astrid  – Highlights Full Head
                                                                // Jun 5 — Marcus
                    new { BookingsId = 15, TreatmentsId = 6 },  // Maja    – Balayage
                                                                // Jun 5 — Isabelle
                    new { BookingsId = 16, TreatmentsId = 3 },  // Emma    – Full Colour
                    new { BookingsId = 17, TreatmentsId = 2 },  // Oliver  – Men's Cut
                                                                // Jun 8 — Sofia
                    new { BookingsId = 18, TreatmentsId = 1 },  // Emma    – Women's Cut & Blowdry
                                                                // Jun 8 — Isabelle
                    new { BookingsId = 19, TreatmentsId = 4 },  // Lucas   – Highlights Half Head
                    new { BookingsId = 20, TreatmentsId = 6 },  // Astrid  – Balayage
                                                                // Jun 11 — Sofia
                    new { BookingsId = 21, TreatmentsId = 3 },  // Lucas   – Full Colour
                    new { BookingsId = 22, TreatmentsId = 1 },  // Maja    – Women's Cut & Blowdry
                                                                // Jun 11 — Isabelle
                    new { BookingsId = 23, TreatmentsId = 3 },  // Oliver  – Full Colour
                    new { BookingsId = 24, TreatmentsId = 8 },  // Emma    – Deep Conditioning Mask
                                                                // Jun 12 — Marcus
                    new { BookingsId = 25, TreatmentsId = 3 },  // Astrid  – Full Colour
                                                                // Jun 12 — Isabelle
                    new { BookingsId = 26, TreatmentsId = 9 },  // Oliver  – Children's Cut
                    new { BookingsId = 27, TreatmentsId = 4 },  // Lucas   – Highlights Half Head
                                                                // Jun 15 — Sofia
                    new { BookingsId = 28, TreatmentsId = 5 },  // Astrid  – Highlights Full Head
                    new { BookingsId = 29, TreatmentsId = 2 },  // Oliver  – Men's Cut
                                                                // Jun 15 — Isabelle
                    new { BookingsId = 30, TreatmentsId = 7 }   // Emma    – Keratin Treatment
                ));
        }
    }
}