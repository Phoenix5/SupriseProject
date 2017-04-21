using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Interview.Domain.Model.Startup))]
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
