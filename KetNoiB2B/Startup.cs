using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KetNoiB2B.Startup))]
namespace KetNoiB2B
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
