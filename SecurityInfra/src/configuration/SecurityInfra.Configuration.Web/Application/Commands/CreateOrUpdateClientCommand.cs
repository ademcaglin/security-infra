using MediatR;
using SecurityInfra.Common.Cqrs;
using SecurityInfra.Configuration.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Commands
{
    public class CreateOrUpdateClientCommand : IRequest<CommandResponse>
    {
        public string Id { get; set; }

        public string ClientId { get; set; }

        public string ProtocolType { get; set; } = "oidc";

        public ICollection<Secret> ClientSecrets { get; set; } = new HashSet<Secret>();

        public bool RequireClientSecret { get; set; } = true;

        public string ClientName { get; set; }

        public string Description { get; set; }

        public string ClientUri { get; set; }

        public string LogoUri { get; set; }

        public bool RequireConsent { get; set; } = true;

        public bool AllowRememberConsent { get; set; } = true;

        public ICollection<string> AllowedGrantTypes { get; set; } = new HashSet<string>();

        public bool RequirePkce { get; set; } = false;

        public bool AllowPlainTextPkce { get; set; } = false;

        public bool AllowAccessTokensViaBrowser { get; set; } = false;

        public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();

        public ICollection<string> PostLogoutRedirectUris { get; set; } = new HashSet<string>();

        public string FrontChannelLogoutUri { get; set; }

        public bool FrontChannelLogoutSessionRequired { get; set; } = true;

        public string BackChannelLogoutUri { get; set; }

        public bool BackChannelLogoutSessionRequired { get; set; } = true;

        public bool AllowOfflineAccess { get; set; } = false;

        public ICollection<string> AllowedScopes { get; set; } = new HashSet<string>();

        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = false;

        public int IdentityTokenLifetime { get; set; } = 300;

        public int AccessTokenLifetime { get; set; } = 3600;

        public int AuthorizationCodeLifetime { get; set; } = 300;

        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;

        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;

        public int? ConsentLifetime { get; set; } = null;

        public TokenUsage RefreshTokenUsage { get; set; } = TokenUsage.OneTimeOnly;

        public bool UpdateAccessTokenClaimsOnRefresh { get; set; } = false;

        public TokenExpiration RefreshTokenExpiration { get; set; } = TokenExpiration.Absolute;

        public AccessTokenType AccessTokenType { get; set; } = AccessTokenType.Jwt;

        public bool EnableLocalLogin { get; set; } = true;

        public ICollection<string> IdentityProviderRestrictions { get; set; } = new HashSet<string>();

        public bool IncludeJwtId { get; set; } = false;

        public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>();

        public bool AlwaysSendClientClaims { get; set; } = false;

        public string ClientClaimsPrefix { get; set; } = "client_";

        public string PairWiseSubjectSalt { get; set; }

        public int? UserSsoLifetime { get; set; }

        public string UserCodeType { get; set; }

        public int DeviceCodeLifetime { get; set; } = 300;

        public ICollection<string> AllowedCorsOrigins { get; set; } = new HashSet<string>();

        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public ICollection<string> ClaimsProviderUris { get; set; } = new HashSet<string>();

    }
}
