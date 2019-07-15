using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakDBConnector;

namespace MatakAPI.Models
{
    public class UsrObj
    { 

        public int UserId { get; set; }
        public int OrgId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Nickname { get; set; }

        public UsrObj(User usr)
        {
            OrgId = usr.OrgId;
            UserId = usr.UserId;
            LastName = usr.LastName;
            FirstName = usr.FirstName;
            Nickname = usr.Nickname;

        }
    }

}
