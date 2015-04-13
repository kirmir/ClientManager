using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerWeb.ViewModels.History
{
    /// <summary>
    /// The filter for history view model
    /// </summary>
    public class FilterHistoryViewModel
    {
        /// <summary>
        /// Gets or sets the date from.
        /// </summary>
        /// <value>
        /// The date from.
        /// </value>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the date till.
        /// </summary>
        /// <value>
        /// The date till.
        /// </value>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateTill { get; set; }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        /// <value>
        /// The history.
        /// </value>
        public IEnumerable<HistoryViewModel> History { get; set; }
    }
}