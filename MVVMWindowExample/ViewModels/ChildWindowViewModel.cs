using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMWindowExample.Core;
using MVVMWindowExample.Models;

namespace MVVMWindowExample.ViewModels
{
    public class ChildWindowViewModel : ViewModelBase
    {
        public int Index
        {
            get { return _Index; }
            set { SetProperty(ref _Index, value); }
        }
        private int _Index;

        public string Property1
        {
            get { return _Property1; }
            set { SetProperty(ref _Property1, value); }
        }
        private string _Property1;

        public SomeData Data { get; private set; }

        public RelayCommand SaveDataCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public ChildWindowViewModel()
        {
            SaveDataCommand = new RelayCommand(SaveData);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SaveData()
        {
            Data = new SomeData();
            Data.Index = Index;
            Data.Property1 = Property1;
            Messenger.Default.Send("ChildWindow");
        }

        private void Cancel()
        {
            Messenger.Default.Send("ChildWindow");
        }
    }
}
