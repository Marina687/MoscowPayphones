using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.DomainObjects.Repositories
{
    public abstract class ReadOnlyTrafficRestrictionRepositoryDecorator : IReadOnlyTrafficRestrictionRepository
    {
        private readonly IReadOnlyTrafficRestrictionRepository _trafficRestrictionRepository;

        public ReadOnlyTrafficRestrictionRepositoryDecorator(IReadOnlyTrafficRestrictionRepository trafficRestrictionRepository)
        {
            _trafficRestrictionRepository = trafficRestrictionRepository;
        }

        public virtual async Task<IEnumerable<TrafficRestriction>> GetAllTrafficRestriction()
        {
            return await _trafficRestrictionRepository?.GetAllTrafficRestriction();
        }

        public virtual async Task<TrafficRestriction> GetTrafficRestriction(long id)
        {
            return await _trafficRestrictionRepository?.GetTrafficRestriction(id);
        }

        public virtual async Task<IEnumerable<TrafficRestriction>> QueryTrafficRestriction(ICriteria<TrafficRestriction> criteria)
        {
            return await _trafficRestrictionRepository?.QueryTrafficRestriction(criteria);
        }
    }
}
