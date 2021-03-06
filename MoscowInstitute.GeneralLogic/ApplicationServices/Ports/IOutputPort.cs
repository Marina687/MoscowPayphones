using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.ApplicationServices.Ports 
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handle(TUseCaseResponse response);
    }
}
