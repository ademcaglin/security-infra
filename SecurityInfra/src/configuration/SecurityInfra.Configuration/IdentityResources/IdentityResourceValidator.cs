using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityInfra.Configuration.IdentityResources
{
    public class IdentityResourceValidator : AbstractValidator<IdentityResource>
    {
        public IdentityResourceValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
