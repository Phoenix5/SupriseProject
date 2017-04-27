using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Interview.Domain.Model.Startup))]

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]

namespace Interview.Domain.Model
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
    }
}
