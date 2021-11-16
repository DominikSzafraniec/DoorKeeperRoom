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

namespace DoorKeeperRoom.ViewModels
{
	public class HandingViewModel : BaseViewModel
	{
		DoorKeeperRoom_DBConnection dataEntities { get; set; } = new DoorKeeperRoom_DBConnection();
		HandingView handingView = new HandingView();

		public ICommand DeleteFromKeyHandingCommand { get; set; }

		public void HandingViewGetData()
		{
			var query =
			from keyhanding in dataEntities.key_handing
			select new { keyhanding.id, keyhanding.id_worker, keyhanding.id_key,keyhanding.handing_date,keyhanding.return_date };
			handingView.key_handingListView.ItemsSource = query.ToList();

		}
		public HandingViewModel()
		{
			DeleteFromKeyHandingCommand = new DeleteFromKeyHandingCommand(this);
		}
	}
}
