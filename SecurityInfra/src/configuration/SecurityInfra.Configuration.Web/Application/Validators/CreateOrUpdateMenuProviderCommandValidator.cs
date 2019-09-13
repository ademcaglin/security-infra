using FluentValidation;
using SecurityInfra.Configuration.Web.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Validations
{
    public class CreateOrUpdateMenuProviderCommandValidator : AbstractValidator<CreateOrUpdateMenuProviderCommand>
    {
        public CreateOrUpdateMenuProviderCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.Uri)
                .NotEmpty()
                .MaximumLength(1000)
                .Must(BeAValidUrl);
        }

        private bool BeAValidUrl(string arg)
        {
            Uri result;
            return Uri.TryCreate(arg, UriKind.Absolute, out result);
        }
    }
}
