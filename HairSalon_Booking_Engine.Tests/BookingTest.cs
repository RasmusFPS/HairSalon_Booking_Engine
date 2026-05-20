using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Models.DTOs;
using Moq;
using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HairSalon_Booking_Engine.Tests
{
    [TestClass]
    public sealed class BookingTest
    {

        private Mock<IBookingService> _ibookingservice;
        private BookingController _bookingcontroller;
        [TestInitialize]
        public void setup()
        {
            _ibookingservice = new Mock<IBookingService>();
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

            _ibookingservice.Setup(service => service.GetAllAsync()).ReturnsAsync(fakebookings);

            _bookingcontroller = new BookingController(_ibookingservice.Object);

            var result = await _bookingcontroller.GetAll();
            var obj = result.Result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
           
        }
    }
}
