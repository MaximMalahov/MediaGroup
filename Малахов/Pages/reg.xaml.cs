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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Малахов.Entity;
using Малахов.Classes;

namespace Малахов
{
	/// <summary>
	/// Логика взаимодействия для reg.xaml
	/// </summary>
	public partial class reg : Page
	{
		public reg()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Login.Text) && !MediaGrEntities.GetContext().Users.Where(x => x.login == Login.Text).Any())
			{
				User user = new User();
				user.login = Login.Text;
				user.password = Password.Password;
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
