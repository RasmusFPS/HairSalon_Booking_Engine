using FluentValidation;
using HairSalon_Booking_Engine.Controllers;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HairSalon_Booking_Engine.Tests.ControllerTests;

[TestClass]
public class CustomerControllerTest
{
    private Mock<ICustomerService> _serviceMock = null!;
    private Mock<IValidator<CreateCustomerRequest>> _validatorMock = null!;
    private CustomerController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<ICustomerService>();
        _validatorMock = new Mock<IValidator<CreateCustomerRequest>>();
        _controller = new CustomerController(_serviceMock.Object, _validatorMock.Object);
    }

    [TestMethod]
    public async Task Update_ExistingCustomer_ReturnsNoContent()
    {
        var request = new UpdateCustomerRequest("Anna", "Berg", "+46701234567", null);
        _serviceMock.Setup(s => s.UpdateAsync(1, request)).ReturnsAsync(ServiceResult.Ok());

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
        // 1. Arrange
        //Creates a list of test customers
        var testCustomers = TestDataBuilder.CreateCustomerResponseList();

        // Tells the mock to return our list
        _serviceMock
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(testCustomers);

        // 2. Act
        var actionResult = await _controller.GetAll();

        // 3. Assert
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
}
