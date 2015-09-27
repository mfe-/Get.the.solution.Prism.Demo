using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Get.the.solution.Common
{
    //http://stackoverflow.com/questions/1067903/how-can-i-bind-an-observablecollection-of-viewmodels-to-a-menuitem
    public interface IMenuItem : ICommand
    {
        string Header { get; }
        IEnumerable<IMenuItem> Items { get; }
        object Icon { get; }
        bool IsCheckable { get; }
        bool IsChecked { get; set; }
        bool Visible { get; }
        bool IsSeparator { get; }
        string ToolTip { get; }
        object Tag { get; }
    }
}
