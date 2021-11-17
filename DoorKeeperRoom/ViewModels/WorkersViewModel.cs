﻿using DoorKeeperRoom.Commands;
using DoorKeeperRoom.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoorKeeperRoom.ViewModels
{
	public class WorkersViewModel : BaseViewModel
	{
		private BaseViewModel _selectedViewModel;
		public BaseViewModel SelectedViewModel
		{
			get { return _selectedViewModel; }
			set
			{
				_selectedViewModel = value;
				OnPropertyChanged(nameof(SelectedViewModel));
			}
		}

		public ICommand UpdateWorkerRecordCommand { get; set; }


		public WorkersViewModel()
		{
			UpdateWorkerRecordCommand = new UpdateWorkerRecordCommand(this);
		}

		//private DelegateCommand<workers> _updateWorkerCommand;
		//public DelegateCommand<workers> UpdateWorkerCommand =>
		//	_updateWorkerCommand ?? (_updateWorkerCommand = new DelegateCommand<workers>(ExecuteUpdateWorkerCommand));
		//void ExecuteUpdateWorkerCommand(workers workers)
		//{
		//	//dataEntities.SaveChanges();
		//}
	}
}
