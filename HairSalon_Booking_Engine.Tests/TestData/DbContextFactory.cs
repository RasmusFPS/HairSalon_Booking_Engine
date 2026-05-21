using Microsoft.EntityFrameworkCore;

namespace HairSalon_Booking_Engine.Tests.TestData
{
    internal static class DbContextFactory
    {
        /// <summary>
        /// Metod för att skapa en fake databas som kan användas i Service tester.
        /// </summary>
        /// <param name="dbName">Namnet på fake databasen.</param>
        /// <returns>Returnerar en ny HairSalonDBContext.</returns>
        public static HairSalonDBContext Create(string dbName)
        {
            var options = new DbContextOptionsBuilder<HairSalonDBContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new HairSalonDBContext(options);
        }
    }
}
