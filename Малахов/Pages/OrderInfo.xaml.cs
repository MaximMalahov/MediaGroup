using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Models.Entity;

namespace Малахов.Pages
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Page
    {
        public OrderInfo(Order sv)
        {
            InitializeComponent();
            if (sv.Manager == null)
                sv.Manager = MediaGrEntities.GetContext().Managers.First(x => x.IDUser == Data.UserID);
            DataContext = sv;
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            MediaGrEntities.GetContext().SaveChanges();
        }
    }
}