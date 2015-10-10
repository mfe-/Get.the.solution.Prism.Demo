using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace Get.the.solution.Common
{
    /// <summary>
    /// Dummy class for MenuItem
    /// </summary>
    [PartNotDiscoverable]
    [Export]
    public class MenuItem : IMenuItem
    {
        public MenuItem()
        {
            Items = new List<IMenuItem>();
        }
        public MenuItem(Action<object> executeMethod, Func<object,bool> canExecuteMethod)
            : this()
        {
            Command = new DelegateCommand<object>(executeMethod, canExecuteMethod);
        }

        protected DelegateCommand<object> Command;

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
            if (Command != null)
            {
                return Command.CanExecute(parameter);
            }
            else
            {
                return true;
            }
      
        }

        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {
            if(Command!=null)
            {
                Command.Execute(parameter);
            }
        }
    }
}
