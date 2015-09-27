using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Get.the.solution.Prism.Modul.Other
{
    [Export]
    public class UserControlOtherViewModel : BindableBase
    {
        public UserControlOtherViewModel()
        {
            Text = "UserControlOtherViewModel";
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
