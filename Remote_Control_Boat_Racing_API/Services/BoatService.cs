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
    /// 
    /// </summary>
    public class BoatService
    {
        private readonly IMongoCollection<Boat> _boat;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public BoatService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _boat = database.GetCollection<Boat>("Boat");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Boat> Get()
        {
            return _boat.Find(boat => true).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boat Get(string id)
        {
            return _boat.Find<Boat>(boat => boat.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boat"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boatIn"></param>
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id)
        {
            _boat.DeleteOne(boat => boat.Id == id);
        }
    }
}
