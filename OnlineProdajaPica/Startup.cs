using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineProdajaPica.Startup))]
namespace OnlineProdajaPica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
