using DoorKeeperRoom.Models;
using DoorKeeperRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoorKeeperRoom.Commands
{
	class UpdateWorkerRecordCommand : ICommand
	{
		private DoorKeeperRoom_DBConnection dataEntities = new DoorKeeperRoom_DBConnection();
		WorkersViewModel viewModel;

		public UpdateWorkerRecordCommand(WorkersViewModel viewModel)
		{
			this.viewModel = viewModel;
			dataEntities.workers.Load();
		}
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true; 
		}

		public void Execute(object parameter)
		{
			var worker = parameter as workers;
			dataEntities.workers.Local.Insert(worker.id_worker, worker);
			dataEntities.SaveChanges();
			
		}
	}
}
