using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Entity;
using Малахов.Pages;

namespace Малахов
{
	/// <summary>
	/// Логика взаимодействия для ServicePage.xaml
	/// </summary>
	public partial class ServicePage : Page
	{
		public ServicePage()
		{
			InitializeComponent();
		}

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			ManagerPage.MainFrame.Navigate(new ServiceEditPage());
		}

		private void BtnDelete_Click(object sender, RoutedEventArgs e)
		{
			var serviceForRemoving = DGridService.SelectedItems.Cast<Service>().ToList();
			if (MessageBox.Show($"Вы точно хотите удалить следующие { serviceForRemoving.Count()} элементов ? ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				try
				{
					MediaGrEntities.GetContext().Services.RemoveRange(serviceForRemoving);
					MediaGrEntities.GetContext().SaveChanges();
					MessageBox.Show("Данные удалены!");
					DGridService.ItemsSource = MediaGrEntities.GetContext().Services.ToList();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message.ToString() + ex.StackTrace.ToString());
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ManagerPage.MainFrame.Navigate(new ServiceEditPage((sender as Button).DataContext as Service));
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			DGridService.ItemsSource = MediaGrEntities.GetContext().Services.ToList();

			BtnAdd.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
			BtnDelete.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
			CellEdit.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
		}

		private void Example_Click(object sender, RoutedEventArgs e) 
		{
			ManagerPage.MainFrame.Navigate(new ExamplePage((sender as Button).DataContext as Service));
		}

	}
}
