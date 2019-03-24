using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    /// <summary>
    /// This class is responsible for processing data
    /// for the event reg collection.
    /// </summary>
    public class EventRegService
    {
        private readonly IMongoCollection<EventReg> _eventReg;

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="config">
        /// Configuratuion for the database.
        /// </param>
        public EventRegService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _eventReg = database.GetCollection<EventReg>("EventReg");
        }

        /// <summary>
        /// Get all event regs from the database
        /// </summary>
        /// <returns>
        /// If successful returns all the event regs
        /// </returns>
        public List<EventReg> Get()
        {
            return _eventReg.Find(events => true).ToList();
        }


        /// <summary>
        /// Get a specific event reg from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the event reg to get from the database
        /// </param>
        /// <returns>
        /// If successful returns the specific event reg
        /// </returns>
        public EventReg Get(string id)
        {
            return _eventReg.Find<EventReg>(eventsReg => eventsReg.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Create a new event reg.
        /// </summary>
        /// <param name="eventsReg">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful returns the event reg created
        /// </returns>
        public EventReg Create(EventReg eventsReg)
        {
            _eventReg.InsertOne(eventsReg);
            return eventsReg;
        }

        /// <summary>
        /// Update an event reg.
        /// </summary>
        /// <param name="id">
        /// ID of the event reg to be updated.
        /// </param>
        /// <param name="eventsRegIn">
        /// Updated information.
        /// </param>
        public void Update(string id, EventReg eventsRegIn)
        {
            _eventReg.ReplaceOne(eventsReg => eventsReg.Id == id, eventsRegIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventsRegIn"></param>
        public void Remove(EventReg eventsRegIn)
        {
            _eventReg.DeleteOne(eventsReg => eventsReg.Id == eventsRegIn.Id);
        }

        /// <summary>
        /// Delete an EventReg
        /// </summary>
        /// <param name="id">
        /// ID of the specific event reg
        /// </param>
        public void Remove(string id)
        {
            _eventReg.DeleteOne(eventsReg => eventsReg.Id == id);
        }
    }
}
