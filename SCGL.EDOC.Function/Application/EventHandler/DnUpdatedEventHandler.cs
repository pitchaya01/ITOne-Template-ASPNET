using Lazarus.Common.DAL;
using Lazarus.Common.EventMessaging;
using SCGL.Common.EventMessaging.Command;
using SCGL.EDOC.Api.Domain;
using SCGL.EDOC.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGL.EDOC.Function.Application.EventHandler
{
    public class DnUpdatedEventEventHandler : IIntegrationEventHandler<DNUpdatedEvent>
    {
        public IRepositoryBase<DOCUMENT> _repoDoc;
        public IDocumentRepository _repoDocBase;
       
        public DnUpdatedEventEventHandler(IDocumentRepository repoDocBase, IRepositoryBase<DOCUMENT> repoDoc)
        {
            _repoDoc = repoDoc;
            _repoDocBase = repoDocBase;
        }
        public async Task Handle(DNUpdatedEvent @event)
        {
            /// Query logic & Business logic
        }

        public async Task Validate(DNUpdatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
