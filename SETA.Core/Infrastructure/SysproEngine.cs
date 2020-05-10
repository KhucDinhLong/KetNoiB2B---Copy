using System;
using SETA.Core.Configuration;
using SETA.Core.Data;
using SETA.Core.Infrastructure.DependencyManagement;

namespace SETA.Core.Infrastructure
{

    /// <summary>
    /// Class SETAEngine
    /// </summary>
    public class SETAEngine : IEngine
    {

        #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Properties

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion

        public void Initialize(SETAConfig config)
        {
            var databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            if (databaseInstalled)
            {
                //startup tasks
                //RunStartupTasks();
            }
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
    }
}
