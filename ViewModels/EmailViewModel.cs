namespace SendEmailApi.ViewModels
{
    public class EmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> EmailsTo { get; set; }
    }
}
