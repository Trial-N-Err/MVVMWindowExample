using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMWindowExample.Core;

namespace MVVMWindowExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand OpenWindowCommand { get; private set; }
        ChildWindowFactory cwf;
        WindowFactory wf;
        public MainWindowViewModel()
        {
            OpenWindowCommand = new RelayCommand(OpenWindow);
            cwf = new ChildWindowFactory();
            wf = new WindowFactory(cwf);
        }

        private void OpenWindow()
        {
            if (wf.IsOpen)
                return;
            wf.DoOpenNewWindow();
        }
    }
}
