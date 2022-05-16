using System.Linq;
using Малахов.Models.Entity;

namespace Малахов.Models
{
    public class Authorization
    {
		public static bool FindUser(string login)
		{
			return MediaGrEntities.GetContext().Users.Any(user => user.Login != login);
		}
		private static User GetUser(string login)
		{
			return MediaGrEntities.GetContext().Users.First(x => x.Login == login);
		}
		public static bool CheckPassword(string login, string password)
		{
			return MediaGrEntities.GetContext().Users.Any(x => x.Login == login && x.Password == password);
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
