using System.Collections.Generic;
using DoorKeeperRoom.ViewModels;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using System.Linq;
using DoorKeeperRoom.Models;
using System;

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
				var wkAll = dataEntities.workers.Where(w => w.name == workerNameTextBox.Text && w.lastname == workerLastnameTextBox.Text);
				if (wkAll.Any())
				{
					if (wkAll.Count()==1)
					{

						workerIdTextBox.Text = wkAll.First().id_worker.ToString();
					} 
					else
					{
						var dialog = new GetWorkerId();
						
						if (dialog.ShowDialog() == true)
						{
							if (dialog.ResponseText.Length == 4)
							{
								try
								{
									var id = Int32.Parse(dialog.ResponseText);
									var wk = wkAll.Where(w => w.id_worker == id);
									if (wk.Any())
									{
										workerIdTextBox.Text = wk.First().id_worker.ToString();
									}
									else
									{
										reqEmpty = true;
										mess = "Nie ma pracowanika o imieniu " + workerNameTextBox.Text + " nazwisku " + workerLastnameTextBox + " i numerze pracownika " + dialog.ResponseText + "!";
									}
								}
								catch (Exception e)
								{
									reqEmpty = true;
									mess = "Wprowadź numer pracownika składający się wyłącznie z cyfr!";
								}
							}
							else
							{
								reqEmpty = true;
								mess = "Nie wpisano poprawnego numeru pracownika!";
							}							
						}
					}
				}
				else
				{
					reqEmpty = true;
					mess = "Nie ma pracowanika o imieniu "+workerNameTextBox.Text+" i nazwisku "+ workerLastnameTextBox +"!";
				}

			}
			if (!reqEmpty)
			{
				if (keyIdTextBox.Text.Length == 4)
				{
					try
					{
						var idk = Int32.Parse(keyIdTextBox.Text);
						var kf = dataEntities.keys.Find(idk);
						if (kf != null)
						{
							var keyAv = dataEntities.key_handing.Where(k => k.id_key == idk && k.return_date == null);
							if (keyAv.Any())
							{
								reqEmpty = true;
								mess = "Klucz jest już pobrany przez pracownika nr:" + keyAv.First().id_worker;
							} else
							{
								dataEntities.key_handing.Add(new key_handing() { id_key = idk, id_worker = int.Parse(workerIdTextBox.Text), handing_date = DateTime.Now });
								dataEntities.SaveChanges();
								
							}
						}
					}
					catch (Exception e)
					{
						reqEmpty = true;
						mess = "Wprowadź numer klucza składający sie wyłącznie z cyfr!";
					}
				}
				else
				{
					reqEmpty = true;
					mess = "Nie wpisano poprawnego numeru klucza!";
				}
			}
			else
			{


			}
			if(reqEmpty)
				MessageBox.Show(mess,"Wydawanie klucza",MessageBoxButton.OK, MessageBoxImage.Warning);
			else
				MessageBox.Show("Wydano klucz pracownikowi", "Wydawanie klucza", MessageBoxButton.OK, MessageBoxImage.Warning);
		}

		private DelegateCommand<string> _receiveKey;
		public DelegateCommand<string> ReceiveKey =>
		_receiveKey ?? (_receiveKey = new DelegateCommand<string>(ReceiveKeyCommand));

		void ReceiveKeyCommand(string s)
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
			if (!reqEmpty)
			{
				var wkAll = dataEntities.workers.Where(w => w.name == workerNameTextBox.Text && w.lastname == workerLastnameTextBox.Text);
				if (wkAll.Any())
				{
					if (wkAll.Count() == 1)
					{

						workerIdTextBox.Text = wkAll.First().id_worker.ToString();
					}
					else
					{
						var dialog = new GetWorkerId();

						if (dialog.ShowDialog() == true)
						{
							if (dialog.ResponseText.Length == 4)
							{
								try
								{
									var id = Int32.Parse(dialog.ResponseText);
									var wk = wkAll.Where(w => w.id_worker == id);
									if (wk.Any())
									{
										workerIdTextBox.Text = wk.First().id_worker.ToString();
									}
									else
									{
										reqEmpty = true;
										mess = "Nie ma pracowanika o imieniu " + workerNameTextBox.Text + " nazwisku " + workerLastnameTextBox + " i numerze pracownika " + dialog.ResponseText + "!";
									}
								}
								catch (Exception e)
								{
									reqEmpty = true;
									mess = "Wprowadź numer pracownika składający się wyłącznie z cyfr!";
								}
							}
							else
							{
								reqEmpty = true;
								mess = "Nie wpisano poprawnego numeru pracownika!";
							}
						}
					}
				}
				else
				{
					reqEmpty = true;
					mess = "Nie ma pracowanika o imieniu " + workerNameTextBox.Text + " i nazwisku " + workerLastnameTextBox + "!";
				}

			}
			if (!reqEmpty)
			{
				if (keyIdTextBox.Text.Length == 4)
				{
					try
					{
						var idk = Int32.Parse(keyIdTextBox.Text);
						var kf = dataEntities.keys.Find(idk);
						if (kf != null)
						{
							var keyAv = dataEntities.key_handing.Where(k => k.id_key == idk && k.return_date == null);
							if (keyAv.Any())
							{
								var idkk = int.Parse(workerIdTextBox.Text);
								var wkKey = keyAv.Where(k => k.id_worker == idkk);
								if (wkKey.Any())
								{
									wkKey.First().return_date = DateTime.Now;
									dataEntities.SaveChanges();
								}
								else
								{
									mess = "Klucz jest już pobrany przez pracownika nr:" + wkKey.First().id_worker + "\n tylko on może zdać klucz";
								}
							}
							else
							{
								reqEmpty = true;
								mess = "Klucz nie został pobrany";

							}
						}
					}
					catch (Exception e)
					{
						//Console.WriteLine(e.Message);
						reqEmpty = true;
						mess = "Wprowadź numer klucza składający sie wyłącznie z cyfr!";
					}
				}
				else
				{
					reqEmpty = true;
					mess = "Nie wpisano poprawnego numeru klucza!";
				}
			}
			else
			{


			}
			if (reqEmpty)
				MessageBox.Show(mess, "Odbieranie klucza", MessageBoxButton.OK, MessageBoxImage.Warning);
			else
				MessageBox.Show("Odebrano klucz od pracownika", "Odbieranie klucza", MessageBoxButton.OK, MessageBoxImage.Warning);

		}

		private DelegateCommand<string> _clearForm;
		public DelegateCommand<string> ClearForm =>
		_clearForm ?? (_clearForm = new DelegateCommand<string>(ClearFormCommand));

		void ClearFormCommand(string s)
		{
			workerIdTextBox.Text = "";
			workerNameTextBox.Text = "";
			workerLastnameTextBox.Text = "";
			keyIdTextBox.Text = "";
		}

	}
}
