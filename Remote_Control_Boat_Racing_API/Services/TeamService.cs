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
    public class TeamService
    {
        private readonly IMongoCollection<Team> _team;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public TeamService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _team = database.GetCollection<Team>("Team");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Team> Get()
        {
            return _team.Find(teams => true).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Team Get(string id)
        {
            return _team.Find<Team>(teams => teams.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teams"></param>
        /// <returns></returns>
        public Team Create(Team teams)
        {
            _team.InsertOne(teams);
            return teams;
        }

        public void Update(string id, Team teamsIn)
        {
            _team.ReplaceOne(teams => teams.Id == id, teamsIn);
        }

        public void Remove(Team teamsIn)
        {
            _team.DeleteOne(teams => teams.Id == teamsIn.Id);
        }

        public void Remove(string id)
        {
            _team.DeleteOne(teams => teams.Id == id);
        }
    }
}
