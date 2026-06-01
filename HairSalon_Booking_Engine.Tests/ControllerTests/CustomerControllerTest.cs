using FluentValidation;
using HairSalon_Booking_Engine.Controllers;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Models.DTOs.Validation;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;
using HairSalon_Booking_Engine.Validation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HairSalon_Booking_Engine.Tests.ControllerTests
{
    [TestClass]
    public class CustomerControllerTest
    {
        private Mock<ICustomerService> _serviceMock = null!;
        private IValidator<CreateCustomerRequest> _createValidator = null!;
        private IValidator<UpdateCustomerRequest> _updateValidator = null!;
        private CustomerController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<ICustomerService>();
            _createValidator = new CreateCustomerValidator();
            _updateValidator = new UpdateCustomerValidator();
            _controller = new CustomerController(_serviceMock.Object, _createValidator, _updateValidator);
        }

        [TestMethod]
        [DataRow("Anna", "Berg", "+46701234567", null)] // Valid data
        [DataRow("Jo", "Smith", "+1234567890", "jo@example.com")]  // Minimum length names
        [DataRow("Jean-Pierre", "O'Brien", "+358912345678", "test@email.com")]  // Special chars in names
        public async Task Create_ValidRequest_ReturnsCreatedAtAction(string firstName, string lastName, string phone, string? email)
        {
            var request = new CreateCustomerRequest(firstName, lastName, phone, email);
            var createdCustomer = new GetCustomerResponse(1, firstName, lastName, phone, email);

            _serviceMock
                .Setup(s => s.CreateAsync(request))
                .ReturnsAsync(ServiceResult<GetCustomerResponse>.Ok(createdCustomer));

            var actionResult = await _controller.Create(request);

            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        [DataRow("", "Berg", "+46701234567", null)]  // Empty first name
        [DataRow("A", "Berg", "+46701234567", null)]  // Too short (< 2)
        [DataRow("Anna123", "Berg", "+46701234567", null)]  // Contains numbers
        [DataRow("Anna@#$", "Berg", "+46701234567", null)]  // Contains symbols
        [DataRow("Anna", "", "+46701234567", null)]  // Empty last name
        [DataRow("Anna", "B", "+46701234567", null)]  // Last name too short
        [DataRow("Anna", "Berg123", "+46701234567", null)]  // Last name with numbers
        [DataRow("Anna", "Berg", "123", null)]  // Invalid phone (too short)
        [DataRow("Anna", "Berg", "+46701234567", "invalid-email")]  // Invalid email
        public async Task Create_InvalidRequest_ReturnsBadRequestWithErrors(string firstName, string lastName, string phone, string? email)
        {
            var invalidRequest = new CreateCustomerRequest(firstName, lastName, phone, email);

            var actionResult = await _controller.Create(invalidRequest);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Update_ExistingCustomer_ReturnsNoContent()
        {
            var request = new UpdateCustomerRequest("Anna", "Berg", "+46701234567", null);

            _serviceMock
                .Setup(s => s.UpdateAsync(1, request))
                .ReturnsAsync(ServiceResult.Ok());

            var actionResult = await _controller.Update(1, request);

            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Update_NonExistingCustomer_ReturnsNotFound()
        {
            var request = new UpdateCustomerRequest("Anna", "Berg", "+46701234567", null);

            _serviceMock
                .Setup(s => s.UpdateAsync(999, request))
                .ReturnsAsync(ServiceResult.NotFound("Ingen kund hittades med ID: 999"));

            var actionResult = await _controller.Update(999, request);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkWithCustomers()
        {
            //Creates a list of test customers
            var testCustomers = TestDataBuilder.CreateCustomerResponseList();

            // Tells the mock to return our list
            _serviceMock
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(testCustomers);

            var actionResult = await _controller.GetAll();

            // Checks if the controller returned a 200 ok status code
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            // Converts result into OkObjectResult
            var okResult = actionResult.Result as OkObjectResult;
            // Checks if the conversion worked
            Assert.IsNotNull(okResult);

            // Gets the data in okResult
            var returnedCustomers = okResult.Value as IEnumerable<GetCustomerResponse>;
            //Fail safe
            Assert.IsNotNull(returnedCustomers);
            //Checks if the amount of customers the test got back matches the amount in the test customer list
            Assert.AreEqual(testCustomers.Count, returnedCustomers.Count());
        }

        [TestMethod]
        public async Task GetById_ExistingId_ReturnsOk()
        {
            var fakeCustomer = new GetCustomerResponse(1, "Anna", "Berg", "+ 46701234567", null);

            _serviceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(fakeCustomer);

            var actionResult = await _controller.GetById(1);

            var result = actionResult.Result as OkObjectResult;

            //if result isnt Null That means it returns Ok200 status code
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DeleteById_ExistingCustomer_ReturnsNoContent()
        {
            int idToDelete = 1;

            _serviceMock
                .Setup(s => s.DeleteAsync(idToDelete))
                .ReturnsAsync(new ServiceResult(ServiceResultStatus.Success));

            var actionResult = await _controller.DeleteByID(idToDelete);

            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteById_NonExistingCustomer_ReturnsNotFound()
        {
            int nonExistingId = 999;

            _serviceMock
                .Setup(s => s.DeleteAsync(nonExistingId))
                .ReturnsAsync(new ServiceResult(ServiceResultStatus.NotFound));

            var actionResult = await _controller.DeleteByID(nonExistingId);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAllCustomers()
        {
            var fakeCustomer = new List<GetCustomerResponse>{
                new GetCustomerResponse(1,"Anna", "Berg", "+ 46701234567", null),
                new GetCustomerResponse(2,"Bengt", "Berg", "+ 46201234567", null)
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(fakeCustomer);

            var actionResult = await _controller.GetAll();

            var ok = actionResult.Result as OkObjectResult;

            var returnedCustomers = ok.Value as IEnumerable<GetCustomerResponse>;
            Assert.IsNotNull(returnedCustomers);
            Assert.AreEqual(2, returnedCustomers.Count());
        }
    }
}