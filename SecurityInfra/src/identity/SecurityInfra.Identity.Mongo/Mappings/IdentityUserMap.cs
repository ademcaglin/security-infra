using MongoDB.Bson.Serialization;
using SecurityInfra.Identity.IdentityUsers;
using SecurityInfra.Identity.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.Mongo.Mappings
{
    public class IdentityUserMap : MongoDbClassMap<IdentityUser>
    {
        public override void Map(BsonClassMap<IdentityUser> cm)
        {
            cm.AutoMap();
            cm.MapIdField(x => x.Id);
            cm.SetIgnoreExtraElements(true);
        }
    }
}
