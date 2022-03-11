using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Service.Interface
{
    public interface IDocumentService
    {
        void GetServiceDoc();
        void GetDocFromBlobStorage(string docNo);
    }
}
