using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DealDouble.Web.Startup))]
namespace DealDouble.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
