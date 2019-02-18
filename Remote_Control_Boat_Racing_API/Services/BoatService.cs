using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    public class BoatService
    {
        private readonly IMongoCollection<Boat> _boat;

        public BoatService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _boat = database.GetCollection<Boat>("Boat");
        }

        public List<Boat> Get()
        {
            return _boat.Find(boat => true).ToList();
        }

        public Boat Get(string id)
        {
            return _boat.Find<Boat>(boat => boat.Id == id).FirstOrDefault();
        }

        public Boat Create(Boat boat)
        {
            _boat.InsertOne(boat);
            return boat;
        }

        public void Update(string id, Boat userIn)
        {
            _boat.ReplaceOne(boat => boat.Id == id, userIn);
        }

        public void Remove(Boat boatIn)
        {
            _boat.DeleteOne(boat => boat.Id == boatIn.Id);
        }

        public void Remove(string id)
        {
            _boat.DeleteOne(boat => boat.Id == id);
        }
    }
}
