namespace PullRequestsViewer.Domain.Interfaces
{
    /// <summary>
    /// Credentials repository interface.
    /// </summary>
    public interface ICredentialsRepository
    {
        /// <summary>
        /// The user.
        /// </summary>
        User User { get; }

        /// <summary>
        /// Set the user.
        /// </summary>
        /// <param name="user">The user to set.</param>
        void SetUser(User user);

        /// <summary>
        /// Verify if the user is setted.
        /// </summary>
        /// <returns>True if the user was setted, false otherwise.</returns>
        bool IsUserSetted();
    }
}