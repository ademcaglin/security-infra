using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration
{
    public class ClaimValidator : AbstractValidator<Claim>
    {
        public ClaimValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .MaximumLength(500);
            RuleFor(x => x.Value)
                .NotEmpty();
        }
    }
}
