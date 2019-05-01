using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatakAPI.Models
{
    public class RouteObj
    {
        public int routeId { get; set; }
        public string name { get; set; }
        public DateTime startDatetime { get; set; }
        public DateTime endDatetime { get; set; }
        public int geojsonDocId { get; set; }
        public int reasonId { get; set; }
        public int priorityId { get; set; }
        public int statusId { get; set; }
        public int orgId { get; set; }
        public int createdByUserId { get; set; }
        public int sentToUserId { get; set; }
        public int approvedByUserId { get; set; }
        public string note { get; set; }



       
    }
}
