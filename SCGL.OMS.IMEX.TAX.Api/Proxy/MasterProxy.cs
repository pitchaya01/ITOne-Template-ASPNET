
using SCGL.EDOC.Api.Model.Proxy.Master;
using SCGL.EDOC.Api.Proxy.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Proxy
{
    public class MasterProxy : IMasterProxy
    {
        public const string BASEURL_MASTER = "";
        public async Task <List<CustomerModel>> GetCustomer()
        {
           
            //var client = new RestClient("https://scm.scglogistics.co.th/scm-api-core/api/MockData/TestTimeOut");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            return new List<CustomerModel>();
        }
    }



}
