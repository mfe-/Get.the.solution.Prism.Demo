using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Get.the.solution.Common;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace Get.the.solution.Prism.Modul.Other
{
    /// <summary>
    /// Defines the module for this assembly.
    /// </summary>
    /// <remarks>
    /// Makes the module discoverable for a module catalog by defining a class which implements IModule
    /// </remarks>
    [ModuleExport(typeof(AnOtherModul))]
    public class AnOtherModul : IModule
    {
        protected IRegionManager _regionManager;

        [ImportingConstructor]
        public AnOtherModul(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Occourding to the <see cref="https://msdn.microsoft.com/en-us/library/ff921140(v=pandp.40)#sec12">msdn guid lines</see>
        /// it is not recommended to use the ServiceLocator to get the CompositeContainer</remarks>
        public void Initialize()
        {
            //register our view manual
            _regionManager.RegisterViewWithRegion(RegionNames.ShellContent, typeof(UserControlOther));           
        }
    }
}
