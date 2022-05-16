using System;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Models.Entity;

namespace Малахов.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class ClientEditPage : Page
	{
		private readonly Client _currentClient = new Client();
		public ClientEditPage()
		{
			InitializeComponent();
			DataContext = _currentClient;
		}
		public ClientEditPage(Client cl)
		{
			InitializeComponent();
			_currentClient = cl;
			DataContext = _currentClient;
		}
		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
            if (string.IsNullOrWhiteSpace(TbSurname.Text) || string.IsNullOrWhiteSpace(TbFirstName.Text) || string.IsNullOrWhiteSpace(TbPatronymic.Text))
                MessageBox.Show("Заполните пожалуйста все поля", "", MessageBoxButton.OK);
            else
            {
                if (_currentClient.ID == 0)
                    MediaGrEntities.GetContext().Clients.Add(_currentClient);
                try
                {
                    MediaGrEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message.ToString()); }
                MessageBox.Show("Вы успешно сохранили данные");
                ManagerPage.MainFrame.GoBack();
            }
        }
	}
}