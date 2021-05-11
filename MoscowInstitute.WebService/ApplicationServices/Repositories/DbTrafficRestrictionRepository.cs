using System;
using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.DomainObjects.Ports;
using MoscowTrafficRestriction.ApplicationServices.Ports.Gateways.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.ApplicationServices.Repositories
{
    public class DbTrafficRestrictionRepository : IReadOnlyTrafficRestrictionRepository,
                                                  ITrafficRestrictionRepository
    {
        private readonly ITrafficRestrictionDatabaseGateway _databaseGateway;

        public DbTrafficRestrictionRepository(ITrafficRestrictionDatabaseGateway databaseGateway)
            => _databaseGateway = databaseGateway;

        public async Task<TrafficRestriction> GetTrafficRestriction(long id)
            => await _databaseGateway.GetTrafficRestriction(id);

        public async Task<IEnumerable<TrafficRestriction>> GetAllTrafficRestriction()
            => await _databaseGateway.GetAllTrafficRestriction();

        public async Task<IEnumerable<TrafficRestriction>> QueryTrafficRestriction(ICriteria<TrafficRestriction> criteria)
            => await _databaseGateway.QueryTrafficRestriction(criteria.Filter);

        public async Task AddTrafficRestriction(TrafficRestriction trafficRestriction)
            => await _databaseGateway.AddTrafficRestriction(trafficRestriction);

        public async Task RemoveTrafficRestriction(TrafficRestriction trafficRestriction)
            => await _databaseGateway.RemoveTrafficRestriction(trafficRestriction);

        public async Task UpdateTrafficRestriction(TrafficRestriction trafficRestriction)
            => await _databaseGateway.UpdateTrafficRestriction(trafficRestriction);

        public async Task ParseAndPush()
            => await _databaseGateway.ParseAndPush();
    }
}
