using System.Data.Entity.Migrations;

namespace IntegrationTestsPOC.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PocContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PocContext context)
        {
            context.People.AddOrUpdate(
                p => p.Id,
                new Person {FirstName = "Andrew", LastName = "Peters"},
                new Person {FirstName = "Brice", LastName = "Lambson"},
                new Person {FirstName = "Rowan", LastName = "Miller"}
            );
        }
    }
}
