using System.Collections.Generic;

namespace Dacha.Dal.Entities
{
    public class Companionship : BaseEntity
    {     
        public string Name { get; set; }
        public string Address { get; set; }
        public string Registration { get; set; }
        public string Chairman { get; set; }
        public string Membership { get; set; }
        public string Addition { get; set; }
        /// <summary>
        ///     Navigation to  Members collection
        /// </summary>
        public  ICollection<Member> Members { get; set; }
        public Companionship()
        {
            Members = new List<Member>();
        }
    }
}
