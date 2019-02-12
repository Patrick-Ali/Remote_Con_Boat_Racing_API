using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private static MongoClient mongoClient = new MongoClient();
        private static MongoDB.Driver.IMongoDatabase db = mongoClient.GetDatabase("test");
        private static IMongoCollection<Test> collection = db.GetCollection<Test>("Testing");

        // GET: api/<controller>
        [HttpGet]
        public List<Test> Get()
        {

            
            //var all = collection.Find(x => x.name == "John Smith").ToList();
            
            var all = collection.Find(_ => true).ToList();

            
            return all; //new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{idd}")]
        public int Get(int idd)
        {
            var all = collection.Find(x => x.age == idd).ToList();
            return idd;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Test value)
        {
            //Test test = new Test
            //{
            //    name = value.name,
            //    age = value.age
            //};

            collection.InsertOneAsync(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
