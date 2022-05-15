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
    /// Логика взаимодействия для ServiceEditPage.xaml
    /// </summary>
    public partial class ServiceEditPage : Page
	{
		private readonly Service _currentService;
		public ServiceEditPage()
		{
			InitializeComponent();
			_currentService = new Service();
			DataContext = _currentService;
        }
		public ServiceEditPage(Service sv)
		{
			InitializeComponent();
			_currentService = sv;
			DataContext = _currentService;
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
            if (string.IsNullOrWhiteSpace(TbTitle.Text) || string.IsNullOrWhiteSpace(TbCost.Text))
                MessageBox.Show("Заполните пожалуйста все поля", "", MessageBoxButton.OK);
            else
            {
                if (_currentService.ID == 0) MediaGrEntities.GetContext().Services.Add(_currentService);
                try
                {
                    MediaGrEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                { MessageBox.Show($"{ex.Message}"); }
                MessageBox.Show("Вы успешно сохранили данные");
                ManagerPage.MainFrame.GoBack();
            }
        }

       

        private void Border_Drop_1(object sender, DragEventArgs e)
        {
            var filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (filePath == null) return;
            TbFileName.Text = $"Файл: {filePath[0].Split('\\').ToList().Last()}";
            _currentService.Music = File.ReadAllBytes(filePath[0]);
        }
    }
}
