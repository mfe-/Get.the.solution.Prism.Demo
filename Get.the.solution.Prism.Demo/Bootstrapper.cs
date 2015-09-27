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
using Get.the.solution.Prism.Modul;
using Get.the.solution.Prism.Modul.Other;
using Prism.Mef;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel.Composition.Primitives;

namespace Get.the.solution.Prism.Demo
{
    public class Bootstrapper : MefBootstrapper
    {
        /// <summary>
        /// 1.
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
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
            //add our modul usercontrol1 and usercontrol2 to catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(UserControl1).Assembly, registrationBuilder));
            //add our modul UserControlOther to catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(UserControlOther).Assembly, registrationBuilder));

        }
        /// <summary>
        /// 3. 
        /// </summary>
        /// <returns></returns>
        protected override CompositionContainer CreateContainer()
        {
            var container = base.CreateContainer();
            //export container - so we can register additional types in the modules by importing the container
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

        //public static IEnumerable<Type> GetExportedTypes<T>()
        //{
        //    return catalog.Parts
        //        .Where(part => IsPartOfType(part, typeof(T)))
        //        .Select(part => ReflectionModelServices.GetPartType(part).Value);
        //}

        private static bool IsPartOfType(ComposablePartDefinition part, string exportTypeIdentity)
        {
            return (part.ExportDefinitions.Any(
                def => def.Metadata.ContainsKey("ExportTypeIdentity") &&
                       def.Metadata["ExportTypeIdentity"].Equals(exportTypeIdentity)));
        }
    }
}
