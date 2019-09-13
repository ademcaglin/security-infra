using FluentValidation;
using SecurityInfra.Configuration.Web.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Validators
{
    public class CreateOrUpdateApiResourceCommandValidator : AbstractValidator<CreateOrUpdateApiResourceCommand>
    {
        public CreateOrUpdateApiResourceCommandValidator()
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

            RuleFor(x => x.Scopes).Must(ContainScopes);
        }

        private bool ContainScopes(IEnumerable<Scope> scopes)
        {
            return scopes.Any();
        }
    }
}
