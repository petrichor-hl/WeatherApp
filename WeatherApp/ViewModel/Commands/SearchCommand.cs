using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM VM { get; set; }

        public SearchCommand(WeatherVM vM)
        {
            VM = vM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            string inputQuery = parameter as string;
            return !string.IsNullOrEmpty(inputQuery);
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
