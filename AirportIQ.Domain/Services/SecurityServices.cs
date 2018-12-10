using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Security.Cryptography;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AirportIQ.Domain.Services
{
    public class SecurityServices : ISecurity
    {

        private TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

        private MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

        private byte[] MD5Hash(string value)
        {
            return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
        }


        public string Encrypt(string stringToEncrypt, string key)
        {
            DES.Key = MD5Hash(key);
            DES.Mode = CipherMode.ECB;
            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt);
            return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }


        public string Decrypt(string encryptedString, string key)
        {
            try
            {
                DES.Key = MD5Hash(key);
                DES.Mode = CipherMode.ECB;
                byte[] Buffer = Convert.FromBase64String(encryptedString);
                return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch
            {
                return null;
            }
        }

    }
}

