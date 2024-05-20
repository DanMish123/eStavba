using System.ComponentModel.DataAnnotations;

namespace eStavba.Models
{
    public class ForumThreadModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<ForumReplyModel>? Replies { get; set; } 
    }
}
