//using Moq;
//using SecurityInfra.Configuration.MenuProviders;
//using SecurityInfra.Configuration.Web.Application.Commands;
//using SecurityInfra.Configuration.Web.Application.IntegrationEvents;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace SecurityInfra.Configuration.Tests.Application
//{
//    public class MenuProviderTests
//    {
//        [Fact]
//        public void CreateMenuProviderCommand_Should_Raise_Event()
//        {
//            var cmd = new CreateOrUpdateMenuProviderCommand();
//            cmd.Uri = "http://abc.com";
//            cmd.Title = "any";
//            var rep = new Mock<IMenuProviderRepository>();
//            rep.Setup(x => x.Save(It.IsAny<MenuProvider>())).Returns(Task.CompletedTask);
//            var handler = new CreateOrUpdateMenuProviderCommandHandler(rep.Object);
//            var result = handler.Handle(cmd, new CancellationToken(true)).Result;
//            Assert.Contains(result.IntegrationEvents, x => x.GetType() == typeof(MenuProvidersChangedIntegrationEvent));
//        }
//    }
//}
