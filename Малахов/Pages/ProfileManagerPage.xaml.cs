using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Entity;

namespace Малахов.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfileManagerPage.xaml
    /// </summary>
    public partial class ProfileManagerPage : Page
    {
        private readonly Manager _currentManager;
        public ProfileManagerPage()
        {
            InitializeComponent();
            _currentManager = MediaGrEntities.GetContext().Managers.Any(x => x.IDUser == Data.UserID) ? MediaGrEntities.GetContext().Managers.First(x => x.IDUser == Data.UserID) : new Manager();
            DataContext = _currentManager;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустоту
            if (string.IsNullOrWhiteSpace(TbSurname.Text) || string.IsNullOrWhiteSpace(TbFirstName.Text))
            {
                MessageBox.Show("Заполните данные: Фамилия и имя!");
                return;
            }

            if (_currentManager.ID == 0)
            {
                _currentManager.IDUser = Data.UserID;
                MediaGrEntities.GetContext().Managers.Add(_currentManager);
            }

            MediaGrEntities.GetContext().SaveChanges();
            MessageBox.Show("Данные успешно сохранены!");
            ManagerPage.GoBack();
        }
    }
}
