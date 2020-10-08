namespace ToDoList.Entity.DTO.Mail
{
    public class SmtpConfigDto
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; }
    }
}
