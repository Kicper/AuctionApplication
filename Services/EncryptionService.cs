using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    public class EncryptionService
    {
        public string Encrypt(string password)
        {
            HashAlgorithm sha = SHA256.Create();
            byte[] passwordCoverted = Encoding.ASCII.GetBytes(password);
            byte[] resultPassword = sha.ComputeHash(passwordCoverted);
            return Encoding.ASCII.GetString(resultPassword);
        }
    }
}