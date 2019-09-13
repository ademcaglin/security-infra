using SecurityInfra.Identity.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SecurityInfra.Identity.Tests
{
    public class IdentityUserTests
    {
        [Fact]
        public void IdentityUser_Should_Be_Created()
        {
            var id = new IdentityUser("1", "A", "A", "B");
            Assert.False(id.IsDeleted);
            Assert.Equal("A", id.CreatedBy);
            Assert.Equal("A", id.FirstName);
            Assert.Equal("B", id.LastName);
            Assert.True(id.State == IdentityUserState.VALID);
        }

    }
}
