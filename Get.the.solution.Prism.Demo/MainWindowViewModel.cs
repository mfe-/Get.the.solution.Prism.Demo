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
    /// <summary>
    /// Provides the main functionality for switching between modules, by defining the Menu ItemsSource
    /// </summary>
    [Export]
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager RegionManager;
        private IEnumerable<IMenuItem> _ImportedMenu;

        /// <summary>
        /// Get or sets the ImportedMenu.
        /// </summary>
        /// <remarks>
        /// The ImportedMenu can be set during the runtime more then once. When setting the value of the ImportedMenu the ItemsSource for view menu is created.
        /// </remarks>
        [ImportMany(typeof(IMenuItem), AllowRecomposition = true)]
        public IEnumerable<IMenuItem> ImportedMenu
        {
            get { return null; }
            set
            {
                _ImportedMenu = value;
                Menu = new ObservableCollection<IMenuItem>();
                foreach (IMenuItem item in _ImportedMenu)
                {
                    Menu.Add(item);
                }
                OnPropertyChanged(() => Menu);
            }
        }

        [ImportingConstructor]
        public MainWindowViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;
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
        /// <summary>
        /// Get or sets the menu items to display
        /// </summary>
        public ICollection<IMenuItem> Menu
        {
            get { return _Menu; }
            set { _Menu = value; }
        }

    }
}
