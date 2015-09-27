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
        public MainWindowViewModel([ImportMany(typeof(IMenuItem))]IEnumerable<IMenuItem> menuitems, IRegionManager regionManager)
        {
            RegionManager = regionManager;
            Menu = new ObservableCollection<IMenuItem>();
            foreach(IMenuItem item in menuitems)
            {
                Menu.Add(item);
            }

            OpenFileCommand = new DelegateCommand<String>(OnOpenFileCommand);
        }

        public DelegateCommand<String> OpenFileCommand { get; set; }

        public void OnOpenFileCommand(String uri)
        {
            if (!String.IsNullOrWhiteSpace(uri))
            {
                RegionManager.RequestNavigate(RegionNames.ShellContent, new Uri(uri, UriKind.Relative));
            }
        }

        private ICollection<IMenuItem> _Menu;

        public ICollection<IMenuItem> Menu
        {
            get { return _Menu; }
            set { _Menu = value; }
        }

    }
}
