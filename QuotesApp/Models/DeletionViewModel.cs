namespace QuotesApp.Models
{
    public class DeletionViewModel
    {
        public int Id { get; set; }
        public Quote Quote { get; set; }
        public string Passcode { get; set; }
        public string Error { get; set; }
    }
}
