using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ContactManagement
{
    public static class UnityConfig
    {
        //IoC Dependency Injection provider
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}