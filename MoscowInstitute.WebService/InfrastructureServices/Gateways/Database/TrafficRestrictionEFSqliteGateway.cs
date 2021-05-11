using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.ApplicationServices.Ports.Gateways.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using MoscowTrafficRestriction.WebService.InfrastructureServices.Gateways;
using System.Text;
using Newtonsoft.Json;

namespace MoscowTrafficRestriction.InfrastructureServices.Gateways.Database
{
    public class TrafficRestrictionEFSqliteGateway : ITrafficRestrictionDatabaseGateway
    {
        private readonly TrafficRestrictionContext _trafficRestrictionContext;

        public TrafficRestrictionEFSqliteGateway(TrafficRestrictionContext trafficRestrictionContext)
            => _trafficRestrictionContext = trafficRestrictionContext;

        public async Task<TrafficRestriction> GetTrafficRestriction(long id)
           => await _trafficRestrictionContext.TrafficRestrictions.Where(r => r.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<TrafficRestriction>> GetAllTrafficRestriction()
            => await _trafficRestrictionContext.TrafficRestrictions.ToListAsync();

        public async Task<IEnumerable<TrafficRestriction>> QueryTrafficRestriction(Expression<Func<TrafficRestriction, bool>> filter)
            => await _trafficRestrictionContext.TrafficRestrictions.Where(filter).ToListAsync();

        public async Task AddTrafficRestriction(TrafficRestriction trafficRestriction)
        {
            _trafficRestrictionContext.TrafficRestrictions.Add(trafficRestriction);
            await _trafficRestrictionContext.SaveChangesAsync();
        }

        public async Task UpdateTrafficRestriction(TrafficRestriction trafficRestriction)
        {
            _trafficRestrictionContext.Entry(trafficRestriction).State = EntityState.Modified;
            await _trafficRestrictionContext.SaveChangesAsync();
        }

        public async Task RemoveTrafficRestriction(TrafficRestriction trafficRestriction)
        {
            _trafficRestrictionContext.TrafficRestrictions.Remove(trafficRestriction);
            await _trafficRestrictionContext.SaveChangesAsync();
        }

        public async Task ParseAndPush()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string result = client.DownloadString("https://apidata.mos.ru/v1/datasets/841/rows?$top=1000&api_key=c941a998bbb9e1e374fc2d7a33f61ed0");
            List<ResultFromServer> resultServer = JsonConvert.DeserializeObject<List<ResultFromServer>>(result);
            var optionsBuilder = new DbContextOptionsBuilder<TrafficRestrictionContext>();
            optionsBuilder.UseSqlite("Data Source=C:/Users/daria/Desktop/8laba/MoscowInstitute.WebService/MoscowTrafficRestriction.db"); ;
            var context = new TrafficRestrictionContext(options: optionsBuilder.Options);
            context.Database.ExecuteSqlRaw("DELETE FROM TrafficRestrictions");
            using (context)
            {
                foreach (var item in resultServer)
                {
                    DomainObjects.TrafficRestriction trafficRestriction = new DomainObjects.TrafficRestriction();
                    trafficRestriction.Name = item.Cells.Name;
                    trafficRestriction.DescriptionLocation = item.Cells.DescriptionLocation;
                    trafficRestriction.PayWay = item.Cells.PayWay;
                    trafficRestriction.IntercityConnectionPayment = item.Cells.IntercityConnectionPayment;
                    trafficRestriction.ValidUniversalServicesCard = item.Cells.ValidUniversalServicesCard;
                    context.Entry(trafficRestriction).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
            await Task.CompletedTask;
        }
    }
}
