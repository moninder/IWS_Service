using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
    public interface ISecurity
    {
        string Encrypt(string stringToEncrypt, string key);
        string Decrypt(string encryptedString, string key);

				// Passwords should be Hashed not encrypted
				

				
    }
}
