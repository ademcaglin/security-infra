using MongoDB.Bson.Serialization;
using SecurityInfra.Configuration.IdentityResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.Mongo.Mappings
{
    public class IdentityResourceMap : MongoDbClassMap<IdentityResource>
    {
        public override void Map(BsonClassMap<IdentityResource> cm)
        {
            cm.AutoMap();
            cm.MapIdField(x => x.Id);
            cm.SetIgnoreExtraElements(true);
        }
    }
}
