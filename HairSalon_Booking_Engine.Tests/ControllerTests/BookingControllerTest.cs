using FluentValidation;
using HairSalon_Booking_Engine.Controllers;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;
using HairSalon_Booking_Engine.Validation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HairSalon_Booking_Engine.Tests.ControllerTests
{
    [TestClass]
    public sealed class BookingControllerTest
    {
        private Mock<IBookingService> _serviceMock = null!;
        private IValidator<CreateBookingRequest> _createValidator = null!;
        private IValidator<UpdateBookingRequest> _updateValidator = null!;
        private BookingController _controller = null!;

        [TestInitialize]
        public void setup()
        {
            _serviceMock = new Mock<IBookingService>();
            _createValidator = new CreateBookingValidator();
            _updateValidator = new UpdateBookingValidator();
            _controller = new BookingController(_serviceMock.Object, _createValidator, _updateValidator);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkWithBookings()
        {
            var testBookings = TestDataBuilder.CreateGetBookingResponseList();

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
            var booking = TestDataBuilder.CreateGetBookingResponse();

            _serviceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(booking);

            var actionResult = await _controller.GetById(1);

            var result = actionResult.Result as OkObjectResult;

            //if result isnt Null That means it returns Ok200 status code
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            _serviceMock
                .Setup(s => s.GetByIdAsync(9999))
                .ReturnsAsync((GetBookingResponse?)null);

            var actionResult = await _controller.GetById(9999);

            var result = actionResult.Result;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task Create_ValidRequest_ReturnsCreatedAtAction()
        {
            var bookingTime = DateTime.Now;
            var request = new CreateBookingRequest(bookingTime, 1, 1, [1, 2]);
            var createdBooking = TestDataBuilder.CreateGetBookingResponse();

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
        public async Task Create_InvalidRequest_ReturnsBadRequestWithErrors()
        {
            var invalidRequest = new CreateBookingRequest(DateTime.Now.AddDays(-1), 0, 0, []);

            var actionResult = await _controller.Create(invalidRequest);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Update_ExistingBooking_ReturnsNoContent()
        {
            var request = new UpdateBookingRequest(DateTime.Now, 2, 3, [2, 3]);
            _serviceMock
                .Setup(s => s.UpdateAsync(1, request))
                .ReturnsAsync(ServiceResult.Ok());

            var actionResult = await _controller.Update(1, request);

            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Update_NonExistingBooking_ReturnsNotFound()
        {
            var request = new UpdateBookingRequest(DateTime.Now, 2, 3, [2, 3]);
            _serviceMock
                .Setup(s => s.UpdateAsync(999, request))
                .ReturnsAsync(ServiceResult.NotFound("Ingen bokning hittades med ID: 999"));

            var actionResult = await _controller.Update(999, request);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteByID_ExistingBooking_ReturnsNoContent()
        {
            _serviceMock
                .Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(new ServiceResult(ServiceResultStatus.Success));

            var actionResult = await _controller.DeleteByID(1);

            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteById_NonExistingBooking_ReturnsNotFound()
        {
            _serviceMock
                .Setup(s => s.DeleteAsync(999))
                .ReturnsAsync(new ServiceResult(ServiceResultStatus.NotFound));

            var actionResult = await _controller.DeleteByID(999);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task GetAvailableTimes_ValidRequest_ReturnsOk()
        {
            var testDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            int testStylistId = 1;

            var fakeTimesList = new List<TimeOnly> { new TimeOnly(10, 0), new TimeOnly(11, 0) };
            var fakeResponseDto = new GetAvailableTimesResponse(testDate, testStylistId,fakeTimesList);

            _serviceMock
                .Setup(s => s.GetAvailableTimesAsync(testDate, testStylistId))
                .ReturnsAsync(ServiceResult<GetAvailableTimesResponse>.Ok(fakeResponseDto));

            var actionResult = await _controller.GetAvailableTimes(testDate, testStylistId);

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAvailableTimes_StylistNotWorking_ReturnsBadRequest()
        {
            var testDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            int nonExistingStylistId = 999;

            _serviceMock
                .Setup(s => s.GetAvailableTimesAsync(testDate, nonExistingStylistId))
                .ReturnsAsync(new ServiceResult<GetAvailableTimesResponse>(ServiceResultStatus.NotFound));

            var actionResult = await _controller.GetAvailableTimes(testDate, nonExistingStylistId);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
    }
}
