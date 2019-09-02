using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace WebApplication1
{
    public class NinjectDep2endencyResolver : IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDep2endencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}