using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Get.the.solution.Common
{
    public class MenuItem : IMenuItem
    {
        public MenuItem()
        {
            Items = new List<IMenuItem>();
        }
        protected string _Header;
        public virtual string Header
        {
            get
            {
                return _Header;
            }
            set
            {
                _Header = value;
            }
        }
        protected IEnumerable<IMenuItem> _Items;
        public virtual IEnumerable<IMenuItem> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
            }
        }

        public virtual object Icon
        {
            get { return null; }
        }

        public virtual bool IsCheckable
        {
            get { return false; }
        }

        public virtual bool IsChecked
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool Visible
        {
            get { return true; }
        }

        public virtual bool IsSeparator
        {
            get { return false; }
        }

        private string _ToolTip;
        public virtual string ToolTip
        {
            get { return String.IsNullOrWhiteSpace(_ToolTip) ? Header : _ToolTip; }
            set
            {
                _ToolTip = value;
            }
        }

        private object _Tag;
        public virtual object Tag
        {
            get { return _Tag; }
            set
            {
                _Tag = value;
            }
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {

        }
    }
}
