using DoorKeeperRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoorKeeperRoom.Commands
{
	public class UpdateViewCommand : ICommand
	{
		private MainViewModel viewModel;

		public UpdateViewCommand(MainViewModel viewModel)
		{
			this.viewModel = viewModel;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{

			if (parameter.ToString()=="Handing")
			{
				viewModel.SelectedViewModel = new HandingViewModel();
			}
			else if (parameter.ToString() == "Workers")
			{
				viewModel.SelectedViewModel = new WorkersViewModel();
			}
			else if (parameter.ToString()=="Keys")
			{
				viewModel.SelectedViewModel = new KeysViewModel();
			}
			else if (parameter.ToString() == "Dispose")
			{
				viewModel.SelectedViewModel = new KeyDisposeViewModel();
			}
		}
	}
}
