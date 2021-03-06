using Lazarus.Common.ExceptionHandling;
using Lazarus.Common.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCGL.OMS.IMEX.TAX.Api.Application.Command;
using SCGL.OMS.IMEX.TAX.Api.Application.Query;
using SCGL.OMS.IMEX.TAX.Api.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.OMS.IMEX.TAX.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiException]

    public class DocumentController : ControllerBase
    {
        public IMediator _mediator;
        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseResult<string>> GetDocument()
        {
            
            var result = await _mediator.Send(new GetDocumentCommand());
            result.Add(new DocumentModel() { DocNo = "aa" });
            return ResponseResult<string>.Success();
        }

        [HttpPost]
        public async Task<ResponseResult<DocumentModel>> SaveDoc(SaveDocumentCommand param)
        {
            var result = await _mediator.Send(param);
            return ResponseResult<DocumentModel>.Success(result);
        }


    }
}
