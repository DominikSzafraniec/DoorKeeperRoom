using System.Collections.Generic;
using DoorKeeperRoom.ViewModels;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using System.Linq;

namespace DoorKeeperRoom.Views
{
	/// <summary>
	/// Logika interakcji dla klasy HandingView.xaml
	/// </summary>
	public partial class KeyDisposeView : UserControl
	{
		private Models.DoorKeeperRoom_DBConnection dataEntities = new Models.DoorKeeperRoom_DBConnection();
		public KeyDisposeView()
		{
			InitializeComponent();
			DataContext = this;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
		}

		private DelegateCommand<string> _disposeKey;
		public DelegateCommand<string> DisposeKey=>
		_disposeKey ?? (_disposeKey = new DelegateCommand<string>(DisposeKeyCommand));

		void DisposeKeyCommand(string s)
		{
			var mess = "";
			var reqEmpty = false;
			if (workerNameTextBox.Text == "")
			{
				reqEmpty = true;
				mess += "\nImię jest wymagane";
			}
			if (workerLastnameTextBox.Text == "")
			{
				reqEmpty = true;
				mess += "\nNazwisko jest wymagane";
			}
			if (keyIdTextBox.Text == "")
			{
				reqEmpty = true;
				mess += "\nNumer klucza jest wymagany";
			}
			if(!reqEmpty)
			{
				var wk = (from workers in dataEntities.workers.Local
							where workers.name.Equals(workerNameTextBox.Text) && workers.lastname.Equals(workerLastnameTextBox.Text)
							select workers);
				if (wk != null)
				{
					if (wk.Count()==1)
					{

						workerIdTextBox.Text = wk.First().id_worker.ToString();
					} else
					{
						reqEmpty = true;
						mess = "Jest kilku pracowników o takim imieniu i nazwisku. Podaj dodatkowo numer pracownika."+ workerNameTextBox.Text + wk.Count().ToString();
						workerIdTextBox.Visibility = Visibility.Visible;
						workerIdLabel.Visibility = Visibility.Visible;
					}
				}

			}
			if (reqEmpty)
			{
				MessageBox.Show(mess, "Wydawanie klucza", MessageBoxButton.OK, MessageBoxImage.None);
			}
			else
			{


			}

			//	if (!reqEmpty)
			//{
			//	keys newKeyAdd = new keys
			//	{
			//		room_name = room_nameTextBoxAdd.Text,
			//	};
			//	object p = dataEntities.keys.Add(newKeyAdd);
			//	dataEntities.SaveChanges();
			//	kViewSource.Source = dataEntities.keys.Local;
			//	kViewSource.View.Refresh();
			//	ClearNewKeyCommand("s");
			//}
			//else
			//{
				
			//}
		}

	}
}
