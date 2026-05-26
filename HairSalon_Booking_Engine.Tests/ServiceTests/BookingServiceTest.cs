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

    [TestMethod]
    public async Task UpdateAsync_ExistingBooking_UpdatesFields()
    {
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(UpdateAsync_ExistingBooking_UpdatesFields));
        ctx.Bookings.Add(TestDataBuilder.CreateBooking(id: 1, stylistId: 2, customerId: 3));
        await ctx.SaveChangesAsync();
        var service = new BookingService(ctx);

        var updatedTime = DateTime.Now.AddDays(1);
        var updateRequest = new CreateBookingRequest(DateTime.Now, updatedTime, 1, 2);

        //Act
        var result = await service.UpdateAsync(1, updateRequest);

        //Assert
        Assert.IsTrue(result.Success);
        var updated = await ctx.Bookings.FindAsync(1);
        Assert.AreEqual(updatedTime, updated!.StartTime);
        Assert.AreEqual(1, updated!.StylistId);
        Assert.AreEqual(2, updated!.CustomerId);
    }

    [TestMethod]
    public async Task UpdateAsync_NonExistingId_ReturnsNotFound()
    {
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(UpdateAsync_NonExistingId_ReturnsNotFound));
        var service = new BookingService(ctx);
        var updateRequest = new CreateBookingRequest(DateTime.Now, DateTime.Now, 1, 1);

        //Act
        var result = await service.UpdateAsync(999, updateRequest);

        //Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(ServiceResultStatus.NotFound, result.Status);
    }
}
