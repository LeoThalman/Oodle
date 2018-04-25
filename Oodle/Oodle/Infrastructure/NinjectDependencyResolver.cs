using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Oodle.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
   
    
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object>  GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Bind OodleRepository to IOodleRepository, so that one is created whenever IOodleRepository is needed
            //kernel.Bind<Oodle.Models.Repos.IOodleRepository>().To<Oodle.Models.Repos.OodleRepository>();
            //Bind TestRepository to IOodleRepository
            kernel.Bind<Oodle.Models.Repos.IOodleRepository>().To<Oodle.Models.Repos.TestRepository>();

        }

    }
}