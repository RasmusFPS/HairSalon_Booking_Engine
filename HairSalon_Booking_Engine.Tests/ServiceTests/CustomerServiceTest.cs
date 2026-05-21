using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Tests.TestData;

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
        var updateRequest = new CreateCustomerRequest("New", "Name", "+46709876543", null);

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
        var request = new CreateCustomerRequest("New", "Name", "+46701234567", null);

        var result = await service.UpdateAsync(999, request);

        Assert.IsFalse(result.Success);
        Assert.AreEqual(ServiceResultStatus.NotFound, result.Status);
    }
}
