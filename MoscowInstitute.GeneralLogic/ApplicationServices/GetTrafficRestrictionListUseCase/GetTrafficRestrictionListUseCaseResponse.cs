using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase
{
    public class GetTrafficRestrictionListUseCaseResponse : UseCaseResponse
    {
        public IEnumerable<TrafficRestriction> TrafficRestriction { get; }

        public GetTrafficRestrictionListUseCaseResponse(IEnumerable<TrafficRestriction> trafficRestriction) => TrafficRestriction = trafficRestriction;
    }
}
