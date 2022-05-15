using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Entity;
using Малахов.Classes;

namespace Малахов.Pages
{
	public partial class aut : Page
	{
		public aut() => InitializeComponent();

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Login.Text) && MediaGrEntities.GetContext().Users.Any(x => x.login == Login.Text))
			{
				if (MediaGrEntities.GetContext().Users.Any(x => x.login == Login.Text && x.password == Password.Password))
                {
					var user = MediaGrEntities.GetContext().Users.First(x => x.login == Login.Text && x.password == Password.Password);
					Data.Access = user.Access;
					Data.UserID = user.ID;
					if (IsRemember.IsChecked == true) FileManager.SetConfig(new Config(user.login, user.password, true));
					ManagerPage.MainFrame.Navigate(new Menu());
				}
				else
					MessageBox.Show("Пароль не верный! Попробуйте еще раз");
			}
			else
				MessageBox.Show("Такого пользователя не существует!");
		}
		private void Button_Click_1(object sender, RoutedEventArgs e) => ManagerPage.MainFrame.Navigate(new reg());

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