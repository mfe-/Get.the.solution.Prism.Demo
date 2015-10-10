using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Get.the.solution.Common;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace Get.the.solution.Prism.Modul
{
    /// <summary>
    /// Defines a menu entry for the module
    /// </summary>
    [Export(typeof(IMenuItem))]
    public class Menu : Get.the.solution.Common.MenuItem
    {
        public override string Header
        {
            get { return "Get.the.solution.Prism.Modul"; }
        }
        public override IEnumerable<IMenuItem> Items
        {
            get
            {
                return new List<Get.the.solution.Common.MenuItem>()
                    {
                        new Get.the.solution.Common.MenuItem(this.NavigateUri, CanExecute) { Header = "UserControl1", Tag = "/UserControl1" },
                        new Get.the.solution.Common.MenuItem(this.NavigateUri, CanExecute){ Header="UserControl2", Tag="/UserControl2"},
                    };
            }
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public void NavigateUri(object parameter)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RequestNavigate(RegionNames.ShellContent, new Uri(parameter.ToString(), UriKind.Relative));
        }
    }
}
