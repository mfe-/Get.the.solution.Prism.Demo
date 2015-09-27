using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Get.the.solution.Common;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace Get.the.solution.Prism.Modul.Other
{
    [ModuleExport(typeof(AnOtherModul))]
    public class AnOtherModul : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Occourding to the <see cref="https://msdn.microsoft.com/en-us/library/ff921140(v=pandp.40)#sec12">msdn guid lines</see>
        /// it is not recommended to use the ServiceLocator to get the CompositeContainer</remarks>
        public void Initialize()
        {
            //maybe we need that later
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            //we can get the compositionContainer because we exported it in the bootstrapper
            CompositionContainer container = ServiceLocator.Current.GetInstance<CompositionContainer>();

            RegistrationBuilder registrationBuilder = new RegistrationBuilder();
            registrationBuilder.ForTypesDerivedFrom<UserControlOther>().Export<UserControlOther>();
            //inject the mainwindowviewmodel
            registrationBuilder.ForType<UserControlOther>().ImportProperty<UserControlOtherViewModel>(p => p.DataContext);
            
            
            ComposablePartDefinition part = container.Catalog.ElementAt(7);

            part.Exports<UserControlOther>();
            part.Exports<UserControlOtherViewModel>();


            regionManager.RegisterViewWithRegion(RegionNames.ShellContent, typeof(UserControlOther));



            
        }
    }
}
