using System;
using System.Collections.Generic;
using SETA.Entity;

namespace SETA.DataAccess.Interface
{
    public interface IMember
    {
        int CheckExist(Member obj);
    }
}
