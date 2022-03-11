using MediatR;
using SCGL.EDOC.Api.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Application.Command
{
    public class SaveDocumentCommand:IRequest<DocumentModel>
    {
        public string DocumentId { get; set; }
        public string LocationCode { get; set; }
        public string CustomerCode { get; set; }
    }

    
}
