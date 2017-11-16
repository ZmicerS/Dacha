using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dacha.Dal.Entities
{
    public class Member : BaseEntity
    {      
        public string Owner { get; set; }
        public string OwnerAddress { get; set; }
        public string PlotNumber { get; set; }
        public string PlotAddress { get; set; }
        public string PlotSquare { get; set; }
        public string Addition { get; set; }
        public Guid? CompanionshipId { get; set; }        
        /// <summary>
        ///     Navigation to CompanionShip
        /// </summary>
        public virtual Companionship Companionship { get; set; }      
        public  ICollection<MemberDoc> MemberDocs { get; set; }
        public Member()
        {
            MemberDocs = new List<MemberDoc>();
        }
    }



}
