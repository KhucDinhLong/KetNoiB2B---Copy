using System;

namespace SETA.Entity
{
    public abstract class AuditableEntity : Entity, IAuditableEntity    
    {
        public DateTime? CreatedDate { get; set; }

        public long? CreatedUserID { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedUserID { get; set; }
    }
}
