using MongoDB.Bson.Serialization;
using SecurityInfra.Identity.IdentityUsers;
using SecurityInfra.Identity.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.Mongo.Mappings
{
    public class IdentityUserRoleMap : MongoDbClassMap<IdentityUserRole>
    {
        public override void Map(BsonClassMap<IdentityUserRole> cm)
        {
            cm.AutoMap();
            cm.MapIdField(x => x.Id);
            cm.SetIgnoreExtraElements(true);
        }
    }
}
