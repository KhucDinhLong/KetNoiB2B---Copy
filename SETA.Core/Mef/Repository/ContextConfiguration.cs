using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SETA.Core.Mef.Repository
{
    public class ContextConfiguration
    {
        [ImportMany(typeof(IEntityConfiguration))]
        public IEnumerable<IEntityConfiguration> Configurations { get; set; }

        [ImportMany(typeof(IEntityConfigurationDbo))]
        public IEnumerable<IEntityConfigurationDbo> ConfigurationsDbo { get; set; }
    }
}
