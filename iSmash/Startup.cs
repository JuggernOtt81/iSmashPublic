using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iSmash.Startup))]
namespace iSmash
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
