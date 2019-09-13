using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurityInfra.Identity.IdentityRoles;
using SecurityInfra.Identity.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Identity.Mongo
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

        public IMongoCollection<IdentityUser> IdentityUsers
        {
            get
            {
                return _database.GetCollection<IdentityUser>("IdentityUsers");
            }
        }

        public IMongoCollection<IdentityRole> IdentityRoles
        {
            get
            {
                return _database.GetCollection<IdentityRole>("IdentityRoles");
            }
        }

        public IMongoCollection<IdentityRoleDepartment> IdentityRoleResources
        {
            get
            {
                return _database.GetCollection<IdentityRoleDepartment>("IdentityRoleResources");
            }
        }
    }
}
