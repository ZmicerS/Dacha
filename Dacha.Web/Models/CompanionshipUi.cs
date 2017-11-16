using System.ComponentModel.DataAnnotations;

namespace Dacha.Web.Models
{
    public class CompanionshipUi
    {        
        public string Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Registration { get; set; }
        [StringLength(100)]
        public string Chairman { get; set; }
        [StringLength(20)]
        public string Membership { get; set; }
        [StringLength(100)]
        public string Addition { get; set; }
    }
}