using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakDBConnector;

namespace MatakAPI.Models
{
    public class UsrObj
    { 

        public int UsedId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Nickname { get; set; }

        public UsrObj(User usr)
        {
            UsedId = usr.UsedId;
            LastName = usr.LastName;
            FirstName = usr.FirstName;
            Nickname = usr.Nickname;
        }
    }

}
