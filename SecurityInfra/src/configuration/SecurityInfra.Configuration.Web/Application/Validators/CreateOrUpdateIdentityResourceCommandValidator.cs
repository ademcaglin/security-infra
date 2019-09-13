using FluentValidation;
using SecurityInfra.Configuration.Web.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Validators
{
    public class CreateOrUpdateIdentityResourceCommandValidator : AbstractValidator<CreateOrUpdateIdentityResourceCommand>
    {
        public CreateOrUpdateIdentityResourceCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
