using System;
using System.ComponentModel.DataAnnotations;

namespace Dacha.Bll.Models
{
    public class MemberDocDto
    {        
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string NameDoc { get; set; }
        [Required]
        public string DocumentMimeType { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public byte[] Document { get; set; }
        [Required]
        public Guid MemberId { get; set; }       
    }
}
