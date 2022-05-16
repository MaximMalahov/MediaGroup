using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Малахов.Models.Entity;

namespace Малахов
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Window
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        private void TBDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TBDateStart.SelectedDate == null || TBDateEnd.SelectedDate == null) return;
            var list = MediaGrEntities.GetContext().Orders.Where(x => x.Date >= TBDateStart.SelectedDate && x.Date <= TBDateEnd.SelectedDate).ToList();
            TblCount.Text = list.Count.ToString();
            TblSum.Text = $"{list.Sum(x => x.Price)} ₽";
        }
    }
}
