using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;

namespace Get.the.solution.Prism.Modul
{
    /// <summary>
    /// Provides logical functionality for a view.
    /// </summary>
    [Export]
    public class UserControl2ViewModel : BindableBase
    {
        public UserControl2ViewModel()
        {
            Text = "UserControl2ViewModel";
        }
        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
    }
}
