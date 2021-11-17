using DoorKeeperRoom.ViewModels;
using DoorKeeperRoom.Models;
using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using System.Linq;
using DoorKeeperRoom.Commands;

namespace DoorKeeperRoom.Views
{
	/// <summary>
	/// Logika interakcji dla klasy WorkersView.xaml
	/// </summary>
	public partial class WorkersView : UserControl
	{
		private DoorKeeperRoom_DBConnection dataEntities = new DoorKeeperRoom_DBConnection();
		CollectionViewSource wrViewSource;
		public WorkersView()
		{
			InitializeComponent();
			wrViewSource = (CollectionViewSource)FindResource("workersViewSource");
			DataContext = this;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			dataEntities.workers.Load();
			wrViewSource.Source = dataEntities.workers.Local;
		}
		
		private DelegateCommand<string> _updateWorkerRecord;
		public DelegateCommand<string> UpdateWorkerRecord =>
			_updateWorkerRecord ?? (_updateWorkerRecord = new DelegateCommand<string>(UpdateWorkerCommand));

		void UpdateWorkerCommand(string s)
		{
			dataEntities.SaveChanges();
		}
		

		private DelegateCommand<string> _addWorkerRecord;
		public DelegateCommand<string> AddWorkerRecord =>
		_addWorkerRecord ?? (_addWorkerRecord = new DelegateCommand<string>(AddNewWorkerCommand));

		void AddNewWorkerCommand(string s)
		{
			var reqEmpty = false;
			if (nameTextBoxAdd.Text=="")
			{
				reqEmpty = true;
			}
			else if (lastnameTextBoxAdd.Text == "")
				{
					reqEmpty = true;
			}
				else if (positionTextBoxAdd.Text == "")
					{
						reqEmpty = true;
			}

			if (!reqEmpty)
			{
				workers newWorkerAdd = new workers
				{
					name = nameTextBoxAdd.Text,
					lastname = lastnameTextBoxAdd.Text,
					position = positionTextBoxAdd.Text,
					department = departmentTextBoxAdd.Text
				};
				object p = dataEntities.workers.Add(newWorkerAdd);
				dataEntities.SaveChanges();
				wrViewSource.Source = dataEntities.workers.Local;
				wrViewSource.View.Refresh();
				ClearNewWorkerCommand("s");
			}
			else
			{
				MessageBox.Show("Imię, Nazwisko oraz stanowisko są wymagane", "Dodawanie pracownika", MessageBoxButton.OK, MessageBoxImage.None);
			}
		}
		private DelegateCommand<string> _clearWorkerRecord;
		public DelegateCommand<string> ClearWorkerRecord =>
		_clearWorkerRecord ?? (_clearWorkerRecord = new DelegateCommand<string>(ClearNewWorkerCommand)); 

		void ClearNewWorkerCommand(string s)
		{
			nameTextBoxAdd.Text = "";
			lastnameTextBoxAdd.Text = "";
			positionTextBoxAdd.Text = "";
			departmentTextBoxAdd.Text = "";
		}
		private DelegateCommand<string> _deleteWorkerRecord;
		public DelegateCommand<string> DeleteWorkerRecord =>
		_deleteWorkerRecord ?? (_deleteWorkerRecord = new DelegateCommand<string>(DeleteWorkerCommand));

		void DeleteWorkerCommand(string s)
		{
			var selected = wrViewSource.View.CurrentItem as workers;
			var keyH = (from key_handing in dataEntities.key_handing.Local
					  where key_handing.id_worker == selected.id_worker
					  select key_handing);
			if (MessageBox.Show("Usuwając pracownika usuwasz całą historię pobieranych przez niego kluczy. Czy na pewno chcesz usunąć pracownika?","Usuwanie pracownika",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes){
			if(keyH != null)
			{
				foreach(var kh in keyH)
				{
					dataEntities.key_handing.Remove(kh);
				}
			}

			dataEntities.workers.Remove(selected);
			dataEntities.SaveChanges();
			wrViewSource.View.Refresh();
			}
		}


	}
}