using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Models.Entity;

namespace Малахов.Pages
{
	/// <summary>
	/// Логика взаимодействия для RegPage.xaml
	/// </summary>
	public partial class RegPage : Page
	{
		public RegPage()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Login.Text) && !MediaGrEntities.GetContext().Users.Where(x => x.Login == Login.Text).Any())
			{
				User user = new User();
				user.Login = Login.Text;
				user.Password = Password.Password;
				MediaGrEntities.GetContext().Users.Add(user);
				MediaGrEntities.GetContext().SaveChanges();
				MessageBox.Show("Вы успешно зарегестрировались!");
				ManagerPage.MainFrame.GoBack();
			}
			else
			{
				MessageBox.Show("Такой пользователь уже существует!");
			}
		}
	}
}
