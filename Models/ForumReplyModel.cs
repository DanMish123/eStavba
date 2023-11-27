namespace eStavba.Models
{
    public class ForumReplyModel
    {
        public int Id { get; set; }
        public int ThreadId { get; set; } 
        public string Content { get; set; }
        public string UserId { get; set; } 
        public DateTime CreatedAt { get; set; }
    
    }
}
