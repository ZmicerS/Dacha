using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dacha.Dal.Entities
{
    public class MemberDoc : BaseEntity
    {       
        public string NameDoc { get; set; }
        public string DocumentMimeType { get; set; }
        public string Description { get; set; }
        public byte[] Document { get; set; }       
        public Guid? MemberId { get; set; }
        public virtual Member Member { get; set; }       
    }


}
