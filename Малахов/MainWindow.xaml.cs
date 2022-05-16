using System;
using System.Windows;
using Малахов.Classes;
using Малахов.Pages;
using Малахов.Pages.EditPages;

namespace Малахов
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthPage());
            ManagerPage.MainFrame = MainFrame;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (ManagerPage.GetPage() is ExamplePage page)
            {
                page.Stop();
                ManagerPage.GoBack();
            }
            else
                ManagerPage.GoBack();

        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (ManagerPage.MainFrame.Content.ToString().Contains("AuthPage"))
            {
                MainMenu.Visibility = Visibility.Collapsed;
                BtnBack.Visibility = Visibility.Hidden;
            }
            else
            {
                MainMenu.Visibility = Visibility.Visible;
                BtnBack.Visibility = Visibility.Visible;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new ClientEditPage());

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new ServiceEditPage());

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new TypesEditPages());

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new AuthPage());

        private void MenuItem_Click_4(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new OrderEditPage());

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ManagerPage.GetPage() is ExamplePage page)
                page.Stop();
        }
    }
}
