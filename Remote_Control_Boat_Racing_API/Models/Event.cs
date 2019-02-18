using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace Remote_Control_Boat_Racing_API.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id;

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("timeStart")]
        public string TimeStart { get; set; }

        [BsonElement("timeEnd")]
        public string TimeEnd { get; set; }


    }
}
