using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    public class TeamService
    {
        private readonly IMongoCollection<Team> _team;

        public TeamService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _team = database.GetCollection<Team>("Team");
        }

        public List<Team> Get()
        {
            return _team.Find(teams => true).ToList();
        }

        public Team Get(string id)
        {
            return _team.Find<Team>(teams => teams.Id == id).FirstOrDefault();
        }

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
