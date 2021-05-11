using MoscowTrafficRestriction.ApplicationServices.Ports.Cache;
using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.DomainObjects.Ports;
using MoscowTrafficRestriction.DomainObjects.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.InfrastructureServices.Repositories
{
    public class CachedReadOnlyTrafficRestrictionRepository : ReadOnlyTrafficRestrictionRepositoryDecorator
    {
        private readonly IDomainObjectsCache<TrafficRestriction> _trafficRestrictionsCache;

        public CachedReadOnlyTrafficRestrictionRepository(IReadOnlyTrafficRestrictionRepository trafficRestrictionRepository,
                                             IDomainObjectsCache<TrafficRestriction> trafficRestrictionsCache)
            : base(trafficRestrictionRepository)
            => _trafficRestrictionsCache = trafficRestrictionsCache;

        public async override Task<TrafficRestriction> GetTrafficRestriction(long id)
            => _trafficRestrictionsCache.GetObject(id) ?? await base.GetTrafficRestriction(id);

        public async override Task<IEnumerable<TrafficRestriction>> GetAllTrafficRestriction()
            => _trafficRestrictionsCache.GetObjects() ?? await base.GetAllTrafficRestriction();

        public async override Task<IEnumerable<TrafficRestriction>> QueryTrafficRestriction(ICriteria<TrafficRestriction> criteria)
            => _trafficRestrictionsCache.GetObjects()?.Where(criteria.Filter.Compile()) ?? await base.QueryTrafficRestriction(criteria);
    }
}
