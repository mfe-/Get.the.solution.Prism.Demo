using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Get.the.solution.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Get.the.solution.Prism.Demo
{
    [Export]
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager RegionManager;

        [ImportingConstructor]
        public MainWindowViewModel([ImportMany(typeof(IMenu))]IEnumerable<IMenu> menuitems,IRegionManager regionManager)
        {
            RegionManager = regionManager;
            Menu = new ObservableCollection<IMenu>();
            //Menu.AddRange(menuitems);

            OpenFileCommand = new DelegateCommand(OnOpenFileCommand);
        }

        public DelegateCommand OpenFileCommand { get; set; }

        public void OnOpenFileCommand()
        {
            RegionManager.RequestNavigate(RegionNames.ShellContent, new Uri("/UserControlOther", UriKind.Relative));
        }

        private ICollection<IMenu> _Menu;

        public ICollection<IMenu> Menu
        {
            get { return _Menu; }
            set { _Menu = value; }
        }

    }
}
