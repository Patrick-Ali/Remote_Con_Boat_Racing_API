using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication3.Controllers
{
    public class User
    {
        private ObjectId id;
        private string firstName;
        private string lastName;
        private DateTime dob;
        private int points;
        private string posistion;
        private Boat boat;
        private string address;
        private string postCode;
        private string city;
        private string email;
        private string password;
        private int phoneNumber;

        User()
        {
            this.firstName = null;
            this.lastName = null;
            this.address = null;
            this.city = null;
            this.postCode = null;
            this.phoneNumber = 0;
            this.dob = new DateTime(11,12,13);
            this.email = null;
            this.password = null;
            this.posistion = "Spectator";
            this.points = 0;
        }

        User(string firstName, string lastName, DateTime dob, string posistion,
                string address, string postCode, string email, int phoneNumber,
                string password, string city)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.postCode = postCode;
            this.phoneNumber = phoneNumber;
            this.dob = dob;
            this.email = email;
            this.password = password;
            this.posistion = posistion;
            this.points = 0;

        }
    }
}
