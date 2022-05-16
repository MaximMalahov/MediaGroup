using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Models.Entity;
using Малахов.Pages.EditPages;

namespace Малахов.Pages
{
	/// <summary>
	/// Логика взаимодействия для ClientPage.xaml
	/// </summary>
	public partial class ClientPage : Page
	{

		public ClientPage()
		{
			InitializeComponent();
		}

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			ManagerPage.MainFrame.Navigate(new ClientEditPage());
		}
		private void BtnDelete_Click(object sender, RoutedEventArgs e)
		{
			var clientsForRemoving = DGridClients.SelectedItems.Cast<Client>().ToList();
			if (MessageBox.Show($"Вы точно хотите удалить следующие { clientsForRemoving.Count()} элементов ? ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				try
				{
					MediaGrEntities.GetContext().Clients.RemoveRange(clientsForRemoving);
					MediaGrEntities.GetContext().SaveChanges();
					MessageBox.Show("Данные удалены!");
					DGridClients.ItemsSource = MediaGrEntities.GetContext().Clients.ToList();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message.ToString() + ex.StackTrace.ToString());
				}
			}
		}

		private void BtnEdit_Click(object sender, RoutedEventArgs e)
		{
			ManagerPage.MainFrame.Navigate(new ClientEditPage((sender as Button).DataContext as Client));
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			DGridClients.ItemsSource = MediaGrEntities.GetContext().Clients.ToList();
			BtnAdd.Visibility = Data.IsManager() ? Visibility.Visible : Visibility.Collapsed;
			BtnDelete.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;


		}
	}
}
