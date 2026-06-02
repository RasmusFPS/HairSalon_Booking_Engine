using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;

namespace HairSalon_Booking_Engine.Tests.ServiceTests;

[TestClass]
public class BookingServiceTest
{
    [TestMethod]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectBooking()
    {
        await using var ctx = DbContextFactory.Create(nameof(GetByIdAsync_ExistingId_ReturnsCorrectBooking));

        var fakeCustomer = TestDataBuilder.CreateCustomer(id: 1);

        var fakeStylist = TestDataBuilder.CreateStylist(id:1);

        ctx.Customers.Add(fakeCustomer);
        ctx.Stylist.Add(fakeStylist);


        var fakeDbBooking = TestDataBuilder.CreateBooking(id: 1, stylistId: 1, customerId: 1);

        ctx.Bookings.Add(fakeDbBooking);
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);

        var result = await service.GetByIdAsync(fakeDbBooking.Id);

        Assert.IsNotNull(result);

        Assert.AreEqual(fakeDbBooking.CustomerId, result.Customer.Id);
        Assert.AreEqual(fakeDbBooking.StylistId, result.Stylist.Id);
        Assert.AreEqual(fakeDbBooking.StartTime, result.StartTime);
    }

    [TestMethod]
    public async Task UpdateAsync_ExistingBooking_UpdatesFields()
    {
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(UpdateAsync_ExistingBooking_UpdatesFields));

        var treatment = TestDataBuilder.CreateTreatment();
        var booking = TestDataBuilder.CreateBooking(id: 1, stylistId: 1, customerId: 1);
        booking.Treatments.Add(treatment);

        ctx.Bookings.Add(booking);
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);
        var updatedTime = DateTime.Now.AddDays(1);
        var updateRequest = new UpdateBookingRequest(
            StartTime: updatedTime, 
            StylistId: 1, 
            CustomerId: 2);

        //Act
        var result = await service.UpdateAsync(1, updateRequest);

        //Assert
        Assert.IsTrue(result.Success);
        var updated = await ctx.Bookings.FindAsync(1);
        Assert.AreEqual(updatedTime, updated!.StartTime);
        Assert.AreEqual(updatedTime.AddMinutes(45), updated!.EndTime);
        Assert.AreEqual(1, updated!.StylistId);
        Assert.AreEqual(2, updated!.CustomerId);
    }

    [TestMethod]
    public async Task UpdateAsync_NonExistingId_ReturnsNotFound()
    {
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(UpdateAsync_NonExistingId_ReturnsNotFound));
        var service = new BookingService(ctx);
        var updateRequest = new UpdateBookingRequest(DateTime.Now, 1, 1);

        //Act
        var result = await service.UpdateAsync(999, updateRequest);

        //Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(ServiceResultStatus.NotFound, result.Status);
    }

    [TestMethod]
    public async Task DeleteAsync_ExistingBooking_RemovesFromDatabase()
    {
        await using var ctx = DbContextFactory.Create(nameof(DeleteAsync_ExistingBooking_RemovesFromDatabase));
        var fakeDbBooking = TestDataBuilder.CreateBooking(id: 1, stylistId: 1, customerId: 1);

        ctx.Bookings.Add(fakeDbBooking);
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);

        var IdToDelete = await service.DeleteAsync(1);

        var result = await service.GetByIdAsync(1);

        Assert.IsNull(result);

    }

    [TestMethod]
    public async Task CreateAsync_ReturnsCreatedBookingData()
    {
        var fakeTreatmentsId = new List<int> { 1, 2 };

        await using var ctx = DbContextFactory.Create(nameof(CreateAsync_ReturnsCreatedBookingData));

        await ctx.Customers.AddAsync(new Customer { Id = 1, FirstName = "Test", LastName = "Customer", Email = "c@test.com", Phone = "000" });
        await ctx.Stylist.AddAsync(new Stylist { Id = 1, FirstName = "Test", LastName = "Stylist" });
        await ctx.Treatments.AddAsync(new Treatment { Id = 1, Name = "Treatment 1" });
        await ctx.Treatments.AddAsync(new Treatment { Id = 2, Name = "Treatment 2" });
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);

        var request = new CreateBookingRequest(
            StartTime: DateTime.Now.AddDays(1),
            StylistId: 1,
            CustomerId: 1,
            TreatmentIds: fakeTreatmentsId
        );

        var result = await service.CreateAsync(request);
        Assert.IsNotNull(result);

        var savedBooking = await ctx.Bookings.FindAsync(result.Data!.Id);
        Assert.IsNotNull(savedBooking);
    }
}
