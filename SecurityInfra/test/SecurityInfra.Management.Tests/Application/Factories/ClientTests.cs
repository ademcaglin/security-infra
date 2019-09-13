//using Moq;
//using SecurityInfra.Configuration.Clients;
//using SecurityInfra.Configuration.Web.Application.Commands;
//using SecurityInfra..Web.Application.Factories;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace SecurityInfra.Configuration.Tests.Application.Factories
//{
//    public class ClientTests
//    {
//        [Fact]
//        public void ClientId_should_not_be_changed_if_client_exists()
//        {
//            var cmd = new CreateOrUpdateClientCommand();
//            cmd.Id = "adad";
//            cmd.ClientId = "abc";
//            var rep = new Mock<IClientRepository>();
//            rep.Setup(x => x.GetById("adad")).Returns(Task.FromResult(new Client()
//            {
//                ClientId = "abcd"
//            }));
//            var c = new ClientFactory(rep.Object).Create(cmd).Result;
//            Assert.Equal("abcd", c.ClientId);
//        }

//        [Fact]
//        public void Id_should_be_created_if_client_doesnt_exist()
//        {
//            var cmd = new CreateOrUpdateClientCommand();
//            cmd.ClientId = "abc";
//            var rep = new Mock<IClientRepository>();
//            rep.Setup(x => x.GetById("adad")).Returns(Task.FromResult<Client>(null));
//            var c = new ClientFactory(rep.Object).Create(cmd).Result;
//            Assert.Equal("abcd", c.ClientId);
//        }
//    }
//}
