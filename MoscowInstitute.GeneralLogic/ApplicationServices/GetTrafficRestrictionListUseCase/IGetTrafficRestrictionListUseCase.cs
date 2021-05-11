using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoscowTrafficRestriction.ApplicationServices.Interfaces;

namespace MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase
{
    public interface IGetTrafficRestrictionListUseCase : IUseCase<GetTrafficRestrictionListUseCaseRequest, GetTrafficRestrictionListUseCaseResponse>
    {
    }
}
