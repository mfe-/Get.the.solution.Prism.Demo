using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Get.the.solution.Common;
using Get.the.solution.Prism.Modul;
using Get.the.solution.Prism.Modul.Other;
using Prism.Mef;
using Prism.Regions;

namespace Get.the.solution.Prism.Demo
{
    public class Bootstrapper : MefBootstrapper 
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<MainWindow>();
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Show();
        }
        /// <summary>
        /// The ConfigureAggregateCatalog method allows you to add type registrations to the AggregateCatalog imperatively. 
        /// Btw. register all necessary types.
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            //https://stefanhenneken.wordpress.com/2013/01/21/mef-teil-11-neuerungen-unter-net-4-5/
            RegistrationBuilder registrationBuilder = new RegistrationBuilder();
            //export all classes which implement IWCFService
            registrationBuilder.ForTypesDerivedFrom<IWCFService>().Export<IWCFService>();
            //export the menu interface
            registrationBuilder.ForTypesDerivedFrom<IMenu>().Export<IMenu>();
            //inject the mainwindowviewmodel into the datacontext property
            registrationBuilder.ForType<MainWindow>().ImportProperty<MainWindowViewModel>(p => p.DataContext);

            //add current assembly to catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly, registrationBuilder));
            //add common project to catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly, registrationBuilder));
            //add our modul usercontrol1 and usercontrol2 to catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(UserControl1).Assembly, registrationBuilder));
            //add our modul UserControlOther to catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(UserControlOther).Assembly, registrationBuilder));

        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }
        protected override CompositionContainer CreateContainer()
        {
            
            var container = base.CreateContainer();
            //export container - so we can register additional types in the modules by importing the container
            container.ComposeExportedValue(container);
            return container;
        }
        protected override void RegisterBootstrapperProvidedTypes()
        {
            //call base method which register the 4 exports for the types logger, module, service and aggregation
            base.RegisterBootstrapperProvidedTypes();

            //register agent
            this.Container.ComposeExportedValue<IWCFService>(new WCFService());
        }
    }
}
