using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Entity;

namespace Малахов.Pages
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu() => InitializeComponent();
        private void BtnClientMove_Click(object sender, RoutedEventArgs e) => ManagerPage.MainFrame.Navigate(new ClientPage());
        private void BtnServiceMove_Click(object sender, RoutedEventArgs e) => ManagerPage.MainFrame.Navigate(new ServicePage());
        private void BtnProfileMove_Click(object sender, RoutedEventArgs e) => ManagerPage.MainFrame.Navigate(new ProfileManagerPage());      
        private void BtnManagerMove_Click(object sender, RoutedEventArgs e) => ManagerPage.MainFrame.Navigate(new TypePages());
        private void BtnOrderMove_Click(object sender, RoutedEventArgs e) => ManagerPage.MainFrame.Navigate(new OrderPage());

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BtnProfile.Visibility = Data.IsManager() ?  Visibility.Visible : Visibility.Collapsed;
            BtnManger.Visibility = Data.IsAdmin() ?  Visibility.Visible : Visibility.Collapsed;

            if(MediaGrEntities.GetContext().Managers.Any(x => x.IDUser == Data.UserID))
            {
                if (MediaGrEntities.GetContext().Managers.First(x => x.IDUser == Data.UserID).Fullname != "")
                    TbName.Text = $"Добро пожаловать, {MediaGrEntities.GetContext().Managers.First(x => x.IDUser == Data.UserID).Fullname}";
                else TbName.Visibility = Visibility.Collapsed;
            }
            else TbName.Visibility = Visibility.Collapsed;
        }
    }
}
