using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Northwind.FruitStore.WebUI.Startup))]
namespace Northwind.FruitStore.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
