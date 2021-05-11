using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.ApplicationServices.Repositories
{
    public class InMemoryTrafficRestrictionRepository : IReadOnlyTrafficRestrictionRepository,
                                                    ITrafficRestrictionRepository
    {
        private readonly List<TrafficRestriction> _trafficRestriction = new List<TrafficRestriction>();

        public InMemoryTrafficRestrictionRepository(IEnumerable<TrafficRestriction> trafficRestriction = null)
        {
            if (trafficRestriction != null)
            {
                _trafficRestriction.AddRange(trafficRestriction);
            }
        }

        public Task AddTrafficRestriction(TrafficRestriction trafficRestriction)
        {
            _trafficRestriction.Add(trafficRestriction);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TrafficRestriction>> GetAllTrafficRestriction()
        {
            return Task.FromResult(_trafficRestriction.AsEnumerable());
        }

        public Task<TrafficRestriction> GetTrafficRestriction(long id)
        {
            return Task.FromResult(_trafficRestriction.Where(o => o.Id == id).FirstOrDefault());
        }

        public Task<IEnumerable<TrafficRestriction>> QueryTrafficRestriction(ICriteria<TrafficRestriction> criteria)
        {
            return Task.FromResult(_trafficRestriction.Where(criteria.Filter.Compile()).AsEnumerable());
        }

        public Task RemoveTrafficRestriction(TrafficRestriction trafficRestriction)
        {
            _trafficRestriction.Remove(trafficRestriction);
            return Task.CompletedTask;
        }

        public Task UpdateTrafficRestriction(TrafficRestriction trafficRestriction)
        {
            var foundTrafficRestruction = GetTrafficRestriction(trafficRestriction.Id).Result;
            if (foundTrafficRestruction == null)
            {
                AddTrafficRestriction(trafficRestriction);
            }
            else
            {
                if (foundTrafficRestruction != trafficRestriction)
                {
                    _trafficRestriction.Remove(foundTrafficRestruction);
                    _trafficRestriction.Add(trafficRestriction);
                }
            }
            return Task.CompletedTask;
        }
        public Task ParseAndPush()
        {
            throw new NotImplementedException();
        }
    }
}
