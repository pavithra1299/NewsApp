namespace UserRegistration.Models
{
    public class DatabaseSettings:IDatabaseSettings
    {
        public string ConnectionString { get; set; }=String.Empty;
        public string DatabaseName { get; set; }=String.Empty;  
        public string CollectionName { get; set; }=String.Empty;
      
    }
}
