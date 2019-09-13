using FluentValidation;
using SecurityInfra.Configuration.Web.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Validators
{
    public class CreateOrUpdateClientCommandValidator : AbstractValidator<CreateOrUpdateClientCommand>
    {
        public CreateOrUpdateClientCommandValidator()
        {
            RuleFor(x => x.ClientName)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.ClientId)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.ClientUri).Must(BeAValidUrl);

            RuleFor(x => x.FrontChannelLogoutUri).Must(BeAValidUrl);

            RuleFor(x => x.LogoUri).Must(BeAValidUrl);

            RuleForEach(x => x.PostLogoutRedirectUris).Must(BeAValidUrl);

            RuleFor(x => x.RedirectUris).Must(ContainsRedirectUriIf);

            RuleForEach(x => x.RedirectUris).Must(BeAValidUrl);

            RuleFor(x => x.ClientSecrets).Must(ContainsSecretsIf);

            RuleFor(x => x.AllowedGrantTypes).Must(ContainsGrantType);
        }

        private bool ContainsSecretsIf(CreateOrUpdateClientCommand client, IEnumerable<Secret> secrets)
        {
            if (client.AllowedGrantTypes.Any(x => x == "client_credentials" ||
                 x == "hybrid" || x == "code"))
                return secrets.Any();
            return true;
        }

        private bool ContainsRedirectUriIf(CreateOrUpdateClientCommand client, IEnumerable<string> uris)
        {
            if (client.AllowedGrantTypes.Any(x => x == "implicit" ||
                 x == "hybrid" || x == "code"))
                return uris.Any();
            return true;
        }

        private bool ContainsGrantType(CreateOrUpdateClientCommand client, IEnumerable<string> grantTypes)
        {
             return grantTypes.Any();
        }

        private bool BeAValidUrl(string arg)
        {
            Uri result;
            return Uri.TryCreate(arg, UriKind.Absolute, out result);
        }
    }
}
