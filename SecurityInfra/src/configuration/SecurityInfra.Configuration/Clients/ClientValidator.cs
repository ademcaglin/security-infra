using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityInfra.Configuration.Clients
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.ClientName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.ClientSecrets).Must(ContainClientSecretsIf);
        }
        private bool ContainClientSecretsIf(Client client, IEnumerable<Secret> secrets)
        {
            if (client.AllowedGrantTypes.Any(x=> x == "client_credentials" ||
                 x == "hybrid" || x == "code"))
                return secrets.Any();
            return true;
        }
    }
}
