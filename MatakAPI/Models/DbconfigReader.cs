using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatakAPI.Models
{
    public class DbconfigReader
    {
        string host { get; set; }
        string username { get; set; }
        string password { get; set; }
        string database { get; set; }
        public string JWTencoding { get; set; }


        public DbconfigReader(string host,string username,string password,string database,string JWTencoding)
        {
            this.host = host;
            this.username = username;
            this.password = password;
            this.database = database;
            this.JWTencoding = JWTencoding;
        }

           
    }
}
