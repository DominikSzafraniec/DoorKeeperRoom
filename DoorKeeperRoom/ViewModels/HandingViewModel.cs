using DoorKeeperRoom.Models;
using DoorKeeperRoom.Commands;
using System.Data.Entity.Core.Objects;
using DoorKeeperRoom.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using System.ComponentModel;

namespace DoorKeeperRoom.ViewModels
{
	public class HandingViewModel : BaseViewModel
	{
		DoorKeeperRoom_DBConnection dataEntities { get; set; } = new DoorKeeperRoom_DBConnection();

		private DelegateCommand<key_handing> _deleteHKCommand;
		public DelegateCommand<key_handing> DeleteHKCommand => 
			_deleteHKCommand ?? (_deleteHKCommand = new DelegateCommand<key_handing>(ExecuteDeleteHKCommand));
		
		void ExecuteDeleteHKCommand(key_handing parameter)
		{
			dataEntities.key_handing.Remove(parameter);	
			dataEntities.SaveChanges();
		}
		public HandingViewModel()
		{

		}


		//public void HandingViewGetData()
		//{
		//	var query =
		//	from keyhanding in dataEntities.key_handing
		//	select new { keyhanding.id, keyhanding.id_worker, keyhanding.id_key, keyhanding.handing_date, keyhanding.return_date };
		//	handingView.key_handingListView.ItemsSource = query.ToList();

		//}
	}
}
