using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;

namespace HairSalon_Booking_Engine.Tests.ServiceTests;

[TestClass]
public class BookingServiceTest
{
    [TestMethod]
    public async Task GetAllAsync_ReturnsCorrectBookings()
    {
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(GetAllAsync_ReturnsCorrectBookings));

        var customer = TestDataBuilder.CreateCustomer();
        var stylist = TestDataBuilder.CreateStylist();

        await ctx.Customers.AddAsync(customer);
        await ctx.Stylist.AddAsync(stylist);

        var bookings = TestDataBuilder.CreateBookingList();

        foreach (var booking in bookings)
        {
            await ctx.Bookings.AddAsync(booking);
        }
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);

        //Act
        var result = await service.GetAllAsync();

        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(bookings.Count, result.Count());
    }

    [TestMethod]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectBooking()
    {
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(GetByIdAsync_ExistingId_ReturnsCorrectBooking));

        var customer = TestDataBuilder.CreateCustomer();
        var stylist = TestDataBuilder.CreateStylist();

        ctx.Customers.Add(customer);
        ctx.Stylist.Add(stylist);

        var booking = TestDataBuilder.CreateBooking();

        ctx.Bookings.Add(booking);
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);

        //Act
        var result = await service.GetByIdAsync(booking.Id);

        //Assert
        Assert.IsNotNull(result);

        Assert.AreEqual(booking.CustomerId, result.Customer.Id);
        Assert.AreEqual(booking.StylistId, result.Stylist.Id);
        Assert.AreEqual(booking.StartTime, result.StartTime);
    }

    [TestMethod]
    public async Task CreateAsync_ReturnsCreatedBookingData()
    {
        await using var ctx = DbContextFactory.Create(nameof(CreateAsync_ReturnsCreatedBookingData));

        await ctx.Customers.AddAsync(TestDataBuilder.CreateCustomer());
        await ctx.Stylist.AddAsync(TestDataBuilder.CreateStylist());
        await ctx.Treatments.AddAsync(TestDataBuilder.CreateTreatment(id: 1));
        await ctx.Treatments.AddAsync(TestDataBuilder.CreateTreatment(id: 2));
        await ctx.Schedules.AddAsync(TestDataBuilder.CreateSchedule());
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);

        var request = new CreateBookingRequest(
            StartTime: DateTime.UtcNow.Date.AddDays(1).AddHours(10),
            StylistId: 1,
            CustomerId: 1,
            TreatmentIds: [1, 2]
        );

        var result = await service.CreateAsync(request);
        Assert.IsNotNull(result);

        var savedBooking = await ctx.Bookings.FindAsync(result.Data!.Id);
        Assert.IsNotNull(savedBooking);
    }

    [TestMethod]
    public async Task UpdateAsync_ExistingBooking_UpdatesFields()
    {
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(UpdateAsync_ExistingBooking_UpdatesFields));

        var treatment = TestDataBuilder.CreateTreatment();
        var booking = TestDataBuilder.CreateBooking();
        booking.Treatments.Add(treatment);

        ctx.Bookings.Add(booking);
        await ctx.Schedules.AddAsync(TestDataBuilder.CreateSchedule());
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);
        var updatedTime = DateTime.UtcNow.Date.AddDays(1).AddHours(10);

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
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(DeleteAsync_ExistingBooking_RemovesFromDatabase));
        var booking = TestDataBuilder.CreateBooking();

        ctx.Bookings.Add(booking);
        await ctx.SaveChangesAsync();

        var service = new BookingService(ctx);

        //Act
        await service.DeleteAsync(1);
        var result = await service.GetByIdAsync(1);

        //Assert
        Assert.IsNull(result);
    }
}
