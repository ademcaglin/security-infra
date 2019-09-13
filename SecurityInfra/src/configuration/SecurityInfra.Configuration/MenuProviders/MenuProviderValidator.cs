using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.MenuProviders
{
    public class MenuProviderValidator : AbstractValidator<MenuProvider>
    {
        public MenuProviderValidator()
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
