using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    public class EventRegService
    {
        private readonly IMongoCollection<EventReg> _eventReg;

        public EventRegService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _eventReg = database.GetCollection<EventReg>("EventReg");
        }

        public List<EventReg> Get()
        {
            return _eventReg.Find(events => true).ToList();
        }

        public EventReg Get(string id)
        {
            return _eventReg.Find<EventReg>(eventsReg => eventsReg.Id == id).FirstOrDefault();
        }

        public EventReg Create(EventReg eventsReg)
        {
            _eventReg.InsertOne(eventsReg);
            return eventsReg;
        }

        public void Update(string id, EventReg eventsRegIn)
        {
            _eventReg.ReplaceOne(eventsReg => eventsReg.Id == id, eventsRegIn);
        }

        public void Remove(EventReg eventsRegIn)
        {
            _eventReg.DeleteOne(eventsReg => eventsReg.Id == eventsRegIn.Id);
        }

        public void Remove(string id)
        {
            _eventReg.DeleteOne(eventsReg => eventsReg.Id == id);
        }
    }
}
