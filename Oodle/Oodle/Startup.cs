using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Oodle.Startup))]
namespace Oodle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
