using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FA.JustBlog.WebMVC.Startup))]
namespace FA.JustBlog.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
