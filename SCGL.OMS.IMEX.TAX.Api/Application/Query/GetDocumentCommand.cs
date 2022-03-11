using MediatR;
using SCGL.OMS.IMEX.TAX.Api.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCGL.OMS.IMEX.TAX.Api.Application.Query
{
    public class GetDocumentCommand:IRequest<List<DocumentModel>>
    {
        public string DocNo { get; set; }
    }
}
