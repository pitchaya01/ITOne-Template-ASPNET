using Lazarus.Common.DAL;
using SCGL.OMS.IMEX.TAX.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.OMS.IMEX.TAX.Api.Repository.Interface
{
    public interface IDocumentRepository:IRepositoryBase<DOCUMENT>
    {
        string GetDoc();
        string FindDocExpire();

    }
}
