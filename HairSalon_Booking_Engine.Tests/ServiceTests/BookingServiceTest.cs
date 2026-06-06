using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;

namespace HairSalon_Booking_Engine.Tests.ServiceTests
{
    [TestClass]
    public class BookingServiceTest
    {
        [TestMethod]
        public async Task GetAllAsync_ReturnsCorrectBookings()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(nameof(GetAllAsync_ReturnsCorrectBookings));

            ctx.Customers.Add(TestDataBuilder.CreateCustomer());
            ctx.Stylist.Add(TestDataBuilder.CreateStylist());

            var bookings = TestDataBuilder.CreateBookingList();
            ctx.Bookings.AddRange(bookings);
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

            ctx.Customers.Add(TestDataBuilder.CreateCustomer());
            ctx.Stylist.Add(TestDataBuilder.CreateStylist());
            ctx.Bookings.Add(TestDataBuilder.CreateBooking());
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);

            //Act
            var result = await service.GetByIdAsync(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Customer.Id);
            Assert.AreEqual(1, result.Stylist.Id);
        }

        [TestMethod]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(nameof(GetByIdAsync_NonExistingId_ReturnsNull));
            var service = new BookingService(ctx);

            //Act
            var result = await service.GetByIdAsync(999);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task CreateAsync_ValidRequest_SavesBookingWithCorrectData()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(
                nameof(CreateAsync_ValidRequest_SavesBookingWithCorrectData));

            ctx.Customers.Add(TestDataBuilder.CreateCustomer());
            ctx.Stylist.Add(TestDataBuilder.CreateStylist());
            ctx.Treatments.Add(TestDataBuilder.CreateTreatment(id: 1));
            ctx.Treatments.Add(TestDataBuilder.CreateTreatment(id: 2));
            ctx.Schedules.Add(TestDataBuilder.CreateSchedule());
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);
            var startTime = DateTime.UtcNow.Date.AddDays(1).AddHours(10);

            var request = new CreateBookingRequest(
                StartTime: startTime,
                StylistId: 1,
                CustomerId: 1,
                TreatmentIds: [1, 2]
            );

            //Act
            var result = await service.CreateAsync(request);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);

            var saved = await ctx.Bookings.FindAsync(result.Data.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(startTime, saved.StartTime);
            Assert.AreEqual(startTime.AddMinutes(90), saved.EndTime);
            Assert.AreEqual(BookingStatus.Pending, saved.Status);
        }

        [TestMethod]
        public async Task CreateAsync_InvalidTreatmentId_ReturnsValidationError()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(
                nameof(CreateAsync_InvalidTreatmentId_ReturnsValidationError));

            ctx.Customers.Add(TestDataBuilder.CreateCustomer());
            ctx.Stylist.Add(TestDataBuilder.CreateStylist());
            ctx.Schedules.Add(TestDataBuilder.CreateSchedule());
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);

            var request = new CreateBookingRequest(
                StartTime: DateTime.Now.AddDays(1).AddHours(10),
                StylistId: 1,
                CustomerId: 1,
                TreatmentIds: [999]
            );

            //Act
            var result = await service.CreateAsync(request);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ServiceResultStatus.ValidationError, result.Status);
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
            ctx.Schedules.Add(TestDataBuilder.CreateSchedule());
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
            Assert.AreEqual(updatedTime.AddMinutes(45), updated.EndTime);
            Assert.AreEqual(1, updated.StylistId);
            Assert.AreEqual(2, updated.CustomerId);
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
        [DataRow(BookingStatus.Completed)]
        [DataRow(BookingStatus.Cancelled)]
        public async Task UpdateAsync_CompletedOrCancelledBooking_ReturnsValidationError(BookingStatus status)
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(
                $"{nameof(UpdateAsync_CompletedOrCancelledBooking_ReturnsValidationError)}_{status}");

            var booking = TestDataBuilder.CreateBooking(status: status);
            ctx.Bookings.Add(booking);
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);
            var updateRequest = new UpdateBookingRequest(DateTime.Now.AddDays(1), 1, 1);

            //Act
            var result = await service.UpdateAsync(1, updateRequest);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ServiceResultStatus.ValidationError, result.Status);
        }

        [TestMethod]
        public async Task DeleteAsync_ExistingBooking_RemovesFromDatabase()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(nameof(DeleteAsync_ExistingBooking_RemovesFromDatabase));
            ctx.Bookings.Add(TestDataBuilder.CreateBooking());
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);

            //Act
            await service.DeleteAsync(1);
            var result = await service.GetByIdAsync(1);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAvailableTimesAsync_ReturnsCorrectTimes()
        {
            await using var ctx = DbContextFactory.Create(nameof(GetAvailableTimesAsync_ReturnsCorrectTimes));

            var tomorrow = DateTime.Today.AddDays(1);
            var testDate = DateOnly.FromDateTime(tomorrow);

            int testStylistId = 1;

            var schedule = new Schedule
            {
                Id = 1,
                StylistId = testStylistId,
                DayOfWeek = tomorrow.DayOfWeek,
                WorkStart = new TimeOnly(9, 0),
                WorkEnd = new TimeOnly(17, 0),
                LunchTime = new TimeOnly(12, 0)
            };

            var booking1 = new Booking { Id = 1, CreatedAt = new DateTime(2026, 5, 20), StartTime = tomorrow.AddHours(9), EndTime = tomorrow.AddHours(10), StylistId = testStylistId, CustomerId = 1, Status = BookingStatus.Confirmed };
            var booking2 = new Booking { Id = 2, CreatedAt = new DateTime(2026, 5, 20), StartTime = tomorrow.AddHours(10), EndTime = tomorrow.AddHours(11), StylistId = testStylistId, CustomerId = 2, Status = BookingStatus.Confirmed };

            ctx.Schedules.Add(schedule);
            ctx.Bookings.AddRange(booking1, booking2);
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);

            var result = await service.GetAvailableTimesAsync(testDate, testStylistId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success, "Service method failed to return success.");

            var responseDto = result.Data;
            Assert.IsNotNull(responseDto);

            var theList = responseDto.AvailableTimes;

            Assert.IsFalse(theList.Contains(new TimeOnly(9, 0)), "09:00 is booked, it should NOT be available.");
            Assert.IsFalse(theList.Contains(new TimeOnly(10, 0)), "10:00 is booked, it should NOT be available.");
            Assert.IsTrue(theList.Contains(new TimeOnly(11, 0)), "11:00 should be free and available.");
        }

        [TestMethod]
        public async Task DeleteAsync_NonExistingId_ReturnsNotFound()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(nameof(DeleteAsync_NonExistingId_ReturnsNotFound));
            var service = new BookingService(ctx);

            //Act
            var result = await service.DeleteAsync(1);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ServiceResultStatus.NotFound, result.Status);
        }

        [TestMethod]
        [DataRow(BookingStatus.Pending)]
        [DataRow(BookingStatus.Confirmed)]
        [DataRow(BookingStatus.Completed)]
        [DataRow(BookingStatus.Cancelled)]
        [DataRow(BookingStatus.NoShow)]
        public async Task ChangeStatusAsync_ExistingBooking_SetsBookingStatus(BookingStatus status)
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(
                $"{nameof(ChangeStatusAsync_ExistingBooking_SetsBookingStatus)}_{status}");

            ctx.Customers.Add(TestDataBuilder.CreateCustomer());
            ctx.Stylist.Add(TestDataBuilder.CreateStylist());
            ctx.Bookings.Add(TestDataBuilder.CreateBooking());
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);

            //Act
            await service.ChangeStatusAsync(1, status);
            var result = await service.GetByIdAsync(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(status, result.Status);
        }

        [TestMethod]
        public async Task CancelAsync_ExistingBooking_SetsStatusToCancelled()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(
                nameof(CancelAsync_ExistingBooking_SetsStatusToCancelled));

            ctx.Customers.Add(TestDataBuilder.CreateCustomer());
            ctx.Stylist.Add(TestDataBuilder.CreateStylist());
            ctx.Bookings.Add(TestDataBuilder.CreateBooking());
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);

            //Act
            await service.CancelAsync(1);
            var result = await service.GetByIdAsync(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(BookingStatus.Cancelled, result.Status);
        }

        [TestMethod]
        public async Task CancelAsync_AlreadyCancelledBooking_ReturnsValidationError()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(
                nameof(CancelAsync_AlreadyCancelledBooking_ReturnsValidationError));

            ctx.Bookings.Add(TestDataBuilder.CreateBooking(status: BookingStatus.Cancelled));
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);

            //Act
            var result = await service.CancelAsync(1);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ServiceResultStatus.ValidationError, result.Status);
        }

        [TestMethod]
        public async Task CancelAsync_CompletedBooking_ReturnsValidationError()
        {
            //Arrange
            await using var ctx = DbContextFactory.Create(
                nameof(CancelAsync_CompletedBooking_ReturnsValidationError));

            ctx.Bookings.Add(TestDataBuilder.CreateBooking(status: BookingStatus.Completed));
            await ctx.SaveChangesAsync();

            var service = new BookingService(ctx);

            //Act
            var result = await service.CancelAsync(1);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ServiceResultStatus.ValidationError, result.Status);
        }
    }
}