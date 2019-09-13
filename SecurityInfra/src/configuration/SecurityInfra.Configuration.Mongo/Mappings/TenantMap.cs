using MongoDB.Bson.Serialization;
using SecurityInfra.Configuration.ApiResources;
using SecurityInfra.Configuration.Tenants;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.Mongo.Mappings
{
    public class TenantMap : MongoDbClassMap<Tenant>
    {
        public override void Map(BsonClassMap<Tenant> cm)
        {
            cm.AutoMap();
            cm.MapIdField(x => x.Id);
            cm.SetIgnoreExtraElements(true);
        }
    }
}
