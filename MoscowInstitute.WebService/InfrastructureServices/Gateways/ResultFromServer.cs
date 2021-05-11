using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.WebService.InfrastructureServices.Gateways
{
    public class Cells
    {

        public string Name { get; set; }

        public string DescriptionLocation { get; set; }

        public string PayWay { get; set; }

        public string IntercityConnectionPayment { get; set; }

        public string ValidUniversalServicesCard { get; set; }


    }

    public class ResultFromServer
    {
        public int Number { get; set; }
        public Cells Cells { get; set; }
    }
}
