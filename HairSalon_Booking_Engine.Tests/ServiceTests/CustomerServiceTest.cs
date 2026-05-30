using HairSalon_Booking_Engine.Controllers;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Booking_Engine.Tests.ServiceTests;

[TestClass]
public class CustomerServiceTest
{
    [TestMethod]
    public async Task UpdateAsync_ExistingCustomer_UpdatesFields()
    {
        await using var ctx = DbContextFactory.Create(nameof(UpdateAsync_ExistingCustomer_UpdatesFields));
        ctx.Customers.Add(TestDataBuilder.CreateCustomer()); // skapa en ny kund med hjälp av test data builder
        await ctx.SaveChangesAsync();

        var service = new CustomerService(ctx);
        // använd DTO för att uppdatera, precis som i vanliga service metoderna
        var updateRequest = new UpdateCustomerRequest("New", "Name", "+46709876543", null);

        var result = await service.UpdateAsync(1, updateRequest);

        Assert.IsTrue(result.Success);
        var updated = await ctx.Customers.FindAsync(1); // hitta den uppdaterade kunden i fake databasen
        Assert.AreEqual("New", updated!.FirstName);
        Assert.AreEqual("+46709876543", updated.Phone);
    }

    [TestMethod]
    public async Task UpdateAsync_NonExistingId_ReturnsNotFound()
    {
        await using var ctx = DbContextFactory.Create(nameof(UpdateAsync_NonExistingId_ReturnsNotFound));
        var service = new CustomerService(ctx);
        var request = new UpdateCustomerRequest("New", "Name", "+46701234567", null);

        var result = await service.UpdateAsync(999, request);

        Assert.IsFalse(result.Success);
        Assert.AreEqual(ServiceResultStatus.NotFound, result.Status);
    }

    [TestMethod]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectCustomer()
    {
        // Arrange
        await using var ctx = DbContextFactory.Create(nameof(GetByIdAsync_ExistingId_ReturnsCorrectCustomer));
        ctx.Customers.Add(TestDataBuilder.CreateCustomer(id: 1));
        await ctx.SaveChangesAsync();

        var service = new CustomerService(ctx);

        // Act
        var result = await service.GetByIdAsync(1);

        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Anna", result.FirstName);
        Assert.AreEqual("Johansson", result.LastName);
    }

    [TestMethod]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        //Arrange
        await using var ctx = DbContextFactory.Create(nameof(GetByIdAsync_NonExistingId_ReturnsNull));
        var service = new CustomerService(ctx);

        //Act
        var result = await service.GetByIdAsync(999);

        //Assert
        Assert.IsNull(result);

        // no customer gets added to the database - its empty
        // we apply and search for id 999, which doesnt exist,
        // and assert IsNull instead for IsNotNull
    }

    [TestMethod]
    public async Task CreateAsync_ValidRequest_PersistsCustomer()
    {
        // Arrange
        await using var ctx = DbContextFactory.Create(nameof(CreateAsync_ValidRequest_PersistsCustomer));
        var service = new CustomerService(ctx);
        var request = new CreateCustomerRequest("Anna", "Johansson", "+46701234567", "anna.johansson@email.com");

        // Act
        var result = await service.CreateAsync(request);

        // Assert
        Assert.IsTrue(result.Success);
        var created = ctx.Customers.FirstOrDefault(c => c.FirstName == "Anna");
        Assert.IsNotNull(created);
        Assert.IsNotNull("Anna", created.FirstName);
        Assert.AreEqual("Johansson", created.LastName);



    }

    [TestMethod]
    public async Task CreateAsync_ReturnsCreatedCustomerData()
    {
        await using var ctx = DbContextFactory.Create(nameof(CreateAsync_ReturnsCreatedCustomerData));
        var service = new CustomerService(ctx);
        var request = new CreateCustomerRequest(
            FirstName: "TestName",
            LastName: "Test",
            Phone: "00000099",
            Email: "Test@email.com"
        );

        var result = await service.CreateAsync(request);

        Assert.IsNotNull(result);

        var savedCustomer = await ctx.Customers.FindAsync(result.Data!.Id);
        Assert.IsNotNull(savedCustomer);
    }

    [TestMethod]
    public async Task DeleteAsync_NonExistingId_ReturnsNotFound()
    {
        await using var ctx = DbContextFactory.Create(nameof(DeleteAsync_NonExistingId_ReturnsNotFound));
        var FakeCustomer = TestDataBuilder.CreateCustomer(id: 1);

        ctx.Customers.Add(FakeCustomer);
        await ctx.SaveChangesAsync();

        var service = new CustomerService(ctx);

        var IdToDelete = await service.DeleteAsync(999);

        Assert.IsFalse(IdToDelete.Success);

        Assert.AreEqual(ServiceResultStatus.NotFound, IdToDelete.Status);
    }

}
