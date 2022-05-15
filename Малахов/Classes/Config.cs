using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Малахов.Classes
{
    internal class Config
    {
		public string Login { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
		public Config()
		{

		}
		public Config(string login, string password, bool remember)
		{
			Login = login;
			Password = password;
			RememberMe = remember;
		}
	}
}
