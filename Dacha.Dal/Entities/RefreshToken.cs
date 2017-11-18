using System;
using System.ComponentModel.DataAnnotations;

namespace Dacha.Dal.Entities
{
    /// <summary>
    ///     Class for keeping refresh token
    ///     attributes for create entity
    /// </summary>
    public class RefreshToken
    {      
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [Required]
        public string ProtectedTicket { get; set; }
    }
}

