using Lazarus.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Domain
{
    public class DOCUMENT
    {
        public DOCUMENT()
        {
            this.ID = Guid.NewGuid();
        }
        [Key]
        
        public Guid ID { get;private set; }
        public string NO { get; private set; }
        public bool IsActive { get; private set; }
        public string GroupNo { get; private set; }



        public static DOCUMENT SaveDocument(string no,string Id,string groupNo)
        {
            var doc = new DOCUMENT();
            doc.NO = no;
            doc.ID = Id.ToGuid();
            doc.GroupNo = groupNo;
            return doc;
        }
        public void DeleteDoc(DateTime DateTime)
        {
            
            this.IsActive = false;
            //SendEmail
        }
        public void JoinDoc()
        {

        }
    }
}
