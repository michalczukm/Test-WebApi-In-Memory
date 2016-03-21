using Autofac;
using Effort;
using IntegrationTestsPOC.DataAccess;

namespace IntegrationTestsPOC.IntegrationTests
{
    public class IntegrationTestsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var dbConnection = DbConnectionFactory.CreateTransient();

            builder.Register(context =>
            {
                var pocContext = new PocContext(dbConnection);
                TestsSeed.Seed(pocContext);

                return pocContext;
            }).As<PocContext>().AsSelf();
        }
    }
}