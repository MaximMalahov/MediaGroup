using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Entity;

namespace Малахов.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для OrderEditPage.xaml
    /// </summary>
    public partial class OrderEditPage : Page
    {
        private Order _currentServiceOrder;
        public OrderEditPage()
        {
            InitializeComponent();
            _currentServiceOrder = new Order();
            _currentServiceOrder.Date = DateTime.Now;
            _currentServiceOrder.Manager = MediaGrEntities.GetContext().Managers.First(x => x.IDUser == Data.UserID);
            getComboBox();
            DataContext = _currentServiceOrder;
        }
        public OrderEditPage(Order sv)
        {
            InitializeComponent();
            _currentServiceOrder = sv;
            getComboBox();
            DataContext = _currentServiceOrder;
        }
        public void getComboBox()
        {
            CBClient.ItemsSource = MediaGrEntities.GetContext().Clients.ToList();
            CBService.ItemsSource = MediaGrEntities.GetContext().Services.ToList();
            CBManager.ItemsSource = MediaGrEntities.GetContext().Managers.ToList();
            var list = new List<int>();
            for (int i = 5; i <= 60; i += 5)
                list.Add(i);
            CbDuration.ItemsSource = list;
        }

        private void BntSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBCount.Text) || CBService.SelectedItem == null || CbDuration.SelectedItem == null) return;
            if(_currentServiceOrder.ID == 0)
            MediaGrEntities.GetContext().Orders.Add(_currentServiceOrder);
            try
            {
                MediaGrEntities.GetContext().SaveChanges();
                MessageBox.Show("Вы добавили заказ");
                ManagerPage.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        

        private void CbDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateSum();


        }
        private void CalculateSum()
        {
            if (CBService.SelectedItem == null) return;
            var item = MediaGrEntities.GetContext().Services.ToList().First(x => x.Title == (CBService.SelectedItem as Service).Title);
            if (string.IsNullOrWhiteSpace(TBCount.Text) || CBService.SelectedItem == null || CbDuration.SelectedItem == null) return;
            var cost = item.Cost;
            if (!int.TryParse(TBCount.Text, out var count))
            {
                MessageBox.Show("Не верное значение");
                return;
            }
            var duration = int.Parse(CbDuration.SelectedItem.ToString());
            var sum = (cost * duration * count);
            if (sum >= 1000)
            {
                sum = sum * 0.95m;
            }
            TBSum.Text = $"Стоимость: {sum} руб.";
            _currentServiceOrder.Price = sum;
        }
        private void TBCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateSum();
        }
    }
}
