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
        public WeatherWindowViewModel ViewModel;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            var query = parameter as string;
            if (string.IsNullOrWhiteSpace(query))
            {
                return false;
            }

            return true;
        }

        public void Execute(object? parameter)
        {
            ViewModel.MakeQuery();
        }
    }
}
