using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientManagerWeb.ViewModels.History
{
    /// <summary>
    /// The history view model.
    /// </summary>
    public class HistoryViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets field to.
        /// </summary>
        /// <value>
        /// The field to.
        /// </value>
        [Display(Name = "Client")]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the send date.
        /// </summary>
        /// <value>
        /// The send date.
        /// </value>
        [Display(Name = "Send date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime SendDate { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From address.
        /// </value>
        [Display(Name = "Sender")]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the letter subject.
        /// </summary>
        /// <value>
        /// The letter subject.
        /// </value>
        [Display(Name = "Letter subject")]
        public string Subject { get; set; }
    }
}