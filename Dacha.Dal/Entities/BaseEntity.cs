using System;

namespace Dacha.Dal.Entities
{
    /// <summary>
    ///     Base class for entities
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        ///     Primary key
        /// </summary>        
        public Guid Id { get; set; }
        /// <summary>
        ///   Timestamp
        /// </summary>       
        public byte[] RowVersion { get; set; }
    }

}
