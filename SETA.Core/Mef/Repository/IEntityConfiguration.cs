using System.Data.Entity.ModelConfiguration.Configuration;

namespace SETA.Core.Mef.Repository
{
     public interface IEntityConfiguration
    {
        void AddConfiguration(ConfigurationRegistrar registrar);
    }

     public interface IEntityConfigurationDbo
     {
         void AddConfiguration(ConfigurationRegistrar registrar);
     }
}