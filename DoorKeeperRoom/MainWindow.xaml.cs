using DoorKeeperRoom.ViewModels;
using System.Windows;


namespace DoorKeeperRoom
{
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
