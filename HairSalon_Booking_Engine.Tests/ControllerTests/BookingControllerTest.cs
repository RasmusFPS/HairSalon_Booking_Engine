using FluentValidation;
using FluentValidation.Results;
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
        public async Task Create_ValidRequest_ReturnsCreatedAtAction()
        {
            var bookingTime = DateTime.Now;
            var request = new CreateBookingRequest(bookingTime, 1, 1, [1, 2]);
            var createdBooking = new GetBookingResponse(
                Id: 1, 
                CreatedAt: bookingTime, 
                StartTime: bookingTime, 
                EndTime: bookingTime.AddMinutes(90), 
                Status: BookingStatus.Pending, 
                Stylist: new(1, "Sofia", "Andersson"), 
                Customer: new(1, "Emma", "Johansson", "070-123 45 67", "emma.johansson@example.com"), 
                Treatments: new List<GetTreatmentResponse>()
                {
                    new(1, "Women's Cut & Blowdry", "Precision cut with a full blowdry finish.", 650m, 60),
                    new(2, "Men's Cut", "Classic scissor or clipper cut.", 350m, 30)
                });

            _validatorMock
                .Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new ValidationResult());

            _serviceMock
                .Setup(s => s.CreateAsync(request))
                .ReturnsAsync(ServiceResult<GetBookingResponse>.Ok(createdBooking));

            var actionResult = await _controller.Create(request);

            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtActionResult));
            var createdResult = actionResult as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(nameof(CustomerController.GetById), createdResult.ActionName);
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

        [TestMethod]
        public async Task DeleteById_NonExistingBooking_ReturnsNotFound()
        {
            int nonExistingId = 999;

            _serviceMock
                .Setup(s => s.DeleteAsync(nonExistingId))
                .ReturnsAsync(new ServiceResult(ServiceResultStatus.NotFound));

            var actionResult = await _controller.DeleteByID(nonExistingId);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }
    }
}
