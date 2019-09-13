//using AutoMapper;
//using Moq;
//using SecurityInfra.Configuration.Clients;
//using SecurityInfra.Configuration.Web.Application.Commands;
//using SecurityInfra.Configuration.Web.Application.Factories;
//using SecurityInfra.Configuration.Web.Application.Mappers;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace SecurityInfra.Configuration.Tests.Application
//{
//    public class ClientTests
//    {
//        //public ClientTests()
//        //{
//        //    Mapper.Initialize(cfg => cfg.CreateMap<CreateOrUpdateClientCommand, Client>());
//        //}

//        [Fact]
//        public void CreateOrUpdateClientCommand_To_Client_Should_Not_Change_ClientId_When_Id_HasValue()
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
//    }
//}
