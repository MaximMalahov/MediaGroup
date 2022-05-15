using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Малахов.Classes;
using Малахов.Entity;

namespace Малахов
{
    /// <summary>
    /// Логика взаимодействия для TypesEditPages.xaml
    /// </summary>
    public partial class TypesEditPages : Page
    {
        private Manager _currentType = new Manager();
        public TypesEditPages()
        {
            InitializeComponent();
            _currentType = new Manager();
            GetCombo();
            DataContext = _currentType;
        }
        public TypesEditPages(Manager tp)
        {
            InitializeComponent();
            _currentType = tp;
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
            if (TbSurname.Text == null)
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
            _currentType.Image = File.ReadAllBytes(filePath[0]);
            var ms = new MemoryStream(_currentType.Image);
            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = ms;
            source.EndInit();
            ImageView.Source = source;
        }
    }
}
