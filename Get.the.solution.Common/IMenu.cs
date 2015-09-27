using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Get.the.solution.Common
{
    //http://stackoverflow.com/questions/1067903/how-can-i-bind-an-observablecollection-of-viewmodels-to-a-menuitem
    public interface IMenu
    {
        String Text { get;  }
    }
}
