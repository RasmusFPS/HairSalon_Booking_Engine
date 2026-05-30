using FluentValidation;
using HairSalon_Booking_Engine.Controllers;
using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HairSalon_Booking_Engine.Tests.ControllerTests
{
    [TestClass]
    public sealed class BookingControllerTest
    {
        private Mock<IBookingService> _serviceMock = null!;
        private Mock<IValidator<CreateBookingRequest>> _validatorMock = null!;
        private BookingController _controller = null!;

        [TestInitialize]
        public void setup()
        {
            _serviceMock = new Mock<IBookingService>();
            _validatorMock = new Mock<IValidator<CreateBookingRequest>>();
            _controller = new BookingController(_serviceMock.Object, _validatorMock.Object);
        }

        [TestMethod]
        public async Task GetAllBooking_ReturnsOkWithBookings()
        {
            //Arrange
            var bookings = new List<GetBookingResponse>
            {
                new(1,
                    DateTime.Today,
                    DateTime.Today.AddDays(1),
                    DateTime.Today.AddDays(1).AddHours(1),
                    BookingStatus.Pending,
                    new GetStylistResponse(1, "Stylist", "Name"),
                    new GetCustomerResponse(1, "Customer", "Name", "+46701234567", "customer@email.se"),
                    new List<GetTreatmentResponse>() 
                    {
                        new GetTreatmentResponse(1, "haircut", "haircut", 120, 120)
                    }
                )

            };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(bookings);

            //Act
            var actionResult = await _controller.GetAll();

            //Assert
            var ok = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(ok);
            Assert.AreEqual(bookings, ok.Value);
        }
    }
}
