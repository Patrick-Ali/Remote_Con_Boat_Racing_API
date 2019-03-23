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
    public class Admin
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
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
