using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Get.the.solution.Common;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace Get.the.solution.Prism.Modul.Other
{
    /// <summary>
    /// Defines a menu entry for the module
    /// </summary>
    [Export(typeof(IMenuItem))]
    public class Menu : Get.the.solution.Common.MenuItem
    {
        public override string Header
        {
            get { return "Get.the.solution.Prism.Modul.Other"; }
        }

        public override IEnumerable<IMenuItem> Items
        {
            get
            {
                return new List<Get.the.solution.Common.MenuItem>()
                    {
                        new Get.the.solution.Common.MenuItem((param)=>{ ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.ShellContent, new Uri(param.ToString(), UriKind.Relative));}, (param) => {return true;} ){ Header="UserControlOther", Tag="/UserControlOther" }
                    };
            }
        }
    }
}
