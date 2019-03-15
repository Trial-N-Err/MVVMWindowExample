using MVVMWindowExample.Models;
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
            if(parameter.Contains("ChildWindow"))
            {
                _WindowFactory.CloseWindow();
                
            }
            IsOpen = false;
        }


    }

    public interface IWindowFactory
    {
        void CreateNewWindow();
        void CloseWindow();
    }

    // Various windows to create
    public class ChildWindowFactory : IWindowFactory
    {
        public ChildWindowViewModel vm { get; private set; }
        private ChildWindow window;
        public void CreateNewWindow()
        {
            vm = new ChildWindowViewModel();
            window = new ChildWindow();
            window.DataContext = vm;
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window.Show();
            window.Closing += Window_Closing;
        }


        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(sender != null)
            {
                Messenger.Default.Send(" ");
            }
            
        }
        public void CloseWindow()
        {
            window.Closing -= Window_Closing;
            window.Close();
        }
    }
}
