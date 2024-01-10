namespace eStavba.Models
{
    public class ForumThreadModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? UserId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public List<ForumReplyModel>? Replies { get; set; } 

    }
}
