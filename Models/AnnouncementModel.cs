using System.ComponentModel.DataAnnotations;

namespace eStavba.Models
{
    
    public class AnnouncementModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}
