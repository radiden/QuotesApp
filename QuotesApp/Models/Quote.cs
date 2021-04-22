using System;
using System.ComponentModel.DataAnnotations;

namespace QuotesApp.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Please provide an at least 3 letter passcode.")]
        public string DeletionPasscode { get; set; }
    }
}
