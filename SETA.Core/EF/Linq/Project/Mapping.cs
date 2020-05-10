using System.Linq.Expressions;
using System.Reflection;

namespace SETA.Core.EF.Linq.Project
{
    class Mapping
    {
        public PropertyInfo DestPropertyInfo { get; private set; }
        public LambdaExpression Transform { get; private set; }

        public Mapping(PropertyInfo propertyInfo, LambdaExpression transform)
        {
            DestPropertyInfo = propertyInfo;
            Transform = transform;
        }
    }
}
