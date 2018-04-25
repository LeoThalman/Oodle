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

        //This function Binds the Repos to IOodle
        private void AddBindings()
        {
            //Bind OodleRepository to IOodleRepository for production
            //kernel.Bind<Oodle.Models.Repos.IOodleRepository>().To<Oodle.Models.Repos.OodleRepository>();
            
            //Bind TestRepository to IOodleRepository for testing
            kernel.Bind<Oodle.Models.Repos.IOodleRepository>().To<Oodle.Models.Repos.TestRepository>();

        }

    }
}