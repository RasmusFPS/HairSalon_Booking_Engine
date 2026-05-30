using FluentValidation;
using HairSalon_Booking_Engine.Controllers;
using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;
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

        [TestMethod]
        public async Task GetAll_ReturnsOkWithBookings()
        {
            var testBookings = TestDataBuilder.CreateBookingResponseList();

            _serviceMock
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(testBookings);

            var actionResult = await _controller.GetAll();

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedBookings = okResult.Value as IEnumerable<GetBookingResponse>;
            Assert.IsNotNull(returnedBookings);
            Assert.AreEqual(testBookings.Count, returnedBookings.Count());
        }

        [TestMethod]
        public async Task GetById_ExistingId_ReturnsOk()
        {
            var fakeBooking = new GetBookingResponse(
                1,
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
            );

            _serviceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(fakeBooking);

            var actionResult = await _controller.GetById(1);

            var result = actionResult.Result as OkObjectResult;

            //if result isnt Null That means it returns Ok200 status code
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task DeleteByID_ExistingBooking_ReturnsNoContent()
        {
            int idToDelete = 1;

            _serviceMock
                .Setup(s => s.DeleteAsync(idToDelete))
                .ReturnsAsync(new ServiceResult(ServiceResultStatus.Success));

            var actionResult = await _controller.DeleteByID(idToDelete);

            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}
