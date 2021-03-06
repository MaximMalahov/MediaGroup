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
	/// Логика взаимодействия для TypePages.xaml
	/// </summary>
	public partial class TypePages : Page
	{
		private bool _view;
		private bool _load;
		public TypePages()
		{
			InitializeComponent();
		}




		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			DGridMan.ItemsSource = MediaGrEntities.GetContext().Managers.ToList();
			LvItems.ItemsSource = MediaGrEntities.GetContext().Managers.ToList();
			_load = true;
		}

        private void BtnDelete_Click_1(object sender, RoutedEventArgs e)
        {
			var TypesForRemoving = DGridMan.SelectedItems.Cast<Manager>().ToList();
			if (MessageBox.Show($"Вы точно хотите удалить следующие { TypesForRemoving.Count()} элементов ? ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				try
				{
					MediaGrEntities.GetContext().Managers.RemoveRange(TypesForRemoving);
					MediaGrEntities.GetContext().SaveChanges();
					MessageBox.Show("Данные удалены!");
					DGridMan.ItemsSource = MediaGrEntities.GetContext().Managers.ToList();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message.ToString());
				}
			}
		}

        private void BtnAdd_Click_1(object sender, RoutedEventArgs e)
        {
			ManagerPage.MainFrame.Navigate(new TypesEditPages());
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			ManagerPage.MainFrame.Navigate(new TypesEditPages((sender as Button).DataContext as Manager));
		}

        private void RbView_Checked(object sender, RoutedEventArgs e)
        {
			if (!_load) return;
			if (_view == false)
			{
				_view = true;
				LvItems.Visibility = Visibility.Visible;
				DGridMan.Visibility = Visibility.Collapsed;
			}
			else
			{
				_view = false;
				LvItems.Visibility = Visibility.Collapsed;
				DGridMan.Visibility = Visibility.Visible;
			}
		}
    }
}