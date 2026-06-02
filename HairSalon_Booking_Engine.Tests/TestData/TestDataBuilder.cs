using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;

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
            Status = BookingStatus.Pending,
            Treatments = new List<Treatment>()
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

        public static GetCustomerResponse CreateGetCustomerResponse(int id = 1)
        {
            return new GetCustomerResponse(
                id, "Anna", "Johansson", "+46701234567", "anna.johansson@email.com");
        }

        public static GetStylistResponse CreateGetStylistResponse(int id = 1)
        {
            return new GetStylistResponse(id, "Erik", "Lindgren");
        }

        public static GetTreatmentResponse CreateGetTreatmentResponse(int id = 1)
        {
            return new GetTreatmentResponse(id, "Haircut", "Classic haircut", 350.00m, 45);
        }

        public static GetBookingResponse CreateGetBookingResponse(int id = 1)
        {
            return new GetBookingResponse(
                id,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now.AddDays(1),
                BookingStatus.Pending,
                new(1, "Erik", "Lindgren"),
                new(1, "Anna", "Johansson", "+46701234567", "anna.johansson@email.com"),
                new List<GetTreatmentResponse>()
            );
        }

        public static GetScheduleResponse CreateGetScheduleResponse(int id = 1)
        {
            return new GetScheduleResponse(
                id, 
                DateTime.Now, 
                DateTime.Now.AddMinutes(60), 
                true, 
                null, 
                new GetStylistResponse(1, "Erik", "Lindgren")
            );
        }

        public static List<GetCustomerResponse> CreateGetCustomerResponseList(int count = 3)
        {
            if (count < 1)
                throw new ArgumentException("Count must be at least 1", nameof(count));

            return Enumerable.Range(1, count)
                .Select(i => new GetCustomerResponse(
                    i,
                    $"Customer{i}",
                    "Berg",
                    $"+4670123456{i}",
                    $"customer{i}@email.com"
                ))
                .ToList();
        }

        public static List<GetBookingResponse> CreateGetBookingResponseList(int count = 3)
        {
            if (count < 1)
                throw new ArgumentException("Count must be at least 1", nameof(count));

            return Enumerable.Range(1, count)
                .Select(i => {
                    //Fake data
                    var fakeStylist = new GetStylistResponse(i, $"Stylist{i}", "Johansson");
                    var fakeCustomer = new GetCustomerResponse(i, $"Customer{i}", "Berg", $"+4670123456{i}", $"customer{i}@email.com");
                    var fakeTreatments = new List<GetTreatmentResponse>();

                    return new GetBookingResponse(
                        i,                          
                        DateTime.Now,               
                        DateTime.Now.AddDays(i),  
                        DateTime.Now.AddDays(i).AddHours(1),
                        BookingStatus.Confirmed,
                        fakeStylist,                
                        fakeCustomer,
                        fakeTreatments
                    );
                })
                .ToList();
        }
    }
}
