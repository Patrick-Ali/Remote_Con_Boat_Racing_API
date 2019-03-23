using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace Remote_Control_Boat_Racing_API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id;

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("dob")]
        public string DOB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("points")]
        public string Points { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("posistion")]
        public string Posistion { get; set; }
        //private Boat boat;

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("postCode")]
        public string PostCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("city")]
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("password")]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("mobilePhoneNumber")]
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("team")]
        public string Team { get; set; }

        //User()
        //{
        //    this.firstName = null;
        //    this.lastName = null;
        //    this.address = null;
        //    this.city = null;
        //    this.postCode = null;
        //    this.phoneNumber = 0;
        //    this.dob = new DateTime(11,12,13);
        //    this.email = null;
        //    this.password = null;
        //    this.posistion = "Spectator";
        //    this.points = 0;
        //}

        //User(string firstName, string lastName, DateTime dob, string posistion,
        //        string address, string postCode, string email, int phoneNumber,
        //        string password, string city)
        //{
        //    this.firstName = firstName;
        //    this.lastName = lastName;
        //    this.address = address;
        //    this.city = city;
        //    this.postCode = postCode;
        //    this.phoneNumber = phoneNumber;
        //    this.dob = dob;
        //    this.email = email;
        //    this.password = password;
        //    this.posistion = posistion;
        //    this.points = 0;

        //}
    }
}
