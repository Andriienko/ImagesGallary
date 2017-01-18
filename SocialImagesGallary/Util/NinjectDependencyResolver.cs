using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using BLL.Services;
using BLL.Interfaces;
using Ninject;
using System.Web.Http.Dependencies;
using Ninject.Syntax;

namespace SocialImagesGallary.Util
{
    public class NinjectDependencyScope : IDependencyScope
    {
        IResolutionRoot resolver;
        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }
        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            resolver = null;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.GetAll(serviceType);
        }
    }
    public class NinjectDependencyResolver : NinjectDependencyScope,IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam):base(kernelParam)
        {
            kernel = kernelParam;
            //AddBindings();
        }
        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }

        //public object GetService(Type serviceType)
        //{
        //    return kernel.TryGet(serviceType);
        //}
        //public IEnumerable<object> GetServices(Type serviceType)
        //{
        //    return kernel.GetAll(serviceType);
        //}
        //private void AddBindings()
        //{
        //    kernel.Bind<IUserService>().To<UserService>();
        //}
    }
}