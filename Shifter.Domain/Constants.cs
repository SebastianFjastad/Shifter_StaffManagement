
namespace Shifter.Domain
{
    /// <summary>
    /// General constants such as date formats that are used by many of the projects
    /// </summary>
    public static class Constants
    {
        public static class EmailTemplates
        {
            public static readonly string PasswordReset = "PasswordReset";

            public static readonly string ShifterRegistration = "ShifterRegistration";
        }

        public static class EmailTemplatePlaceHolders
        {
            public static readonly string Password = "{Password}";
            public static readonly string Username = "{Username}";
            public static readonly string Name = "{Name}";
        }
    }
}