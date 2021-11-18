using System.Collections.Generic;
using DoorKeeperRoom.ViewModels;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using DoorKeeperRoom.Models;
using System.Linq;

namespace DoorKeeperRoom.Views
{
	/// <summary>
	/// Logika interakcji dla klasy HandingView.xaml
	/// </summary>
	public partial class HandingView : UserControl
	{
		private Models.DoorKeeperRoom_DBConnection dataEntities = new Models.DoorKeeperRoom_DBConnection();
		CollectionViewSource khViewSource;

		public HandingView()
		{
			InitializeComponent();
			khViewSource = (CollectionViewSource)FindResource("key_handingViewSource");
			DataContext = this;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			dataEntities.key_handing.Load();
			khViewSource.Source = dataEntities.key_handing.Local;

		}

		private DelegateCommand<string> _updateKeyHandingRecord;
		public DelegateCommand<string> UpdateKeyHandingRecord =>
			_updateKeyHandingRecord ?? (_updateKeyHandingRecord = new DelegateCommand<string>(UpdateKeyHandingCommand));

		void UpdateKeyHandingCommand(string s)
		{
			dataEntities.SaveChanges();
		}

		private DelegateCommand<string> _deleteKeyHandingRecord;
		public DelegateCommand<string> DeleteKeyHandingRecord =>
		_deleteKeyHandingRecord ?? (_deleteKeyHandingRecord = new DelegateCommand<string>(DeleteKeyHandingCommand));

		void DeleteKeyHandingCommand(string s)
		{
			var selected = key_handingDataGrid.SelectedItem as key_handing;
			if (MessageBox.Show("Usuwając pracownika usuwasz całą historię pobieranych przez niego kluczy. Czy na pewno chcesz usunąć pracownika?", "Usuwanie pracownika", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
			{
				dataEntities.key_handing.Remove(selected);
				dataEntities.SaveChanges();
				khViewSource.View.Refresh();
			}
		}

		private void key_handingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
			var selectedRecord = (Models.key_handing)key_handingDataGrid.SelectedItem;
			var worker = dataEntities.workers.Find(selectedRecord.id_worker);
			var key = dataEntities.keys.Find(selectedRecord.id_key);
		}
		private CollectionView getJoinedTable()
		{
			var sttt = dataEntities.key_handing
				.Join(
					dataEntities.workers,
					hk => hk.id_worker,
					worker => worker.id_worker,
					(hk, worker) => new
					{
						id = hk.id,
						id_key = hk.id_key,
						id_worker =hk.id_worker,
						name = worker.name,
						lastname = worker.lastname,
						handingdate=hk.handing_date,
						returndate=hk.return_date
					}
					)
				.Join(
				dataEntities.keys,
				hk=>hk.id_key,
				key=>key.id_key,
				(hk, key)=>new
				{
					id = hk.id,
					id_key = hk.id_key,
					roomname=key.room_name,
					id_worker = hk.id_worker,
					name = hk.name,
					lastname = hk.lastname,
					handingdate = hk.handingdate,
					returndate = hk.returndate
				}
				);
			
			return new CollectionView(sttt.ToList());
		}

		private void joinedData_Loaded(object sender, RoutedEventArgs e)
		{
			var grid = sender as DataGrid;
			grid.ItemsSource = getJoinedTable();
		}
	}
}
