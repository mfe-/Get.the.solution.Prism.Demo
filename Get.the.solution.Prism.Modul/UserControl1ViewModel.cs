using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Get.the.solution.Prism.Modul
{
    [Export]
    public class UserControl1ViewModel : BindableBase
    {
        public UserControl1ViewModel()
        {
            Text = "UserControl1ViewModel";
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
