using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.IO;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    public class EventService
    {
        private readonly IMongoCollection<Event> _event;
        private readonly GridFSBucket bucket;

        public EventService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _event = database.GetCollection<Event>("Event");
            bucket = new GridFSBucket(database);
        }

        public string UploadFile(byte[] stream, string location, string date) {
            //IGridFSBucket bucket;
            var t = Task.Run<ObjectId>(() => {
                return
                bucket.UploadFromBytesAsync(location+date+".pdf", stream);
                //fs.UploadFromStreamAsync("test.pdf", stream);
            });
            return t.Result.ToString();
        }

        public List<Event> Get()
        {
            return _event.Find(events => true).ToList();
        }

        public EventIn Get(string id)
        {
            Event events = _event.Find<Event>(tempEvent => tempEvent.Id == id).FirstOrDefault();
            ObjectId temp = ObjectId.Parse(events.EventFileID);
            var x = bucket.DownloadAsBytesAsync(temp);
            Task.WaitAll(x);
            EventIn eventIn = new EventIn()
            {
                Id = events.Id,
                VideoURL = events.VideoURL,
                Name = events.Name,
                Location = events.Location,
                Date = events.Date,
                TimeStart = events.TimeStart,
                TimeEnd = events.TimeEnd,
                Description = events.Description,
                EventFile = x.Result
            };
            return eventIn;
        }

        public Event Create(EventIn events)
        {
            Event eventIn = new Event() {
                VideoURL = events.VideoURL,
                Name = events.Name,
                Location = events.Location,
                Date = events.Date,
                TimeStart = events.TimeStart,
                TimeEnd = events.TimeEnd,
                EventFileID = UploadFile(events.EventFile, events.Location, events.Date)
            };
            _event.InsertOne(eventIn);
            return eventIn;
        }

        public void Update(string id, Event eventsIn)
        {
            List<Event> tempEvents = Get();
            Event tempEvent = new Event();
            foreach (Event temp in tempEvents)
            {
                if(temp.Id == eventsIn.Id)
                {
                    tempEvent.Id = eventsIn.Id;
                    tempEvent.Location = eventsIn.Location;
                    tempEvent.VideoURL = eventsIn.Name;
                    tempEvent.TimeStart = eventsIn.TimeStart;
                    tempEvent.TimeEnd = eventsIn.TimeEnd;
                    tempEvent.Description = eventsIn.Description;
                    tempEvent.Date = eventsIn.Date;
                    tempEvent.Name = eventsIn.Name;
                    tempEvent.EventFileID = temp.EventFileID;
                }
            }
            _event.ReplaceOne(events => events.Id == id, tempEvent);
        }

        public void Remove(Event eventsIn)
        {
            _event.DeleteOne(events => events.Id == eventsIn.Id);
        }

        public void Remove(string id)
        {
            _event.DeleteOne(events => events.Id == id);
        }
    }
}
