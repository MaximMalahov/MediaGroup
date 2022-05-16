using System.Windows.Controls;

namespace Малахов.Classes
{
    class ManagerPage
    {
        public static Frame MainFrame { get; set; }
        public static void Navigate(Page movePage)
        {
            MainFrame.Navigate(movePage);
        }
        public static void GoBack()
        {
            if(MainFrame.CanGoBack) MainFrame.GoBack();
        }
        public static Page GetPage()
        {
            return MainFrame.Content as Page;
        }


    }
}
