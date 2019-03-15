using MVVMWindowExample.ViewModels;
using MVVMWindowExample.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMWindowExample.Core
{
    public class WindowFactory
    {
        private readonly IWindowFactory _WindowFactory;
        public bool IsOpen { get; set; }
        public RelayCommand OpenNewWindow { get; set; }
        public WindowFactory(IWindowFactory windowFactory)
        {
            _WindowFactory = windowFactory;
            /**
             * Would need to assign value to m_openNewWindow here, and associate the DoOpenWindow method
             * to the execution of the command.
             * */
            OpenNewWindow = new RelayCommand(DoOpenNewWindow);
            Messenger.Default.Register<string>(_WindowFactory, DoCloseWindow);
            IsOpen = false;
        }

        public void DoOpenNewWindow()
        {
            _WindowFactory.CreateNewWindow();
            IsOpen = true;
        }

        public void DoCloseWindow(string parameter)
        {
            IsOpen = false;
        }


    }

    public interface IWindowFactory
    {
        bool CreateNewWindow();
        bool CloseWindow();
    }

    // Various windows to create
    public class ChildWindowFactory : IWindowFactory
    {
        public bool CreateNewWindow()
        {
            ChildWindow window = new ChildWindow
            {
                DataContext = new ChildWindowViewModel()
            };
            window.Show();
            window.Closing += Window_Closing;
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseWindow();
        }
        public bool CloseWindow()
        {
            Messenger.Default.Send("true");
            return true;
        }
    }
}
