using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MoscowTrafficRestriction.DomainObjects.Ports
{
    public interface IReadOnlyTrafficRestrictionRepository
    {
        Task<TrafficRestriction> GetTrafficRestriction(long id);

        Task<IEnumerable<TrafficRestriction>> GetAllTrafficRestriction();

        Task<IEnumerable<TrafficRestriction>> QueryTrafficRestriction(ICriteria<TrafficRestriction> criteria);

    }

    public interface ITrafficRestrictionRepository
    {
        Task AddTrafficRestriction(TrafficRestriction trafficRestriction);

        Task RemoveTrafficRestriction(TrafficRestriction trafficRestriction);
             
        Task UpdateTrafficRestriction(TrafficRestriction trafficRestriction);
        Task ParseAndPush();
    }
}
