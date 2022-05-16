using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Models.Entity;

namespace Малахов.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfileManagerPage.xaml
    /// </summary>
    public partial class ProfileManagerPage : Page
    {
        private readonly Manager _currentManager;
        private readonly Client _currentClient;
        public ProfileManagerPage()
        {
            InitializeComponent();
            if (Data.IsUser())
            {
                _currentClient = MediaGrEntities.GetContext().Clients.Any(x => x.IDUser == Data.UserID) ? MediaGrEntities.GetContext().Clients.First(x => x.IDUser == Data.UserID) : new Client();
                DataContext = _currentClient;
            }
            else
            {
                _currentManager = MediaGrEntities.GetContext().Managers.Any(x => x.IDUser == Data.UserID) ? MediaGrEntities.GetContext().Managers.First(x => x.IDUser == Data.UserID) : new Manager();
                DataContext = _currentManager;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустоту
            if (string.IsNullOrWhiteSpace(TbSurname.Text) || string.IsNullOrWhiteSpace(TbFirstName.Text))
            {
                MessageBox.Show("Заполните данные: Фамилия и имя!");
                return;
            }

            if (Data.IsUser())
            {
                if (_currentClient.ID == 0)
                {
                    _currentClient.IDUser = Data.UserID;
                    MediaGrEntities.GetContext().Clients.Add(_currentClient);
                }
            }
            else
            {
                if (_currentManager.ID == 0)
                {
                    _currentManager.IDUser = Data.UserID;
                    MediaGrEntities.GetContext().Managers.Add(_currentManager);
                }
            }

            MediaGrEntities.GetContext().SaveChanges();
            MessageBox.Show("Данные успешно сохранены!");
            ManagerPage.GoBack();
        }
    }
}
