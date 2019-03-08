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

        [BsonElement("videoURL")]
        public string VideoURL { get; set; }

        [BsonElement("eventFileID")]
        public string EventFileID { get; set; }

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