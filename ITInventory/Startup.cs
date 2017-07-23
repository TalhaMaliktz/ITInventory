using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITInventory.Startup))]
namespace ITInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
