using MongoDB.Bson.Serialization;
using SecurityInfra.Configuration.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.Mongo.Mappings
{
    public class ClientMap : MongoDbClassMap<Client>
    {
        public override void Map(BsonClassMap<Client> cm)
        {
            cm.AutoMap();
            cm.MapIdField(x => x.Id);
            cm.SetIgnoreExtraElements(true);
        }
    }
}
