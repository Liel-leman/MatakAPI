using MatakDBConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatakAPI.Models
{
    public class OrgObj
    {
        public int OrgId { get; set; }
        public string Name { get; set; }
        
        public OrgObj(Organization org)
        {
            this.OrgId = org.OrgId;
            this.Name = org.Name;
        }

        
    }
}
