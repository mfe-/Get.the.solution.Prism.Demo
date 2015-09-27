using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;

namespace Get.the.solution.Prism.Modul
{
    [Export]
    public class UserControl2ViewModel
    {
        [ImportingConstructor]
        public UserControl2ViewModel(IRegionManager regionManager)
        {

        }
    }
}
