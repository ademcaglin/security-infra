//using FluentValidation.TestHelper;
//using SecurityInfra.Management.Web.Application.Commands;
//using SecurityInfra.Management.Web.Application.Validators;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xunit;

//namespace SecurityInfra.Configuration.Tests.Application.Validators
//{
//    public class ClientValidatorTests
//    {
//        [Fact]
//        public void Client_should_contain_at_least_one_granttype()
//        {
//            var validator = new CreateOrUpdateClientCommandValidator();
//            var validClient = new CreateOrUpdateClientCommand();
//            validClient.AllowedGrantTypes.Add("implicit");
//            validator.ShouldNotHaveValidationErrorFor(x => x.AllowedGrantTypes, validClient);
//            validator.ShouldHaveValidationErrorFor(x => x.AllowedGrantTypes, new CreateOrUpdateClientCommand());
//        }

//        [Fact]
//        public void Client_should_contain_secret_if_granttype_required_secret()
//        {
//            var validator = new CreateOrUpdateClientCommandValidator();
//            var validClient = new CreateOrUpdateClientCommand();
//            validClient.AllowedGrantTypes.Add("hybrid");
//            validClient.ClientSecrets.Add(new Secret()
//            {
//                Type = "sf",
//                Value = "fsf"
//            });
//            validator.ShouldNotHaveValidationErrorFor(x => x.ClientSecrets, validClient);

//            var inValidClient = new CreateOrUpdateClientCommand();
//            inValidClient.AllowedGrantTypes.Add("hybrid");
//            validator.ShouldHaveValidationErrorFor(x => x.ClientSecrets, inValidClient);
//        }

//        [Fact]
//        public void Client_should_contain_redirecturi_if_grantype_required()
//        {
//            var validator = new CreateOrUpdateClientCommandValidator();
//            var validClient = new CreateOrUpdateClientCommand();
//            validClient.AllowedGrantTypes.Add("hybrid");
//            validClient.RedirectUris.Add("http://localhost");
//            validator.ShouldNotHaveValidationErrorFor(x => x.ClientSecrets, validClient);

//            var inValidClient = new CreateOrUpdateClientCommand();
//            inValidClient.AllowedGrantTypes.Add("hybrid");
//            validator.ShouldHaveValidationErrorFor(x => x.RedirectUris, inValidClient);
//        }
//    }
//}
