using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Domain
{
    public class MASTER_DOCTYPE
    {
        public MASTER_DOCTYPE()
        {
            this.ID = Guid.NewGuid();
        }
        [Key]

        public Guid ID { get; set; }
        public string NAME { get; set; }
    }
}
