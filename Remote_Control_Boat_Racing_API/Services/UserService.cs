using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;
        private readonly string passPhrase = "l%HJb5N^O@fl0K02H9PsxlR9algJTzK7ARBjJsd3fPG0&GwkrU";
        private readonly string passPhrase2 = "yUVyb$shjp4*%S6G!fx5t%i!fTZ@b8KQ#ymQyfhgNQ$#mKB0vA";

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _user = database.GetCollection<User>("User");
        }

        public List<User> Get()
        {
            List<User> hold = _user.Find(user => true).ToList();
            List<User> final = ChangeEnc(hold);

            return final;
        }

        public List<User> ChangeEnc(List<User> users) {
            List<User> publicEncUsers = new List<User>();

            foreach (User user in users) {

                user.Address = Crypto.Decrypt(user.Address, passPhrase2);
                user.City = Crypto.Decrypt(user.City, passPhrase2);
                user.DOB = Crypto.Decrypt(user.DOB, passPhrase2);
                user.Email = Crypto.Decrypt(user.Email, passPhrase2);
                user.FirstName = Crypto.Decrypt(user.FirstName, passPhrase2);
                user.LastName = Crypto.Decrypt(user.LastName, passPhrase2);
                user.PostCode = Crypto.Decrypt(user.PostCode, passPhrase2);
                user.Password = Crypto.Decrypt(user.Password, passPhrase2);
                user.Team = Crypto.Decrypt(user.Team, passPhrase2);
                user.Posistion = Crypto.Decrypt(user.Posistion, passPhrase2);
                user.PhoneNumber = Crypto.Decrypt(user.PhoneNumber, passPhrase2);
                user.MobilePhoneNumber = Crypto.Decrypt(user.MobilePhoneNumber, passPhrase2);

                //string passHold = Crypto.HashPassword(user.Password);

                User crypto = new User();
                crypto.Id = user.Id;
                crypto.Address = Crypto.Encrypt(user.Address, passPhrase);
                crypto.City = Crypto.Encrypt(user.City, passPhrase);
                crypto.DOB = Crypto.Encrypt(user.DOB, passPhrase);
                crypto.Email = Crypto.Encrypt(user.Email, passPhrase);
                crypto.FirstName = Crypto.Encrypt(user.FirstName, passPhrase);
                crypto.LastName = Crypto.Encrypt(user.LastName, passPhrase);
                crypto.PostCode = Crypto.Encrypt(user.PostCode, passPhrase);
                crypto.Password = Crypto.Encrypt(user.Password, passPhrase);
                crypto.Team = Crypto.Encrypt(user.Team, passPhrase2);
                crypto.Posistion = Crypto.Encrypt(user.Posistion, passPhrase);
                crypto.Points = Crypto.Encrypt(user.Points, passPhrase);
                crypto.PhoneNumber = Crypto.Encrypt(user.PhoneNumber, passPhrase);
                crypto.MobilePhoneNumber = Crypto.Encrypt(user.MobilePhoneNumber, passPhrase);

                publicEncUsers.Add(crypto);
            }
            return publicEncUsers;
        }

        public User Get(string id)
        {
            return _user.Find<User>(user => user.Id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            List<User> users = Get();
            string tempHold = Crypto.Decrypt(user.Email, passPhrase);
            foreach (User element in users)
            {
                string temp = Crypto.Decrypt(element.Email, passPhrase);
                if (user.Email == temp)
                {
                    return null;
                }
            }
            User crypto = CreateEnc(user);
            _user.InsertOne(crypto);
            return crypto;
        }

        public User CreateEnc(User user)
        {

            user.Address = Crypto.Decrypt(user.Address, passPhrase);
            user.City = Crypto.Decrypt(user.City, passPhrase);
            user.DOB = Crypto.Decrypt(user.DOB, passPhrase);
            user.Email = Crypto.Decrypt(user.Email, passPhrase);
            user.FirstName = Crypto.Decrypt(user.FirstName, passPhrase);
            user.LastName = Crypto.Decrypt(user.LastName, passPhrase);
            user.PostCode = Crypto.Decrypt(user.PostCode, passPhrase);
            user.Password = Crypto.Decrypt(user.Password, passPhrase);
            user.Team = Crypto.Decrypt(user.Team, passPhrase);
            user.Posistion = Crypto.Decrypt(user.Posistion, passPhrase);
            user.PhoneNumber = Crypto.Decrypt(user.PhoneNumber, passPhrase);
            user.MobilePhoneNumber = Crypto.Decrypt(user.MobilePhoneNumber, passPhrase);

            string passHold = Crypto.HashPassword(user.Password);

            User crypto = new User();
            crypto.Address = Crypto.Encrypt(user.Address, passPhrase2);
            crypto.City = Crypto.Encrypt(user.City, passPhrase2);
            crypto.DOB = Crypto.Encrypt(user.DOB, passPhrase2);
            crypto.Email = Crypto.Encrypt(user.Email, passPhrase2);
            crypto.FirstName = Crypto.Encrypt(user.FirstName, passPhrase2);
            crypto.LastName = Crypto.Encrypt(user.LastName, passPhrase2);
            crypto.PostCode = Crypto.Encrypt(user.PostCode, passPhrase2);
            crypto.Password = Crypto.Encrypt(passHold, passPhrase2);
            crypto.Team = Crypto.Encrypt(user.Team, passPhrase2);
            crypto.Posistion = Crypto.Encrypt(user.Posistion, passPhrase2);
            crypto.Points = Crypto.Encrypt(user.Points, passPhrase2);
            crypto.PhoneNumber = Crypto.Encrypt(user.PhoneNumber, passPhrase2);
            crypto.MobilePhoneNumber = Crypto.Encrypt(user.MobilePhoneNumber, passPhrase2);

            return crypto;
        }

        public void Update(string id, User userIn)
        {
            _user.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(User userIn)
        {
            _user.DeleteOne(user => user.Id == userIn.Id);
        }

        public void Remove(string id)
        {
            _user.DeleteOne(user => user.Id == id);
        }
    }
}
