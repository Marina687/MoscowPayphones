using MoscowTrafficRestriction.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.ApplicationServices.Ports.Gateways.Database
{
    public interface ITrafficRestrictionDatabaseGateway
    {
        Task AddTrafficRestriction(TrafficRestriction trafficRestriction);

        Task RemoveTrafficRestriction(TrafficRestriction trafficRestriction);

        Task UpdateTrafficRestriction(TrafficRestriction trafficRestriction);

        Task<TrafficRestriction> GetTrafficRestriction(long id);

        Task<IEnumerable<TrafficRestriction>> GetAllTrafficRestriction();

        Task<IEnumerable<TrafficRestriction>> QueryTrafficRestriction(Expression<Func<TrafficRestriction, bool>> filter);
        Task ParseAndPush();
    }
}
