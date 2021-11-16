using DoorKeeperRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoorKeeperRoom.Commands
{
	public class DeleteFromKeyHandingCommand : ICommand
	{
		HandingViewModel viewModel;
		public DeleteFromKeyHandingCommand(HandingViewModel viewModel)
		{
			this.viewModel = viewModel;
		}
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			throw new NotImplementedException();
		}

		public void Execute(object parameter)
		{
			viewModel.
		}
	}
}
