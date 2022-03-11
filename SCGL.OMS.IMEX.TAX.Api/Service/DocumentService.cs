using SCGL.OMS.IMEX.TAX.Api.Repository.Interface;
using SCGL.OMS.IMEX.TAX.Api.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.OMS.IMEX.TAX.Api.Service
{
    public class DocumentService : IDocumentService
    {
        public IDocumentRepository _dRepo;
        public DocumentService(IDocumentRepository drepo)
        {
            _dRepo = drepo;
        }

        public void GetDocFromBlobStorage(string docNo)
        {
             // CallBolob
        }

        public void GetServiceDoc()
        {
            throw new NotImplementedException();
        }
    }
}
