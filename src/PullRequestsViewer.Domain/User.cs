using System;

namespace PullRequestsViewer.Domain
{
    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public User(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException(nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(nameof(password));

            Username = username;
            Password = password;

            // TODO: further validation against the providers? But how once we implement mutiple?
        }

        /// <summary>
        /// The username.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// The password
        /// </summary>
        public string Password { get; private set; }
    }
}