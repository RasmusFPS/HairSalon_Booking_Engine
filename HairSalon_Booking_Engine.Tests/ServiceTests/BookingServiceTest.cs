using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;
using System.Threading.Tasks;

namespace HairSalon_Booking_Engine.Tests.ServiceTests;

[TestClass]
public class BookingServiceTest
{
    [TestMethod]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectBooking()
    {
        await using var ctx = DbContextFactory.Create(nameof(GetByIdAsync_ExistingId_ReturnsCorrectBooking));

        var fakeDbBooking = TestDataBuilder.CreateBooking(id: 1, stylistId: 2, customerId: 3);

        ctx.Bookings.Add(fakeDbBooking);
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);

        var result = await service.GetByIdAsync(fakeDbBooking.Id);

        Assert.IsNotNull(result);

        Assert.AreEqual(fakeDbBooking.CustomerId, result.CustomerId);
        Assert.AreEqual(fakeDbBooking.StylistId, result.StylistId);
        Assert.AreEqual(fakeDbBooking.StartTime, result.StartTime);
    }
}
