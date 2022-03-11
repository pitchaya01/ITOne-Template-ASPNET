using Lazarus.Common.EventMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGL.Common.EventMessaging.Command
{
    public class DNUpdatedEvent : IntegrationEvent
    {
        public DNHeader DNHeader { get; set; }
    }
    public class DNHeader
    {
        public string Dnnumber { get; set; }
        public int? LegId { get; set; }
    }
}
