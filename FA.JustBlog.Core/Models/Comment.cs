using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        public string Email { get; set; }

        [ForeignKey("Post")]
        public Guid PostId { get; set; }

        public virtual Post Post { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        [Display(Name = "Comment Header")]
        public string CommentHeader { get; set; }

        [StringLength(1024, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        [Display(Name = "Comment Text")]
        public string CommentText { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Comment Time")]
        public DateTime CommentTime { get; set; }
    }
}
