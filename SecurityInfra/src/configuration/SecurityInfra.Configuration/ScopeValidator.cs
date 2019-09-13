using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration
{
    public class ScopeValidator : AbstractValidator<Scope>
    {
        public ScopeValidator()
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
