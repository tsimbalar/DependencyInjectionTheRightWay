using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace BoringBank.WebPortal
{
    public class AppCompositionRoot : DefaultControllerFactory
    {
        private readonly IUnityContainer _unityContainer;

        public AppCompositionRoot(IUnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");
            _unityContainer = unityContainer;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController) _unityContainer.Resolve(controllerType);
        }
    }
}
