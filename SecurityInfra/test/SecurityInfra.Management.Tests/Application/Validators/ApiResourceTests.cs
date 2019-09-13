//using FluentValidation.TestHelper;
//using SecurityInfra.Management.Web.Application.Commands;
//using SecurityInfra.Management.Web.Application.Validators;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xunit;

//namespace SecurityInfra.Configuration.Tests.Application.Validators
//{
//    public class ApiResourceTests
//    {
//        [Fact]
//        public void Api_resource_should_contain_at_least_one_scope()
//        {
//            var validator = new CreateOrUpdateApiResourceCommandValidator();
//            var validApiResource = new CreateOrUpdateApiResourceCommand();
//            validApiResource.Scopes.Add(new Scope()
//            {
//                Name = "abc",
//                DisplayName = "dgd"
//            });
//            validator.ShouldNotHaveValidationErrorFor(x => x.Scopes, validApiResource);
//            validator.ShouldHaveValidationErrorFor(x => x.Scopes, new CreateOrUpdateApiResourceCommand());
//        }
//    }
//}
