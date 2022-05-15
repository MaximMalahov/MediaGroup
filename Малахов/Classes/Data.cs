namespace Малахов.Classes
{
    public class Data
    {
        public static int Access { get; set; }
        public static int UserID { get; set; }
        public static bool IsUser() => Access == 0;
        public static bool IsManager() => Access == 1;
        public static bool IsAdmin() => Access == 2;
        public static string CurrentDirectory { get; set; } = "MediaGroup";
        public static string CurrentConfigFile { get; set; } = "config";
        public static string CurrentConfigExtension { get; set; } = "json";
    }
}
