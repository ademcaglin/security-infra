using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurityInfra.Configuration.Clients;
using SecurityInfra.Configuration.ApiResources;
using SecurityInfra.Configuration.IdentityResources;
using SecurityInfra.Configuration.MenuProviders;
using System;
using System.Collections.Generic;
using System.Text;
using SecurityInfra.Configuration.Tenants;

namespace SecurityInfra.Configuration.Mongo
{
    public class ConfigurationDbContext
    {
        private IMongoDatabase _database { get; }

        public ConfigurationDbContext(IOptions<ConfigurationDbOptions> options)
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(options.Value.ConnectionString));
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(options.Value.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

        public IMongoCollection<Client> Clients
        {
            get
            {
                return _database.GetCollection<Client>("Clients");
            }
        }

        public IMongoCollection<ApiResource> ApiResources
        {
            get
            {
                return _database.GetCollection<ApiResource>("ApiResources");
            }
        }

        public IMongoCollection<IdentityResource> IdentityResources
        {
            get
            {
                return _database.GetCollection<IdentityResource>("IdentityResources");
            }
        }

        public IMongoCollection<MenuProvider> MenuProviders
        {
            get
            {
                return _database.GetCollection<MenuProvider>("MenuProviders");
            }
        }
        public IMongoCollection<Tenant> Tenants
        {
            get
            {
                return _database.GetCollection<Tenant>("Tenants");
            }
        }
    }
}
