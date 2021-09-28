namespace VyprCore.Foundation.Extensions
{
    using VyprCore.Models.Domain;

    /// <summary>
    /// Vypr User Extensions
    /// </summary>
    public static class VyprUserExtensions
    {
        /// <summary>
        /// Determines whether [is system administrator].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if [is system administrator] [the specified user]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSystemAdministrator(this VyprUser user)
        {
            return user.SystemAdministrator != null;
        }
    }
}
