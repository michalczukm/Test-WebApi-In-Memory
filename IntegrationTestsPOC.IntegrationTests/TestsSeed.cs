using System.Data.Entity.Migrations;
using IntegrationTestsPOC.DataAccess;

namespace IntegrationTestsPOC.IntegrationTests
{
    public static class TestsSeed
    {
        public static void Seed(PocContext context)
        {
            context.People.AddOrUpdate(
                p => p.Id,
                new Person { FirstName = "Andrew", LastName = "Peters" },
                new Person { FirstName = "Brice", LastName = "Lambson" },
                new Person { FirstName = "Rowan", LastName = "Miller" }
                );

            context.SaveChanges();
        }
    }
}