using MediatR;
using SCGL.EDOC.Api.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCGL.EDOC.Api.Application.Query
{
    public class GetDocumentCommand:IRequest<List<DocumentModel>>
    {
        public string DocNo { get; set; }
    }
}
