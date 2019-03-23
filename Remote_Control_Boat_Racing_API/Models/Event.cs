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
    public class Event
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
        [BsonElement("videoURL")]
        public string VideoURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("eventFileID")]
        public string EventFileID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("date")]
        public string Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("location")]
        public string Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("timeStart")]
        public string TimeStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("timeEnd")]
        public string TimeEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; }


    }
}

// Front end form
// Enter name, date, location, timeStart, and timeEnd, videoURL
// Browse for file, select file
// Turn file into bytes
// send bytes and get file id
// send event object with name, date, location, timeStart, timeEnd, videoURL,
// and recived file id

// Return file 
// Load page get event info
// provide link which wil trigger download file,
// send file bytes 
// convert bytes into file 