using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Models.Entity;
using Малахов.Pages.EditPages;
using word = Microsoft.Office.Interop.Word;

namespace Малахов.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            CBFilter.ItemsSource = MediaGrEntities.GetContext().Services.GroupBy(x => x.Title).Select(x => x.Key).ToList();
            CBSort.ItemsSource = DGridOrder.Columns.Select(x=> x.Header).ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage.MainFrame.Navigate(new OrderEditPage((sender as Button)?.DataContext as Order));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage.MainFrame.Navigate(new OrderEditPage());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var orderForRemoving = DGridOrder.SelectedItems.Cast<Order>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие { orderForRemoving.Count()} элементов ? ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MediaGrEntities.GetContext().Orders.RemoveRange(orderForRemoving);
                    MediaGrEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridOrder.ItemsSource = MediaGrEntities.GetContext().Orders.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + ex.StackTrace.ToString());
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BtnAdd.Visibility = Data.IsManager() ? Visibility.Visible : Visibility.Collapsed;
            BtnDelete.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            CellEdit.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            Report.Visibility = Data.IsUser() ? Visibility.Collapsed : Visibility.Visible;
            BtnInfo.Visibility = Data.IsManager() ? Visibility.Visible : Visibility.Collapsed;
            DGridOrder.ItemsSource = Data.IsUser() ? MediaGrEntities.GetContext().Orders.Where(x => x.Client.IDUser == Data.UserID).ToList() : MediaGrEntities.GetContext().Orders.ToList();
            BtnDoOrder.Visibility = Data.IsUser() ? Visibility.Visible : Visibility.Collapsed;
            
        }

        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGridOrder.ItemsSource = MediaGrEntities.GetContext().Orders.Where(x => x.Service.Title == CBFilter.SelectedItem.ToString()).ToList();
        }



        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = DGridOrder.ItemsSource.Cast<Order>().ToList();
            switch (CBSort.SelectedItem.ToString())
            {
                case "Клиент":
                   DGridOrder.ItemsSource = list.OrderBy(x => x.Client.Fullname).ToList();
                    break;
                case "Сервис":
                    DGridOrder.ItemsSource = list.OrderBy(x => x.Service.Title).ToList();
                    break;
                case "Сумма":
                    DGridOrder.ItemsSource = list.OrderBy(x => x.Price).ToList();
                   break;
                case "Длительность":
                    DGridOrder.ItemsSource = list.OrderBy(x => x.DurationInSecond).ToList();
                    break;
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = TbSearch.Text.ToLower();
            var list = new List<Order>();
            MediaGrEntities.GetContext().Orders.ToList().ForEach(x =>
            {
                if (x.Service.Title.ToLower().Contains(searchText) ||
                x.Client.Fullname.ToLower().Contains(searchText) ||
                x.Manager.Fullname.ToLower().Contains(searchText) ||
                x.Date.ToString().Contains(searchText))
                    list.Add(x);
            });
            DGridOrder.ItemsSource = list;
        }

        private void BtnDocument_Click(object sender, RoutedEventArgs e)
        {
            if (!((sender as Button)?.DataContext is Order order)) return;
            var culture = new CultureInfo("ru-RU");
            var items = new Dictionary<string, string>
            {
                ["<Number>"] = order.ID.ToString(),
                ["<Manager>"] = order.Manager.Fullname,
                ["<Client>"] = order.Client.Fullname,
                ["<Date>"] = order.Date.ToString(culture.DateTimeFormat.ShortDatePattern),
            };
            var app = new word.Application();

                object file = new FileInfo("../../Resource/Template.docx").FullName;
                var missing = Type.Missing;
                app.Documents.Open(file);
                foreach (var item in items)
                {
                    var find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;
                    object wrap = word.WdFindWrap.wdFindContinue;
                    object replace = word.WdReplace.wdReplaceAll;

                    find.Execute(
                        Type.Missing,
                        false,
                        false,
                        false,
                        missing,
                        false,
                        true,
                        wrap,
                        false,
                        missing,
                        replace
                    );
                }
                app.ActiveDocument.SaveAs(
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/{order.ID}-{order.Date.ToString(culture.DateTimeFormat.ShortDatePattern)}.docx");
                app.ActiveDocument.Close();
            
                app.Quit();

        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            new ReportPage().Show();
        }

        #region CheckBoxIsMe

        private void CbxIsMe_OnChecked(object sender, RoutedEventArgs e)
        {
            IsMeChecked();
        }
        private void CbxIsMe_OnUnchecked(object sender, RoutedEventArgs e)
        {
            IsMeChecked();
        }

        #endregion

        private void IsMeChecked()
        {
            DGridOrder.ItemsSource = CbxIsMe.IsChecked == true ? MediaGrEntities.GetContext().Orders.Where(x => x.Manager.IDUser == Data.UserID).ToList() : MediaGrEntities.GetContext().Orders.ToList();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new OrderEditPage());

        private void Info_OnClick(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new OrderInfo((sender as Button)?.DataContext as Order));

    }
}
