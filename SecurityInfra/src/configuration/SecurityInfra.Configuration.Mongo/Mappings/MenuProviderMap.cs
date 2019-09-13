using MongoDB.Bson.Serialization;
using SecurityInfra.Configuration.MenuProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.Mongo.Mappings
{
    public class MenuProviderMap : MongoDbClassMap<MenuProvider>
    {
        public override void Map(BsonClassMap<MenuProvider> cm)
        {
            cm.AutoMap();
            cm.MapIdField(x => x.Id);
            cm.SetIgnoreExtraElements(true);
        }
    }
}
