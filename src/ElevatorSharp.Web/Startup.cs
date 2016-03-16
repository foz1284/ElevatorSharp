using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElevatorSharp.Web.Startup))]
namespace ElevatorSharp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
