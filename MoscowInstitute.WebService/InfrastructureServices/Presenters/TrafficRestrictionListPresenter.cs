using System.Net;
using Newtonsoft.Json;
using MoscowTrafficRestriction.ApplicationServices.Ports;
using MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase;

namespace MoscowTrafficRestriction.InfrastructureServices.Presenters
{
    public class TrafficRestrictionListPresenter : IOutputPort<GetTrafficRestrictionListUseCaseResponse>
    {
        public JsonContentResult ContentResult { get; }

        public TrafficRestrictionListPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetTrafficRestrictionListUseCaseResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.TrafficRestriction) : JsonConvert.SerializeObject(response.Message);
        }
    }
}
