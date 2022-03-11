using SCGL.OMS.IMEX.TAX.Api.Domain;
using SCGL.OMS.IMEX.TAX.Api.Infrastructure;
using SCGL.OMS.IMEX.TAX.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.OMS.IMEX.TAX.Api.Repository
{
    public class DocumentRepository : RepositoryBase<DOCUMENT>, IDocumentRepository
    {
        public DbDataContext _db;
        public DbReadDataContext _dbRead;
        public DocumentRepository(DbDataContext db, DbReadDataContext dbRead) : base(db, dbRead)
        {
            _dbRead = dbRead;
            _db = db;
        }

        public string FindDocExpire()
        {
            var expireDoc = _db.DOCUMENTS.FirstOrDefault();
            return expireDoc.NO;
        }

        public string GetDoc()
        {
            var aa = _db.DOCUMENTS.FirstOrDefault();

            var dd = (from doc in _db.DOCUMENTS
                      join m in _db.MASTER_DOCTYPES on doc.NO equals m.NAME
                      select new
                      {
                          Test = m.NAME
                      }).ToList();

            return aa.NO;
        }
    }
}
