using SecurityInfra.Configuration.MenuProviders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation.TestHelper;

namespace SecurityInfra.Configuration.Tests.Domain
{
    public class MenuProviderTests
    {
        [Fact]
        public void MenuProvider_Should_Be_Created()
        {
            var pr = new MenuProvider("A", "B");
            Assert.True(pr.Enabled);
            Assert.Equal("A", pr.Uri);
            Assert.Equal("B", pr.Title);
            Assert.False(string.IsNullOrEmpty(pr.Id));
        }

        [Fact]
        public void MenuProvider_Should_Be_Disabled()
        {
            var pr = new MenuProvider("A", "B");
            pr.Disable();
            Assert.False(pr.Enabled);
        }

        [Theory]
        [ClassData(typeof(MenuProviderTestData))]
        public void Tenant_Should_Be_Validated(MenuProvider pr, bool result)
        {
            var validator = new MenuProviderValidator();
            var valid = validator.Validate(pr).IsValid;
            Assert.Equal(valid, result);
        }
    }

    class MenuProviderTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new MenuProvider("http://a.com", new string('a', 200)), true };
            yield return new object[] { new MenuProvider(new string('a', 201), "B"), false };
            yield return new object[] { new MenuProvider("B", new string('a', 201)), false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
