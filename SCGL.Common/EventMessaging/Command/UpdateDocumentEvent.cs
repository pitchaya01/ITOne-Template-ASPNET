using Lazarus.Common.EventMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCGL.Common.EventMessaging.Command
{
 


    public class UpdateDocumentEvent: IntegrationEvent
    {
        public string DOCNO { get; set; }
        public string Status { get; set; }
    }
}
