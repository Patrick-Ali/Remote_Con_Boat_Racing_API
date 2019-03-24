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
    /// for the boat collection.
    /// </summary>
    public class BoatService
    {
        private readonly IMongoCollection<Boat> _boat;

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="config">
        /// Configuratuion for the database.
        /// </param>
        public BoatService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _boat = database.GetCollection<Boat>("Boat");
        }

        /// <summary>
        /// Get all boats from the database
        /// </summary>
        /// <returns>
        /// If successful returns all the boats
        /// </returns>
        public List<Boat> Get()
        {
            return _boat.Find(boat => true).ToList();
        }

        /// <summary>
        /// Get a specific boat from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the boat to get from the database.
        /// </param>
        /// <returns>
        /// If successful returns the specific boat
        /// </returns>
        public Boat Get(string id)
        {
            return _boat.Find<Boat>(boat => boat.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Create a new boat.
        /// </summary>
        /// <param name="boat">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful returns the boat created.
        /// </returns>
        public Boat Create(Boat boat)
        {
            List<Boat> boats = Get();
            foreach (Boat boatTemp in boats)
            {
                if (boatTemp.CaptainID == boat.CaptainID)
                {
                    return null;
                }
            }
            _boat.InsertOne(boat);
            return boat;
        }

        /// <summary>
        /// Update a boat.
        /// </summary>
        /// <param name="id">
        /// ID of the boat to be updated.
        /// </param>
        /// <param name="boatIn">
        /// Updated information.
        /// </param>
        public void Update(string id, Boat boatIn)
        {

            _boat.ReplaceOne(boat => boat.CaptainID == boatIn.CaptainID, boatIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boatIn"></param>
        public void Remove(Boat boatIn)
        {
            _boat.DeleteOne(boat => boat.Id == boatIn.Id);
        }

        /// <summary>
        /// Delete a Boat
        /// </summary>
        /// <param name="id">
        /// ID of the specific boat
        /// </param>
        public void Remove(string id)
        {
            _boat.DeleteOne(boat => boat.Id == id);
        }
    }
}
