using SCGL.OMS.IMEX.TAX.Api.Model.Proxy.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.OMS.IMEX.TAX.Api.Proxy.Interface
{
    public interface IMasterProxy
    {
        Task<List<CustomerModel>> GetCustomer();
    }
}
