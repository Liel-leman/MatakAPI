using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatakAPI.Models
{
    public class RouteObj
    {
        public int RouteId { get; set; }
        public string Name { get; set; }
        public DateTime StartDatetime { get; set; }
        public DateTime EndDatetime { get; set; }
        public int GeojsonDocId { get; set; }
        public int ReasonId { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int OrgId { get; set; }
        public int CreatedByUserId { get; set; }
        public int SentToUserId { get; set; }
        public int ApprovedByUserId { get; set; }
        public string Note { get; set; }
        public string GeoJsonString { get; set; }




    }
}
