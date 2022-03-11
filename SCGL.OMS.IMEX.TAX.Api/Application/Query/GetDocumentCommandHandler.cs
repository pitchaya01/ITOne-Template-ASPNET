using Lazarus.Common.DAL;
using Lazarus.Common.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SCGL.EDOC.Api.Domain;
using SCGL.EDOC.Api.Infrastructure;
using SCGL.EDOC.Api.Model.Document;
using SCGL.EDOC.Api.Repository.Interface;
using SCGL.EDOC.Api.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Application.Query
{
    public class GetDocumentCommandHandler : IRequestHandler<GetDocumentCommand, List<DocumentModel>>
    {
        public IDocumentService _dService;
        public IDocumentRepository _dRepo;
        public IRepositoryBase<DOCUMENT> _docRepo;
        public DbDataContext _db;
        public IRepositoryBase<MASTER_DOCTYPE> _repoDocType;
        public GetDocumentCommandHandler(DbDataContext db,IDocumentService dService, IDocumentRepository dRepo, IRepositoryBase<DOCUMENT> docRepo, IRepositoryBase<MASTER_DOCTYPE> mdoc)
        {
            _db = db;
            _repoDocType = mdoc;
            _dService = dService;
            _dRepo = dRepo;
            _docRepo = docRepo;
        }
        public async Task<List<DocumentModel>> Handle(GetDocumentCommand request, CancellationToken cancellationToken)
        {
            var ddd = AppConfigUtilities.GetAppConfig<string>("JwtKey");
            var db = new DbDataContext(DbContextOptionsFactory.GetDbContext());
        
            var dd = _db.Database.GetDbConnection().ConnectionString;
            var query = _dRepo.Query().FirstOrDefault();
            var docType = _repoDocType.Get(a => a.ID != null).ToList();

            var expire = _dRepo.FindDocExpire();

 

            return null;
        }
    }
}
