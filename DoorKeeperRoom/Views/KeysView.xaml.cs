using DoorKeeperRoom.Models;
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

	}
}
