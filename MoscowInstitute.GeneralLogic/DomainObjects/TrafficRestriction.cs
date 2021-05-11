using System;
using System.Collections.Generic;
using System.Text;

namespace MoscowTrafficRestriction.DomainObjects
{
    public class TrafficRestriction : DomainObject 
    {

        public string DescriptionLocation { get; set; }

        public string PayWay { get; set; }

        public string IntercityConnectionPayment { get; set; }

        public string Name { get; set; }

        public string ValidUniversalServicesCard { get; set; }
        
    }
}
