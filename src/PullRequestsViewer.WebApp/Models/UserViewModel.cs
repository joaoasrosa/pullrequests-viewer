using PullRequestsViewer.Domain;
using System.ComponentModel.DataAnnotations;

namespace PullRequestsViewer.WebApp.Models
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public User ConvertTo()
        {
            return new User(Username, Password);
        }
    }
}
