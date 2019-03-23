using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace Remote_Control_Boat_Racing_API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Team
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id;

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("captain")]
        public string CaptainID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("pit")]
        public string PitID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("recruiting")]
        public string Recruiting { get; set; }

    }
}
