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
    /// This controller is responsible for processing data
    /// for the user collection.
    /// </summary>
    public class UserService
    {
        private readonly IMongoCollection<User> _user;
        private readonly string passPhrase = "l%HJb5N^O@fl0K02H9PsxlR9algJTzK7ARBjJsd3fPG0&GwkrU";
        private readonly string passPhrase2 = "yUVyb$shjp4*%S6G!fx5t%i!fTZ@b8KQ#ymQyfhgNQ$#mKB0vA";

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="config">
        /// Configuratuion for the database.
        /// </param>
        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _user = database.GetCollection<User>("User");
        }

        // GET: api/<controller>
        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <returns>
        /// If successful returns all the users.
        /// </returns>
        public List<User> Get()
        {
            List<User> hold = _user.Find(user => true).ToList();
            List<User> final = ChangeEnc(hold);

            return final;
        }

        /// <summary>
        /// This function changes the encryption
        /// from the backend passphrase to the
        /// front ends passphrase.
        /// </summary>
        /// <param name="users">
        /// List of users to change the encryption
        /// of.
        /// </param>
        /// <returns>
        /// Returns a list of users with their encryption
        /// changed.
        /// </returns>
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
                user.Points = Crypto.Decrypt(user.Points, passPhrase2);

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
                crypto.Team = Crypto.Encrypt(user.Team, passPhrase);
                crypto.Posistion = Crypto.Encrypt(user.Posistion, passPhrase);
                crypto.Points = Crypto.Encrypt(user.Points, passPhrase);
                crypto.PhoneNumber = Crypto.Encrypt(user.PhoneNumber, passPhrase);
                crypto.MobilePhoneNumber = Crypto.Encrypt(user.MobilePhoneNumber, passPhrase);
               

                publicEncUsers.Add(crypto);
            }
            return publicEncUsers;
        }

        /// <summary>
        /// Get a specific user from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the user to get from the database.
        /// </param>
        /// <returns>
        /// If successful returns the specific user.
        /// </returns>
        public User Get(string id)
        {
            User tempUser = _user.Find<User>(user => user.Id == id).FirstOrDefault();
            List<User> temp = new List<User>();
            temp.Add(tempUser);
            List<User> final = ChangeEnc(temp);
            User sendUser = final[0];
            return sendUser;
            //return _user.Find<User>(user => user.Id == id).FirstOrDefault();
        }

        // POST api/<controller>
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful returns the created
        /// user.
        /// </returns>
        public User Create(User user)
        {
            List<User> users = Get();
            string tempHold = Crypto.Decrypt(user.Email, passPhrase);
            foreach (User element in users)
            {
                string temp = Crypto.Decrypt(element.Email, passPhrase);
                if (tempHold == temp)
                {
                    return null;
                }
            }
            User crypto = CreateEnc(user, false);
            _user.InsertOne(crypto);
            return crypto;
        }

        /// <summary>
        /// Encrypts a given user with the
        /// API's passphrase.
        /// </summary>
        /// <param name="user">
        /// Admin to be encrypted.
        /// </param>
        /// <param name="update">
        /// Determine if the password needs to be encrypted.
        /// </param>
        /// <returns>
        /// If successful returns the encrypted user.
        /// </returns>
        public User CreateEnc(User user, Boolean update)
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
            user.Points = Crypto.Decrypt(user.Points, passPhrase);

            string passHold = user.Password;

            if (update == false)
            {
                passHold = Crypto.HashPassword(user.Password);
            }

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

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="id">
        /// ID of the user to be updated.
        /// </param>
        /// <param name="userIn">
        /// Updated information.
        /// </param>
        public void Update(string id, User userIn)
        {
            User crypto = CreateEnc(userIn, true);
            crypto.Id = userIn.Id;
            _user.ReplaceOne(user => user.Id == id, crypto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIn"></param>
        public void Remove(User userIn)
        {
            _user.DeleteOne(user => user.Id == userIn.Id);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">
        /// ID of the specific user
        /// </param>
        public void Remove(string id)
        {
            _user.DeleteOne(user => user.Id == id);
        }
    }
}
