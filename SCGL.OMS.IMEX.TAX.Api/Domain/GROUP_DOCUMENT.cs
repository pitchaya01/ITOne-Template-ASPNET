using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Domain
{
    public class GROUP_DOCUMENT
    {
        public GROUP_DOCUMENT()
        {
            this.ID = Guid.NewGuid();
        }
        [Key]

        public Guid ID { get; set; }
        public string NO { get; set; }
    }
}
