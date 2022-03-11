using Lazarus.Common.DAL;
using SCGL.EDOC.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Repository.Interface
{
    public interface IDocumentRepository:IRepositoryBase<DOCUMENT>
    {
        string GetDoc();
        string FindDocExpire();

    }
}
