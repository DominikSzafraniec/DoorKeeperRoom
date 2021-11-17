using System.Collections.Generic;
using DoorKeeperRoom.ViewModels;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

		private void key_handingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			/*
			var selectedRecord = (Models.key_handing)key_handingListView.SelectedItem;
			idTextBoxEdit.Text = selectedRecord.id.ToString();
			id_keyTextBoxEdit.Text = selectedRecord.id_key.ToString();
			id_workerTextBoxEdit.Text = selectedRecord.id_worker.ToString();
			handing_dateDatePickerEdit.SelectedDate=selectedRecord.handing_date;
			return_dateDatePickerEdit.SelectedDate = selectedRecord.return_date;
			*/
		}
	}
}
