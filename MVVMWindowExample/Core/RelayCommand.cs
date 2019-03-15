using System;
using System.Windows.Input;

namespace MVVMWindowExample.Core
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Action methodToExecute;
        private Action<object> methodToExecuteWithParameter;
        private Func<bool> canExecuteEvaluator;

        public object ParameterValue { get; set; }

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action<object> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecuteWithParameter = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public RelayCommand(Action methodToExecute) : this(methodToExecute, null) { }
        public RelayCommand(Action<object> methodToExecute) : this(methodToExecute, null) { }


        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
                return true;
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }

        public void Execute(object parameter)
        {
            this.ParameterValue = parameter;
            if(parameter != null)
                this.methodToExecuteWithParameter.Invoke(parameter);
            else
                this.methodToExecute.Invoke();
        }
    }
}
