using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Models.Entity;

namespace Малахов.Pages
{
	public partial class AuthPage : Page
	{
		public AuthPage() => InitializeComponent();

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Login.Text) && MediaGrEntities.GetContext().Users.Any(x => x.Login == Login.Text))
			{
				if (MediaGrEntities.GetContext().Users.Any(x => x.Login == Login.Text && x.Password == Password.Password))
                {
					var user = MediaGrEntities.GetContext().Users.First(x => x.Login == Login.Text && x.Password == Password.Password);
					Data.Access = user.Access;
					Data.UserID = user.ID;
					if (IsRemember.IsChecked == true) FileManager.SetConfig(new Config(user.Login, user.Password, true));
					ManagerPage.MainFrame.Navigate(new Menu());
				}
				else
					MessageBox.Show("Пароль не верный! Попробуйте еще раз");
			}
			else
				MessageBox.Show("Такого пользователя не существует!");
		}
		private void Button_Click_1(object sender, RoutedEventArgs e) => ManagerPage.MainFrame.Navigate(new RegPage());

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
			var config = FileManager.GetConfig();
			if (!config.RememberMe) return;
			Login.Text = config.Login;
			Password.Password = config.Password;
			IsRemember.IsChecked = true;
		}
    }
}