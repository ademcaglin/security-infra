using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {

        public string ConnectionString { get; }

        public ApplicationModule(string constr)
        {
            ConnectionString = constr;
        }

        protected override void Load(ContainerBuilder builder)
        {

            //builder.Register(c => new OrderQueries(ConnectionString))
            //    .As<IOrderQueries>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<BuyerRepository>()
            //    .As<IBuyerRepository>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<OrderRepository>()
            //    .As<IOrderRepository>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<RequestManager>()
            //   .As<IRequestManager>()
            //   .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
