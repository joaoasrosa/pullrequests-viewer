using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullRequestsViewer.WebApp.Models
{
    public class PullRequestViewModel
    {
        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The HTML URL.
        /// </summary>
        public Uri HtmlUrl { get; set; }
        /// <summary>
        /// Author name.
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// Number.
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// The created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// The last update date.
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
        /// <summary>
        /// The total open time in minutes, since last update.
        /// </summary>
        public double TotalOpenTimeInMinutes
        {
            get
            {
                return (DateTime.UtcNow - LastUpdateDate).TotalMinutes;
            }
        }
    }
}
