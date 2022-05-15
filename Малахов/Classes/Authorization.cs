using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Малахов.Entity;

namespace Малахов.Classes
{
    public class Authorization
    {
		public static bool FindUser(string login)
		{
			return MediaGrEntities.GetContext().Users.Any(user => user.login == login);
		}
		private static User GetUser(string login)
		{
			return MediaGrEntities.GetContext().Users.First(x => x.login == login);
		}
		public static bool CheckPassword(string login, string password)
		{
			return MediaGrEntities.GetContext().Users.Any(x => x.login == login && x.password == password);
		}

		public static byte GetAccess(string login, string password)
		{
			return (byte)(CheckPassword(login, password) ? GetUser(login).Access : 0);
		}

		public static int GetID(string login, string password)
		{
			return CheckPassword(login, password) ? GetUser(login).ID : 0;
		}
	}
}
