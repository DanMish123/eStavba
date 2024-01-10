namespace eStavba.Models
{
    
    public class AnnouncementModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
