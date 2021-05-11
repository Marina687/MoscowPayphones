using MoscowTrafficRestriction.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase
{
    public class GetTrafficRestrictionListUseCaseRequest : IUseCaseRequest<GetTrafficRestrictionListUseCaseResponse>
    {
        public string Name { get; private set; }
        public long? TrafficRestrictionId { get; private set; }

        private GetTrafficRestrictionListUseCaseRequest()
        { }

        public static GetTrafficRestrictionListUseCaseRequest CreateAllTrafficRestrictionRequest()
        {
            return new GetTrafficRestrictionListUseCaseRequest();
        }

        public static GetTrafficRestrictionListUseCaseRequest CreateTrafficRestrictionRequest(long trafficRestrictionId)
        {
            return new GetTrafficRestrictionListUseCaseRequest() { TrafficRestrictionId = trafficRestrictionId };
        }
            public static GetTrafficRestrictionListUseCaseRequest CreateEventTrafficRestrictionRequest(string Name)
            {
            return new GetTrafficRestrictionListUseCaseRequest() { Name = Name };  
        }

        }
    }


