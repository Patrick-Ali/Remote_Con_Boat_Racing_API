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
    /// for the team collection.
    /// </summary>
    public class TeamService
    {
        private readonly IMongoCollection<Team> _team;

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="config">
        /// Configuratuion for the database.
        /// </param>
        public TeamService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _team = database.GetCollection<Team>("Team");
        }

        /// <summary>
        /// Get all teams from the database
        /// </summary>
        /// <returns>
        /// If successful returns all the teams
        /// </returns>
        public List<Team> Get()
        {
            return _team.Find(teams => true).ToList();
        }

        /// <summary>
        /// Get a specific team from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the team to get from the database.
        /// </param>
        /// <returns>
        /// If successful returns the specific team
        /// </returns>
        public Team Get(string id)
        {
            return _team.Find<Team>(teams => teams.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Create a new team.
        /// </summary>
        /// <param name="teams">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful returns the team created
        /// </returns>
        public Team Create(Team teams)
        {
            _team.InsertOne(teams);
            return teams;
        }

        /// <summary>
        /// Update a team.
        /// </summary>
        /// <param name="id">
        /// ID of the team to be updated.
        /// </param>
        /// <param name="teamsIn">
        /// Updated information.
        /// </param>
        public void Update(string id, Team teamsIn)
        {
            _team.ReplaceOne(teams => teams.Id == id, teamsIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamsIn"></param>
        public void Remove(Team teamsIn)
        {
            _team.DeleteOne(teams => teams.Id == teamsIn.Id);
        }

        /// <summary>
        /// Delete a team
        /// </summary>
        /// <param name="id">
        /// ID of the specific team
        /// </param>
        public void Remove(string id)
        {
            _team.DeleteOne(teams => teams.Id == id);
        }
    }
}
