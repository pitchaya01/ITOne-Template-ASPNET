using Lazarus.Common.DAL;
using Lazarus.Common.EventMessaging;
using Lazarus.Common.Model;
using MediatR;
using SCGL.Common.EventMessaging.Command;
using SCGL.EDOC.Api.Domain;
using SCGL.EDOC.Api.Model.Document;
using SCGL.EDOC.Api.Proxy.Interface;
using SCGL.EDOC.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Application.Command
{
    public class SaveDocumentCommandHandler : IRequestHandler<SaveDocumentCommand, DocumentModel>
    {
        public IRepositoryBase<DOCUMENT> _repoDoc;
        public IRepositoryBase<GROUP_DOCUMENT> _repoGroup;
        public IEventBus _bus;
        public IMasterProxy _masterProxy;
        public IDocumentRepository _repoDocBase;

        public SaveDocumentCommandHandler(IRepositoryBase<DOCUMENT> repoDoc, IDocumentRepository repoDocBase, IRepositoryBase<GROUP_DOCUMENT> repoGroup, IEventBus bus, IMasterProxy mas)
        {
            _masterProxy = mas;
            _bus = bus;
            _repoGroup = repoGroup;
            _repoDoc = repoDoc;
            _repoDocBase = repoDocBase;
        }
        public async Task<DocumentModel> Handle(SaveDocumentCommand request, CancellationToken cancellationToken)
        {
            var customers = await _masterProxy.GetCustomer();

            for (int i = 0; i < 50; i++)
            {


                var customer = customers.FirstOrDefault(x => x.Code == request.CustomerCode);


            }


            if (request.LocationCode == "xx")
                throw new MessageError("Invalid locationCode");
 

            var doc = _repoDoc.Get(a => a.NO == request.DocumentId).FirstOrDefault();

            

            doc.DeleteDoc(DateTime.Now);

            _repoDoc.Add(doc);
            _repoDoc.Commit();



            _repoDoc.Update(doc);


            var group = new GROUP_DOCUMENT();
            group.NO = "aa";

    
                _repoDoc.Add(doc);


                _repoGroup.Add(group);

            var data = new UpdateDocumentEvent();
            data.DOCNO = "XXX";
            data.Status = "Completed";
           _bus.Publish(data);


            var result = new DocumentModel();
        if(request.DocumentId=="xxx")
            //DoSomthing
            {

            }
            return result;
        }
    }
}
