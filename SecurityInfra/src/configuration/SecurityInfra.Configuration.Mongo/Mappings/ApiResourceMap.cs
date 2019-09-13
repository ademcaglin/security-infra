using MongoDB.Bson.Serialization;
using SecurityInfra.Configuration.ApiResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.Mongo.Mappings
{
    public class ApiResourceMap : MongoDbClassMap<ApiResource>
    {
        public override void Map(BsonClassMap<ApiResource> cm)
        {
            cm.AutoMap();
            cm.MapIdField(x => x.Id);
            //cm.GetMemberMap(x=>x.Description)
            cm.SetIgnoreExtraElements(true);
        }
    }
}
