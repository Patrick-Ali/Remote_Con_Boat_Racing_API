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
    /// <summary>
    /// This class is responsible for processing data
    /// for the event collection.
    /// </summary>
    public class EventService
    {
        private readonly IMongoCollection<Event> _event;
        private readonly GridFSBucket bucket;

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="config">
        /// Configuratuion for the database.
        /// </param>
        public EventService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _event = database.GetCollection<Event>("Event");
            bucket = new GridFSBucket(database);
        }

        /// <summary>
        /// Uploads a file to the database.
        /// </summary>
        /// <param name="stream">
        /// The file in byte format to be stored.
        /// </param>
        /// <param name="location">
        /// Part of the filename, the events location.
        /// </param>
        /// <param name="date">
        /// Part of the filename, the events date.
        /// </param>
        /// <returns>
        /// Returns the file in byte format.
        /// </returns>
        public string UploadFile(byte[] stream, string location, string date) {
            //IGridFSBucket bucket;
            var t = Task.Run<ObjectId>(() => {
                return
                bucket.UploadFromBytesAsync(location+date+".pdf", stream);
                //fs.UploadFromStreamAsync("test.pdf", stream);
            });
            return t.Result.ToString();
        }

        /// <summary>
        /// Get all events from the database
        /// </summary>
        /// <returns>
        /// If successful return all the events
        /// </returns>
        public List<Event> Get()
        {
            return _event.Find(events => true).ToList();
        }

        /// <summary>
        /// Get a specific event from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the event to get from the database
        /// </param>
        /// <returns>
        /// If successful return the specific event
        /// </returns>
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

        /// <summary>
        /// Create a new event.
        /// </summary>
        /// <param name="events">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful returns the created event
        /// </returns>
        public Event Create(EventIn events)
        {
            Event eventIn = new Event() {
                VideoURL = events.VideoURL,
                Name = events.Name,
                Location = events.Location,
                Date = events.Date,
                TimeStart = events.TimeStart,
                TimeEnd = events.TimeEnd,
                EventFileID = UploadFile(events.EventFile, events.Location, events.Date),
                Description = events.Description
            };
            _event.InsertOne(eventIn);
            return eventIn;
        }

        /// <summary>
        /// Update an event.
        /// </summary>
        /// <param name="id">
        /// ID of the event to be updated.
        /// </param>
        /// <param name="eventsIn">
        /// Updated information.
        /// </param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventsIn"></param>
        public void Remove(Event eventsIn)
        {
            _event.DeleteOne(events => events.Id == eventsIn.Id);
        }

        /// <summary>
        /// Delete an Event
        /// </summary>
        /// <param name="id">
        /// ID of the specific event
        /// </param>
        public void Remove(string id)
        {
            _event.DeleteOne(events => events.Id == id);
        }
    }
}
