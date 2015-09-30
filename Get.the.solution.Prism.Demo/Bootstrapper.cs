using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.ComponentModel.Composition.ReflectionModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Get.the.solution.Common;
using Prism.Mef;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel.Composition.Primitives;

namespace Get.the.solution.Prism.Demo
{
    /// <summary>
    /// Implements the main functionality for the MefBootstrapper for registering and importing classes
    /// </summary>
    public class Bootstrapper : MefBootstrapper
    {
        /// <summary>
        /// 1. Populates the Module Catalog.
        /// </summary>
        /// <returns>A new Module Catalog.</returns>
        /// <remarks>
        /// This method uses the Module Discovery method of populating the Module Catalog. It requires a post-build event in each module to place
        /// the module assembly in the module catalog directory.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = new DirectoryModuleCatalog();
            moduleCatalog.ModulePath = @".\Modules";
            return moduleCatalog;
        }
        /// <summary>
        /// 2. The ConfigureAggregateCatalog method allows you to add type registrations to the AggregateCatalog imperatively. 
        /// Btw. register all necessary types.
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            //https://stefanhenneken.wordpress.com/2013/01/21/mef-teil-11-neuerungen-unter-net-4-5/
            RegistrationBuilder registrationBuilder = new RegistrationBuilder();
            //export all classes which implement IWCFService
            registrationBuilder.ForTypesDerivedFrom<IWCFService>().Export<IWCFService>();
            //export the menu interface
            registrationBuilder.ForTypesDerivedFrom<IMenuItem>().Export<IMenuItem>();
            //inject the mainwindowviewmodel into the datacontext property
            registrationBuilder.ForType<MainWindow>().ImportProperty<MainWindowViewModel>(p => p.DataContext);
            //add current assembly to catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly, registrationBuilder));

            registrationBuilder = new RegistrationBuilder();
            //make sure the IMenuItem will be exported
            registrationBuilder.ForTypesDerivedFrom<IMenuItem>().Export<IMenuItem>();

            //add common project to catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly, registrationBuilder));

        }
        /// <summary>
        /// 3. 
        /// </summary>
        /// <returns></returns>
        protected override CompositionContainer CreateContainer()
        {
            var container = base.CreateContainer();
            //export container - so we can register additional types in the modules by importing the container - recomposing
            container.ComposeExportedValue(container);
            return container;
        }
        /// <summary>
        /// 4.
        /// </summary>
        protected override void RegisterBootstrapperProvidedTypes()
        {
            //call base method which register the 4 exports for the types logger, module, service and aggregation
            base.RegisterBootstrapperProvidedTypes();

            //register agent
            this.Container.ComposeExportedValue<IWCFService>(new WCFService());
        }
        /// <summary>
        /// 5.
        /// </summary>
        /// <returns></returns>
        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }
        /// <summary>
        /// 6.
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<MainWindow>();
        }
        /// <summary>
        /// 7.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
