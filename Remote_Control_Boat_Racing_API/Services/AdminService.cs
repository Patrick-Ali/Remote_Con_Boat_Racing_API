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
    /// for the admin collection.
    /// </summary>
    public class AdminService
    {
        private readonly IMongoCollection<Admin> _admin;
        private readonly string passPhrase = "l%HJb5N^O@fl0K02H9PsxlR9algJTzK7ARBjJsd3fPG0&GwkrU";
        private readonly string passPhrase2 = "yUVyb$shjp4*%S6G!fx5t%i!fTZ@b8KQ#ymQyfhgNQ$#mKB0vA";

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="config">
        /// Configuratuion for the database.
        /// </param>
        public AdminService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _admin = database.GetCollection<Admin>("Admin");
        }

        /// <summary>
        /// Get all admins from the database
        /// </summary>
        /// <returns>
        /// If successful return all the admins.
        /// </returns>
        public List<Admin> Get()
        {
            List<Admin> hold = _admin.Find(user => true).ToList();
            List<Admin> final = ChangeEnc(hold);

            return final;
        }

        /// <summary>
        /// This function changes the encryption
        /// from the backend passphrase to the
        /// front ends passphrase.
        /// </summary>
        /// <param name="admins">
        /// List of admins to change the encryption
        /// of.
        /// </param>
        /// <returns>
        /// Returns a list of admins with their encryption
        /// changed.
        /// </returns>
        public List<Admin> ChangeEnc(List<Admin> admins)
        {
            List<Admin> publicEncUsers = new List<Admin>();

            foreach (Admin admin in admins)
            {
                admin.Email = Crypto.Decrypt(admin.Email, passPhrase2);
                admin.Password = Crypto.Decrypt(admin.Password, passPhrase2);

                Admin crypto = new Admin();
                crypto.Id = admin.Id;
                crypto.Email = Crypto.Encrypt(admin.Email, passPhrase);
                crypto.Password = Crypto.Encrypt(admin.Password, passPhrase);


                publicEncUsers.Add(crypto);
            }
            return publicEncUsers;
        }

        /// <summary>
        /// Get a specific admin from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the admin to get from the database
        /// </param>
        /// <returns>
        /// If successful returns the specific admin.
        /// </returns>
        public Admin Get(string id)
        {
            Admin tempUser = _admin.Find<Admin>(user => user.Id == id).FirstOrDefault();
            List<Admin> temp = new List<Admin>();
            temp.Add(tempUser);
            List<Admin> final = ChangeEnc(temp);
            Admin sendUser = final[0];
            return sendUser;

        }

        /// <summary>
        /// Create a new admin.
        /// </summary>
        /// <param name="admin">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful returns the created admin
        /// otherwise returns null.
        /// </returns>
        public Admin Create(Admin admin)
        {
            List<Admin> users = Get();
            string tempHold = Crypto.Decrypt(admin.Email, passPhrase);
            foreach (Admin element in users)
            {
                string temp = Crypto.Decrypt(element.Email, passPhrase);
                if (admin.Email == temp)
                {
                    return null;
                }
            }
            Admin crypto = CreateEnc(admin);
            _admin.InsertOne(crypto);
            return crypto;
        }

        /// <summary>
        /// Encrypts a given admin with the
        /// API's passphrase.
        /// </summary>
        /// <param name="admin">
        /// Admin to be encrypted.
        /// </param>
        /// <returns>
        /// If successful returns the encrypted admin.
        /// </returns>
        public Admin CreateEnc(Admin admin)
        {


            admin.Email = Crypto.Decrypt(admin.Email, passPhrase);
            admin.Password = Crypto.Decrypt(admin.Password, passPhrase);


            string passHold = Crypto.HashPassword(admin.Password);

            Admin crypto = new Admin();

            crypto.Email = Crypto.Encrypt(admin.Email, passPhrase2);

            crypto.Password = Crypto.Encrypt(passHold, passPhrase2);


            return crypto;
        }

        /// <summary>
        /// Update an admin.
        /// </summary>
        /// <param name="id">
        /// ID of the admin to be updated.
        /// </param>
        /// <param name="adminIn">
        /// Updated information.
        /// </param>
        public void Update(string id, Admin adminIn)
        {
            _admin.ReplaceOne(admin => admin.Id == id, adminIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminIn"></param>
        public void Remove(Admin adminIn)
        {
            _admin.DeleteOne(admin => admin.Id == adminIn.Id);
        }

        /// <summary>
        /// Delete an Admin
        /// </summary>
        /// <param name="id">
        /// ID of the specific admin
        /// </param>
        public void Remove(string id)
        {
            _admin.DeleteOne(admin => admin.Id == id);
        }
    }
}
