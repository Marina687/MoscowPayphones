using System.Threading.Tasks;
using System.Collections.Generic;
using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.DomainObjects.Ports;
using MoscowTrafficRestriction.ApplicationServices.Ports;

namespace MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase
{
    public class GetTrafficRestrictionListUseCase : IGetTrafficRestrictionListUseCase
    {
        private readonly IReadOnlyTrafficRestrictionRepository _readOnlyTrafficRestrictionRepository;

        public GetTrafficRestrictionListUseCase(IReadOnlyTrafficRestrictionRepository readOnlyTrafficRestrictionRepository)
            => _readOnlyTrafficRestrictionRepository = readOnlyTrafficRestrictionRepository;

        public async Task<bool> Handle(GetTrafficRestrictionListUseCaseRequest request, IOutputPort<GetTrafficRestrictionListUseCaseResponse> outputPort)
        {
            IEnumerable<TrafficRestriction> trafficRestrictions = null;
            if (request.TrafficRestrictionId != null)
            {
                var trafficRestriction = await _readOnlyTrafficRestrictionRepository.GetTrafficRestriction(request.TrafficRestrictionId.Value);
                trafficRestrictions = (trafficRestriction != null) ? new List<TrafficRestriction>() { trafficRestriction } : new List<TrafficRestriction>();

            }
            else if (request.Name != null)
            {
                trafficRestrictions = await _readOnlyTrafficRestrictionRepository.QueryTrafficRestriction(new EventCriteria(request.Name));
            }
            else
            {
                trafficRestrictions = await _readOnlyTrafficRestrictionRepository.GetAllTrafficRestriction();
            }
            outputPort.Handle(new GetTrafficRestrictionListUseCaseResponse(trafficRestrictions));
            return true;
        }
    }
}
