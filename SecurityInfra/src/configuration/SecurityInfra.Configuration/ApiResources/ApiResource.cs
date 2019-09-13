using System.Collections.Generic;

namespace SecurityInfra.Configuration.ApiResources
{
    public class ApiResource
    {
        public string Id { get; set; }

        /// <summary>
        /// Indicates if this resource is enabled. Defaults to true.
        /// </summary>
        public bool Enabled { get; private set; } = true;

        /// <summary>
        /// The unique name of the resource.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the resource.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the resource.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of accociated user claims that should be included when this resource is requested.
        /// </summary>
        public ICollection<string> UserClaims { get; set; } = new HashSet<string>();

        /// <summary>
        /// Gets or sets the custom properties for the resource.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The API secret is used for the introspection endpoint. The API can authenticate with introspection using the API name and secret.
        /// </summary>
        public ICollection<Secret> ApiSecrets { get; set; } = new HashSet<Secret>();

        /// <summary>
        /// An API must have at least one scope. Each scope can have different settings.
        /// </summary>
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>(); 

        public void Disable()
        {
            Enabled = false;
        }

    }
}
