using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;

namespace PullRequestsViewer.GitHub
{
    /// <summary>
    /// Credentials repository.
    /// </summary>
    public class CredentialsRepository : ICredentialsRepository
    {
        /// <summary>
        /// The user.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Verify if the user is setted.
        /// </summary>
        /// <returns>True if the user was setted, false otherwise.</returns>
        public bool IsUserSetted()
        {
            return User != null;
        }

        /// <summary>
        /// Set the user.
        /// </summary>
        /// <param name="user">The user to set.</param>
        public void SetUser(User user)
        {
            User = user;
        }
    }
}