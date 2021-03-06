using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Models;
using Малахов.Models.Entity;

namespace Малахов.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для TypesEditPages.xaml
    /// </summary>
    public partial class TypesEditPages : Page
    {
        private readonly Manager _currentType;
        public TypesEditPages(Manager tp = null)
        {
            InitializeComponent();
            _currentType = tp ?? new Manager();
            GetCombo();
            DataContext = _currentType;
        }
        private void GetCombo()
        {
            if (_currentType.IDUser == 0)
            {
                var list = MediaGrEntities.GetContext().Users.Select(x => x.ID).Except(MediaGrEntities.GetContext().Managers.Select(x => x.IDUser)).ToList();
                CbLogin.ItemsSource = MediaGrEntities.GetContext().Users.Where(x => list.Any(y => y == x.ID)).ToList();
            }
            else
            {
                var list = MediaGrEntities.GetContext().Users.Select(x => x.ID).Except(MediaGrEntities.GetContext().Managers.Select(x => x.IDUser)).ToList();
                var users = MediaGrEntities.GetContext().Users.Where(x => list.Any(y => y == x.ID)).ToList();
                users.Add(_currentType.User);
                CbLogin.ItemsSource = users;
            }

        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbSurname.Text))
                MessageBox.Show("Заполните пожалуйста все поля", "", MessageBoxButton.OK);
            else
            {
                if (_currentType.ID == 0) MediaGrEntities.GetContext().Managers.Add(_currentType);
                try
                {
                    MediaGrEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message.ToString()); }
                MessageBox.Show("Вы успешно добавили/изменили ");
                ManagerPage.MainFrame.GoBack();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CbLogin.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            SpView.Visibility = Visibility.Collapsed;
            ImageView.Visibility = Visibility.Visible;
            var filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (filePath == null) return;
            var source = ImageManager.CroppedToBitmapImage(filePath[0]);
            _currentType.Image = ImageManager.CroppedToBytes(source);
            ImageView.Source = source;
        }
    }
}
