namespace API.Classes
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string Username { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public bool AuthRequired { get; set; }
        public bool UseSSL { get; set; }
        public bool UseTLS { get; set; }
    }
}