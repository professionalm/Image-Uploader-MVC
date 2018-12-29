using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCImageUplaoder.Startup))]
namespace MVCImageUplaoder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
