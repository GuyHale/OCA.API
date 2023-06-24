using System.ComponentModel;

namespace OCA.API.Helpers
{
    public static class LookUps
    {
        public enum Roles
        {
            None,

            [Description("User")]
            User,

            [Description("Admin")]
            Admin
        }
    }
}
