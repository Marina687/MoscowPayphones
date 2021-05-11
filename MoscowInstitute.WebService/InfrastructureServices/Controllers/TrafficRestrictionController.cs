using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase;
using MoscowTrafficRestriction.InfrastructureServices.Presenters;

namespace MoscowTrafficRestriction.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficRestrictionController : ControllerBase
    {
        private readonly ILogger<TrafficRestrictionController> _logger;
        private readonly IGetTrafficRestrictionListUseCase _getTrafficRestrictionListUseCase;

        public TrafficRestrictionController(ILogger<TrafficRestrictionController> logger, IGetTrafficRestrictionListUseCase getTrafficRestrictionListUseCase)
        {
            _logger = logger;
            _getTrafficRestrictionListUseCase = getTrafficRestrictionListUseCase;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTrafficRestrictions()
        {
            var presenter = new MoscowTrafficRestriction.InfrastructureServices.Presenters.TrafficRestrictionListPresenter();
            await _getTrafficRestrictionListUseCase.Handle(GetTrafficRestrictionListUseCaseRequest.CreateAllTrafficRestrictionRequest(), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("{trafficRestrictionId}")]
        public async Task<ActionResult> GetTrafficRestriction(long trafficRestrictionId)
        {
            var presenter = new MoscowTrafficRestriction.InfrastructureServices.Presenters.TrafficRestrictionListPresenter();
            await _getTrafficRestrictionListUseCase.Handle(GetTrafficRestrictionListUseCaseRequest.CreateTrafficRestrictionRequest(trafficRestrictionId), presenter);
            return presenter.ContentResult;
        }
        [HttpGet("Events/{events}")]
        public async Task<ActionResult> GetEventFilter(string Event)
        {
            var presenter = new TrafficRestrictionListPresenter();
            await _getTrafficRestrictionListUseCase.Handle(GetTrafficRestrictionListUseCaseRequest.CreateEventTrafficRestrictionRequest(Event), presenter);
            return presenter.ContentResult;
        }
    }
}
