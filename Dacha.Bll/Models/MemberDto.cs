using System;
using System.ComponentModel.DataAnnotations;

namespace Dacha.Bll.Models
{
    public class MemberDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Owner { get; set; }

        [StringLength(100)]
        public string OwnerAddress { get; set; }

        [StringLength(100)]
        public string PlotNumber { get; set; }

        [StringLength(100)]
        public string PlotAddress { get; set; }

        [StringLength(100)]
        public string PlotSquare { get; set; }

        [StringLength(100)]
        public string Addition { get; set; }

        [Required]
        public Guid? CompanionshipId { get; set; }
    }
}
