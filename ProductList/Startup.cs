using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductList.Startup))]
namespace ProductList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
