using Autofac.Extras.Multitenant;

namespace SETA.Core.Mvc
{
    public class RequestParameterStrategy : ITenantIdentificationStrategy
    {

        public string keyName { get; set; }

        public RequestParameterStrategy(string keyName = "tenant")
        {
            this.keyName = keyName;
        }

        public bool TryIdentifyTenant(out object tenantKey)
        {
            tenantKey = null;
            return true;
        }

    }
}
