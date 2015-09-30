using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mef.Modularity;
using Prism.Modularity;

namespace Get.the.solution.Prism.Modul
{
    /// <summary>
    /// Defines the module for this assembly.
    /// </summary>
    /// <remarks>
    /// Makes the module discoverable for a module catalog by defining a class which implements IModule
    /// </remarks>
    [ModuleExport(typeof(ModulDefinition))]
    public class ModulDefinition : IModule
    {
        public void Initialize()
        {
            //Because all views and viewmodels will be discovered automatically we have nothing to do here
        }
    }
}
