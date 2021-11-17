using DoorKeeperRoom.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DoorKeeperRoom.Views
{
	/// <summary>
	/// Logika interakcji dla klasy KeysView.xaml
	/// </summary>
	public partial class KeysView : UserControl
	{
		private DoorKeeperRoom_DBConnection dataEntities = new DoorKeeperRoom_DBConnection();
		CollectionViewSource kViewSource;


		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			dataEntities.keys.Load();
			kViewSource.Source = dataEntities.keys.Local;
		}
		public KeysView()
		{
			InitializeComponent();
			kViewSource = (CollectionViewSource)FindResource("keysViewSource");
			DataContext = this;
		}

		private DelegateCommand<string> _updateKeyRecord;
		public DelegateCommand<string> UpdateKeyRecord =>
			_updateKeyRecord ?? (_updateKeyRecord = new DelegateCommand<string>(UpdateKeyCommand));

		void UpdateKeyCommand(string s)
		{
			dataEntities.SaveChanges();
		}


		private DelegateCommand<string> _addKeyRecord;
		public DelegateCommand<string> AddKeyRecord =>
		_addKeyRecord ?? (_addKeyRecord = new DelegateCommand<string>(AddNewKeyCommand));

		void AddNewKeyCommand(string s)
		{
			var reqEmpty = false;
			if (room_nameTextBoxAdd.Text == "")
			{
				reqEmpty = true;
			}
			if (!reqEmpty)
			{
				keys newKeyAdd = new keys
				{
					room_name = room_nameTextBoxAdd.Text,
				};
				object p = dataEntities.keys.Add(newKeyAdd);
				dataEntities.SaveChanges();
				kViewSource.Source = dataEntities.keys.Local;
				kViewSource.View.Refresh();
				ClearNewKeyCommand("s");
			}
			else
			{
				MessageBox.Show("Imię, Nazwisko oraz stanowisko są wymagane", "Dodawanie Pomieszczenia", MessageBoxButton.OK, MessageBoxImage.None);
			}
		}
		private DelegateCommand<string> _clearKeyRecord;
		public DelegateCommand<string> ClearKeyRecord =>
		_clearKeyRecord ?? (_clearKeyRecord = new DelegateCommand<string>(ClearNewKeyCommand));

		void ClearNewKeyCommand(string s)
		{
			room_nameTextBoxAdd.Text = "";
		}
		private DelegateCommand<string> _deleteKeyRecord;
		public DelegateCommand<string> DeleteKeyRecord =>
		_deleteKeyRecord ?? (_deleteKeyRecord = new DelegateCommand<string>(DeleteKeyCommand));

		void DeleteKeyCommand(string s)
		{
			var selected = kViewSource.View.CurrentItem as keys;
			var keyH = (from key_handing in dataEntities.key_handing.Local
						where key_handing.id_key == selected.id_key
						select key_handing);
			if (MessageBox.Show("Usuwając pomieszczenie usuwasz całą historię pobierania klucza do danego pomieszczenia. Czy na pewno chcesz usunąć Pomieszczenie?", "Usuwanie Pomieszczenia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
			{
				if (keyH != null)
				{
					foreach (var kh in keyH)
					{
						dataEntities.key_handing.Remove(kh);
					}
				}

				dataEntities.keys.Remove(selected);
				dataEntities.SaveChanges();
				kViewSource.View.Refresh();
			}
		}

	}
}
