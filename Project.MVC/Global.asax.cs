using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using AutoMapper;
using SimpleInjector;
using System.Reflection;
using Project.Service.Models;
using Project.MVC.Models;
using SimpleInjector.Integration.Web.Mvc;

namespace Project.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            var container = new Container();
            container.Register< Service.Service.IVehicleMakeServis, Service.Service.VehicleMakeServis >();
            container.Register<Service.Service.IVehicleModelServis, Service.Service.VehicleModelServis>();         
           
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver (container) );

            Mapper.Initialize(config: cfg => { cfg.CreateMap<VehicleMake, VehicleMakeVM>(); cfg.CreateMap<VehicleModel, VehicleModelVM>(); });

            //Mapper.Configuration.AssertConfigurationIsValid();
        }
            
    }
}
