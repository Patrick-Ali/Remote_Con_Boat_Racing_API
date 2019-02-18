using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    public class EventService
    {
        private readonly IMongoCollection<Event> _event;

        public EventService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _event = database.GetCollection<Event>("Event");
        }

        public List<Event> Get()
        {
            return _event.Find(events => true).ToList();
        }

        public Event Get(string id)
        {
            return _event.Find<Event>(events => events.Id == id).FirstOrDefault();
        }

        public Event Create(Event events)
        {
            _event.InsertOne(events);
            return events;
        }

        public void Update(string id, Event eventsIn)
        {
            _event.ReplaceOne(events => events.Id == id, eventsIn);
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
