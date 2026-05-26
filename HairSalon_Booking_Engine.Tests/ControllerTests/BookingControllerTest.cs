using FluentValidation;
using HairSalon_Booking_Engine.Controllers;
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
        public async Task GetAllBooking_ShouldReturnOkResult_WithListOfBookings()
        {
            //

            //fake data
           var fakebookings = new List<GetBookingResponse>
           {
                new(new DateTime(2025, 5, 1), new DateTime(2025, 5, 12, 10, 0, 0), 1, 1),
                new(new DateTime(2025, 5, 2), new DateTime(2025, 5, 12, 11, 0, 0), 1, 2)
           };

            _serviceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(fakebookings);

            var result = await _controller.GetAll();
            var obj = result.Result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
           
        }
    }
}
