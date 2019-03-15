using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MVVMWindowExample.Core;
using MVVMWindowExample.Models;

namespace MVVMWindowExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand OpenWindowCommand { get; private set; }
        ChildWindowFactory cwf;
        WindowFactory wf;

        public ObservableCollection<SomeData> DataList { get; private set; }

        public MainWindowViewModel()
        {
            OpenWindowCommand = new RelayCommand(OpenWindow);
            cwf = new ChildWindowFactory();
            wf = new WindowFactory(cwf);

            DataList = new ObservableCollection<SomeData>();
        }

        private void OpenWindow()
        {
            if (wf.IsOpen)
                return;
            wf.DoOpenNewWindow();
            Task t = Task.Run(() =>
            {
                while (wf.IsOpen)
                {
                    Thread.Sleep(500);
                }
            }).ContinueWith(a =>
            {
                if (cwf.vm.Data.Property1 != null)
                {
                    App.Current.Dispatcher.Invoke(()=>DataList.Add(cwf.vm.Data));

                }
            });
        }
    }
}
