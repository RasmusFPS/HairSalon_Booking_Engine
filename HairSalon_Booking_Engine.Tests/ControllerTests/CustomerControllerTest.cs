using FluentValidation;
using FluentValidation.Results;
using HairSalon_Booking_Engine.Controllers;
using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;
using Microsoft.AspNetCore.Http.HttpResults;
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

    //[TestMethod]
    //public async Task Create_ValidRequest_ReturnsCreatedAtAction()
    //{
    //    var request = new CreateCustomerRequest("Anna", "Berg", "+46701234567", null);
    //    var createdCustomer = new GetCustomerResponse(1, "Anna", "Berg", "+46701234567", null);

    //    _validatorMock
    //        .Setup(v => v.ValidateAsync(request))
    //        .ReturnsAsync(new ValidationResult());

    //    _serviceMock
    //        .Setup(s => s.CreateAsync(request))
    //        .ReturnsAsync(ServiceResult<GetCustomerResponse>.Ok(createdCustomer));

    //    var actionResult = await _controller.Create(request);

    //    Assert.IsInstanceOfType(actionResult, typeof(CreatedAtActionResult));
    //    var createdResult = actionResult as CreatedAtActionResult;
    //    Assert.IsNotNull(createdResult);
    //    Assert.AreEqual(nameof(CustomerController.GetById), createdResult.ActionName);
    //}

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
   
        var fakeCustomer = new GetCustomerResponse(1,"Anna", "Berg", "+ 46701234567", null);
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

