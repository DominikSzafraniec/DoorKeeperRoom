using System.Collections.Generic;
using DoorKeeperRoom.ViewModels;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using DoorKeeperRoom.Models;

namespace DoorKeeperRoom.Views
{
	/// <summary>
	/// Logika interakcji dla klasy HandingView.xaml
	/// </summary>
	public partial class HandingView : UserControl
	{
		private Models.DoorKeeperRoom_DBConnection dataEntities = new Models.DoorKeeperRoom_DBConnection();
		CollectionViewSource khViewSource;
		CollectionViewSource khAll;
		public HandingView()
		{
			InitializeComponent();
			khViewSource = (CollectionViewSource)FindResource("key_handingViewSource");
			khAll = (CollectionViewSource)FindResource("key_handingAll");
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
		private void getJoiningTable()
		{
			//var query = from hk in dataEntities.key_handing
			//			join w in dataEntities.workers on hk.id_worker equals w.id_worker
		}
	}
}
