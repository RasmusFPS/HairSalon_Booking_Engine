using HairSalon_Booking_Engine.Models;

namespace HairSalon_Booking_Engine.Tests.TestData
{
    /// <summary>
    /// Denna klassen kommer endast vara användbar för Service tester.<br />
    /// De är de enda testerna som behöver ha direkt åtkomst
    /// till databas modeller.
    /// </summary>
    public static class TestDataBuilder
    {
        public static Customer CreateCustomer(int id = 1) => new()
        {
            Id = id,
            FirstName = "Anna",
            LastName = "Johansson",
            Phone = "+46701234567",
            Email = "anna.johansson@email.com"
        };

        public static Stylist CreateStylist(int id = 1) => new()
        {
            Id = id,
            FirstName = "Erik",
            LastName = "Lindgren"
        };

        public static Treatment CreateTreatment(int id = 1) => new()
        {
            Id = id,
            Name = "Haircut",
            Description = "Classic haircut",
            Price = 350.00m,
            DurationMin = 45
        };

        public static Booking CreateBooking(int id = 1, int stylistId = 1, int customerId = 1) => new()
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            StartTime = DateTime.UtcNow.AddDays(1),
            StylistId = stylistId,
            CustomerId = customerId,
            Status = BookingStatus.Pending
        };

        public static Schedule CreateSchedule(int id = 1, int stylistId = 1) => new()
        {
            Id = id,
            StartTime = DateTime.UtcNow.AddDays(1).Date.AddHours(9),
            EndTime = DateTime.UtcNow.AddDays(1).Date.AddHours(10),
            Available = true,
            Notes = null,
            StylistId = stylistId
        };

        /// <summary>
        /// Creates a collection of customers with unique IDs and email addresses.
        /// </summary>
        /// <param name="count">Number of customers to create. Must be positive.</param>
        /// <exception cref="ArgumentException">Thrown when count is less than 1.</exception>
        public static List<Customer> CreateCustomerList(int count = 3)
        {
            if (count < 1)
                throw new ArgumentException("Count must be at least 1", nameof(count));

            return Enumerable.Range(1, count)
                .Select(i =>
                {
                    var customer = CreateCustomer(i);
                    customer.FirstName = $"Customer{i}";
                    customer.Email = $"customer{i}@email.com";
                    return customer;
                })
                .ToList();
        }

        /// <summary>
        /// Creates a collection of bookings with unique IDs and relationships.
        /// <param name="count">Number of bookings to create. must be positive.</param>>
        /// <exception cref="ArgumentException">Thrown when count is less than 1.</exception>
        /// </summary>
        public static List<Booking> CreateBookingList(int count = 3)
        {
            if (count < 1)
                throw new ArgumentException("Count must be at least 1", nameof(count));

            return Enumerable.Range(1, count)
                .Select(i => CreateBooking(id: i, stylistId: (i % 3) + 1, customerId: i))
                .ToList();
        }

        /// <summary>
        /// Creates a collection of schedules with unique IDs and relationships
        /// </summary>
        /// <param name="count">Number of schedules to create. must be positive.</param>
        /// <param name="stylistId">Id of the stylist tied to this schedule.</param>
        /// <exception cref="ArgumentException">Thrown when count is less than 1.</exception>
        /// <returns></returns>
        public static List<Schedule> CreateScheduleList(int count = 3, int stylistId = 1)
        {
            if (count < 1)
                throw new ArgumentException("Count must be at least 1", nameof(count));

            return Enumerable.Range(1, count)
                .Select(i => CreateSchedule(id: i, stylistId: stylistId))
                .ToList();
        }
    }
}
