using System;

namespace SETA.Entity
{
    public interface IAuditableEntity 
    {
        DateTime? CreatedDate { get; set; }
        long? CreatedUserID { get; set; }
        DateTime? UpdatedDate { get; set; }
        long? UpdatedUserID { get; set; }
    }
}
