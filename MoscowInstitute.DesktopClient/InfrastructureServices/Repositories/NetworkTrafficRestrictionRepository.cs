using MoscowTrafficRestriction.ApplicationServices.Ports.Cache;
using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.InfrastructureServices.Repositories
{
    public class NetworkTrafficRestrictionRepository : NetworkRepositoryBase, IReadOnlyTrafficRestrictionRepository
    {
        private readonly IDomainObjectsCache<TrafficRestriction> _trafficRestrictionCache;

        public NetworkTrafficRestrictionRepository(string host, ushort port, bool useTls, IDomainObjectsCache<TrafficRestriction> trafficRestrictionCache)
            : base(host, port, useTls)
            => _trafficRestrictionCache = trafficRestrictionCache;

        public async Task<TrafficRestriction> GetTrafficRestriction(long id)
            => CacheAndReturn(await ExecuteHttpRequest<TrafficRestriction>($"TrafficRestriction/{id}"));

        public async Task<IEnumerable<TrafficRestriction>> GetAllTrafficRestriction()
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<TrafficRestriction>>($"TrafficRestriction"), allObjects: true);

        public async Task<IEnumerable<TrafficRestriction>> QueryTrafficRestriction(ICriteria<TrafficRestriction> criteria)
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<TrafficRestriction>>($"TrafficRestriction"), allObjects: true)
               .Where(criteria.Filter.Compile());

        private IEnumerable<TrafficRestriction> CacheAndReturn(IEnumerable<TrafficRestriction> trafficRestriction, bool allObjects = false)
        {
            if (allObjects)
            {
                _trafficRestrictionCache.ClearCache();
            }
            _trafficRestrictionCache.UpdateObjects(trafficRestriction, DateTime.Now.AddDays(1), allObjects);
            return trafficRestriction;
        }

        private TrafficRestriction CacheAndReturn(TrafficRestriction trafficRestriction)
        {
            _trafficRestrictionCache.UpdateObject(trafficRestriction, DateTime.Now.AddDays(1));
            return trafficRestriction;
        }
    }
}
