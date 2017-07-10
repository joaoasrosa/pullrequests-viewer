using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;

namespace PullRequestsViewer.GitHub
{
    /// <summary>
    /// Credentials repository.
    /// </summary>
    public class CredentialsRepository : ICredentialsRepository
    {
        /// <inheritdoc />
        public User User { get; private set; }

        /// <inheritdoc />
        public bool IsUserSetted()
        {
            return User != null;
        }

        /// <inheritdoc />
        public void SetUser(User user)
        {
            User = user;
        }
    }
}